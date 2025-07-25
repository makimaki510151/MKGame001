using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField, Header("重力の有り無し")]
    private bool isGravity = false;
    [SerializeField, Header("重力の強さ")]
    private float gravityScale = 0.1f;
    [SerializeField, Header("回転する速度")]
    private float rotationSpeed = 180f;
    [SerializeField, Header("クリックできる範囲の指定")]
    private float safeDistance = 4;
    [SerializeField, Header("回転速度を等速にするか変速にするか")]
    private bool isDebug = false;
    [SerializeField, Header("右クリック時の回転速度低下の割合")]
    private float slowRateDef = 0.25f;
    private float rateNow = 1;
    [SerializeField, Header("クリックインターバル(秒)")]
    private float clickIntervalTime = 1;
    private float clickIntervalTimer = 0;
    [SerializeField, Header("ダメージを受けた際のヒットストップ(秒)")]
    private float hitStopTime = 0.1f;
    private float hitStopTimer = 0;
    [SerializeField, Header("軸の移動時間(秒)")]
    private float axisOfRotationMoveTime = 0.5f;
    private float axisOfRotationMoveTimer = 0;
    [SerializeField, Header("プレイヤーと軸との最大距離(-4)")]
    private int playerRangeMax = 10;
    [SerializeField, Header("ダメージクールタイム")]
    private float damageCTime = 1;
    private float damageCTTimer = 0;
    [SerializeField, Header("クリックできる範囲を表示するObjectのTransform")]
    private Transform clickRanegImageTransform = null;
    [SerializeField, Header("回転する軸ObjectのTransfrom")]
    private Transform axisOfRotationTransform = null;
    [SerializeField, Header("HPのハートのObject")]
    private List<GameObject> hpUIObject = new();
    [SerializeField, Header("回転軸についているCanvasのObject")]
    private GameObject clickclickIntervalCanvasObject = null;
    private Transform clickclickIntervalCanvasTransform = null;
    [SerializeField, Header("クリックインターバルのSlider")]
    private Slider clickIntervalSlider = null;
    private GameObject clickIntervalSliderObject = null;
    [SerializeField, Header("自機が回転する範囲表示のPrefabObject")]
    private GameObject rotationRangeObject = null;
    [SerializeField, Header("池内の低下速度")]
    private float pondSpeed = 0.5f;
    [SerializeField, Header("自機が回転する範囲表示の大きさの倍率(変更非推奨)")]
    private float sizeRate = 4;
    [SerializeField, Header("プレイヤーのHP(変更非推奨)")]
    private int maxHp = 3;
    private int nowHp;
    [SerializeField, Header("プレイヤーと軸との距離を表示するテキスト")]
    private TMP_Text playerRangeText = null;
    [SerializeField, Header("プレイヤーと軸との線")]
    private LineRenderer line = null;
    [SerializeField, Header("右クリック時")]
    private GameObject rClickImageObject = null;
    [SerializeField,Header("池入った時のレイヤーオブジェクト")]
    private GameObject pondImageObject = null;
    [SerializeField, Header("お茶会エリアのレイヤーオブジェクト")]
    private GameObject teacupImageObject = null;



    enum DirectionRotation
    {
        Left,
        Right
    }

    private Vector3 axisOfRotationMovePos = Vector3.zero;
    private Vector3 axisOfRotationStartPos = Vector3.zero;

    private DirectionRotation directionRotation = DirectionRotation.Left;
    private Transform myTransform = null;
    private float radius;
    private Vector3 rotationValue = Vector3.zero;
    private SpriteRenderer axisOfRotationSpriteRenderer = null;
    private int lastDamage = 0;
    private bool isSlowRate = false;
    private bool isClickInterval = false;
    private bool isHitStop = false;
    private bool isGameOver = false;
    private bool isPause = false;
    private bool isBound = true;
    private bool isStage4Gimmick = false;


    private int lClickCount = 0;
    private int invertCount = 0;
    private float clearTime = 0;
    private float slowTime = 0;

    private GameObject tempObject = null;
    private float tempFloat;
    private Vector3 tempVector3 = new(0, 0, 0);

    public void OnLClick(InputAction.CallbackContext context)
    {
        if (isStage4Gimmick)
        {
            return;
        }
        if (context.started && Time.timeScale != 0 && !isClickInterval && !isPause)
        {
            if (TouchAct(Input.mousePosition))
            {
                //myTransform.parent = sceneRootTransform;
                tempVector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tempVector3.z = 0;
                tempFloat = Vector2.Distance(axisOfRotationTransform.position, tempVector3);

                if (Vector2.Distance(axisOfRotationTransform.position, myTransform.position) > playerRangeMax)
                {
                    return;
                }

                if (tempFloat < safeDistance)
                {
                    ClickMove(tempVector3);
                }
                else
                {
                    tempVector3 = axisOfRotationTransform.position + (tempVector3 - axisOfRotationTransform.position).normalized * safeDistance;
                    ClickMove(tempVector3);
                }
                //myTransform.parent = axisOfRotationTransform;   
            }
        }
    }
    private void ClickMove(Vector3 value)
    {
        //axisOfRotationMoveTimer = 0;
        //axisOfRotationMovePos = value;
        //axisOfRotationStartPos = axisOfRotationTransform.position;

        axisOfRotationTransform.position = value;

        lClickCount++;
        isClickInterval = true;
        clickIntervalSliderObject.SetActive(true);

        radius = Vector2.Distance(myTransform.position, axisOfRotationTransform.position);
        tempObject = Instantiate(rotationRangeObject, tempVector3, Quaternion.identity);
        tempObject.transform.parent = axisOfRotationTransform;
        tempObject.transform.localScale = new Vector3(radius * sizeRate, radius * sizeRate, 1);
    }
    private bool TouchAct(Vector2 pos)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(pos);
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(worldPoint, Vector2.zero))
        {
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("ClickBox"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void OnRClick(InputAction.CallbackContext context)
    {
        if (isStage4Gimmick)
        {
            return;
        }
        if (Time.time != 0 && !isPause)
        {
            if (context.started)
            {
                //rateNow = slowRateDef;
                isSlowRate = true;
                Time.timeScale = slowRateDef;
                rClickImageObject.SetActive(true);
            }
            else if (context.canceled&&!isGameOver)
            {
                //rateNow = 1;
                isSlowRate = false;
                Time.timeScale = 1;
                rClickImageObject.SetActive(false);
            }
        }
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (isStage4Gimmick)
        {
            return;
        }
        if (context.started)
        {
            if (isPause)
            {
                StageRoot.Instance.PauseClose();
            }
            else
            {
                ButtonPause();
            }
        }
    }

    void Start()
    {
        myTransform = transform;
        //myTransform.parent = axisOfRotationTransform;
        axisOfRotationSpriteRenderer = axisOfRotationTransform.gameObject.GetComponent<SpriteRenderer>();

        nowHp = maxHp;

        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.positionCount = 2;

        LeftRotation();
        radius = Vector2.Distance(myTransform.position, axisOfRotationTransform.position);

        clickIntervalSlider.maxValue = clickIntervalTime;
        clickclickIntervalCanvasTransform = clickclickIntervalCanvasObject.transform;
        clickIntervalSliderObject = clickIntervalSlider.gameObject;

        clickRanegImageTransform.localScale = new Vector3(safeDistance, safeDistance, 1);

        axisOfRotationMoveTimer = axisOfRotationMoveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStage4Gimmick)
        {
            return;
        }
        if (!isPause)
        {
            if (damageCTTimer > 0)
            {
                damageCTTimer -= Time.deltaTime;
            }
            // Bound Cooltime
            if (!isBound) isBound = true;
            if (isSlowRate)
            {
                slowTime += Time.unscaledDeltaTime;
            }
            clearTime += Time.deltaTime;

            if (!isDebug)
            {
                myTransform.RotateAround(axisOfRotationTransform.position, rotationValue, rateNow * Time.deltaTime * (rotationSpeed / radius));
            }
            else
            {
                myTransform.RotateAround(axisOfRotationTransform.position, rotationValue, rateNow * Time.deltaTime * rotationSpeed);
            }
            if (isGravity && Time.timeScale == 1)
            {
                tempVector3 = (axisOfRotationTransform.position - myTransform.position).normalized;
                myTransform.localPosition += tempVector3 * gravityScale * rateNow;
            }

            int playerRnageInt = (int)Vector2.Distance(myTransform.position, axisOfRotationTransform.position);
            playerRangeText.text = playerRnageInt.ToString();

            if (playerRnageInt < playerRangeMax)
            {
                axisOfRotationSpriteRenderer.color = Color.black;
            }
            else
            {
                axisOfRotationSpriteRenderer.color = Color.red;
            }

            //axisOfRotationTransform.eulerAngles += rateNow * Time.deltaTime * rotationValue;
            clickclickIntervalCanvasTransform.rotation = Quaternion.identity;
            if (isClickInterval)
            {
                clickIntervalTimer += Time.deltaTime;
                clickIntervalSlider.value = clickIntervalTimer;
                if (clickIntervalTimer > clickIntervalTime)
                {
                    clickIntervalTimer = 0;
                    isClickInterval = false;
                    clickIntervalSliderObject.SetActive(false);
                }
            }
            if (isHitStop && !isGameOver)
            {
                hitStopTimer += Time.unscaledDeltaTime;
                if (hitStopTimer > hitStopTime)
                {
                    hitStopTimer = 0;
                    isHitStop = false;
                    if (isSlowRate) Time.timeScale = slowRateDef;
                    else Time.timeScale = 1;
                }
            }

            line.SetPosition(0, myTransform.position);
            line.SetPosition(1, axisOfRotationTransform.position);
        }
    }
    private void LeftRotation()
    {
        rotationValue = -myTransform.forward;
        //rotationValue = new Vector3(0, 0, rotationSpeed);
        directionRotation = DirectionRotation.Left;
    }
    private void RightRotation()
    {
        rotationValue = myTransform.forward;
        //rotationValue = new Vector3(0, 0, -rotationSpeed);
        directionRotation = DirectionRotation.Right;
    }

    private void Damage(int value)
    {
        damageCTTimer = damageCTime;
        nowHp -= value;
        hpUIObject[nowHp].SetActive(false);
        isHitStop = true;
        Time.timeScale = 0;
        if (nowHp <= 0)
        {
            isGameOver = true;
            StageRoot.Instance.GameOver(lastDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBound)
        {
            switch (collision.tag)
            {

                case "Goal":
                    StageRoot.Instance.StgaeGoal(lClickCount, invertCount, clearTime, slowTime);
                    break;
                case "Item":
                    StageRoot.Instance.GetItem(collision.gameObject);
                    break;
                case "Enemy":
                    if (collision.GetComponent<ParentsEnemy>().Damage(1))
                    {
                        isHitStop = true;
                        Time.timeScale = 0;
                    }
                    else
                    {
                        switch (directionRotation)
                        {
                            case DirectionRotation.Left:
                                RightRotation();
                                break;
                            case DirectionRotation.Right:
                                LeftRotation();
                                break;
                        }
                        invertCount++;
                    }

                    break;
                case "EnemyAttack":
                    lastDamage = collision.GetComponent<DamageSource>().damageType;
                    Damage(1);
                    break;
                case "DamageWall":
                    if(damageCTTimer > 0)
                    {
                        break;
                    }
                    lastDamage = collision.GetComponent<DamageSource>().damageType;
                    Damage(1);
                    switch (directionRotation)
                    {
                        case DirectionRotation.Left:
                            RightRotation();
                            break;
                        case DirectionRotation.Right:
                            LeftRotation();
                            break;
                    }
                    invertCount++;
                    break;
                case "Damage":
                    if (damageCTTimer > 0)
                    {
                        break;
                    }
                    lastDamage = collision.GetComponent<DamageSource>().damageType;
                    Damage(1);
                    break;
                case "Wall":
                    switch (directionRotation)
                    {
                        case DirectionRotation.Left:
                            RightRotation();
                            break;
                        case DirectionRotation.Right:
                            LeftRotation();
                            break;
                    }
                    invertCount++;
                    break;
                case "Stage4Gimmick":
                    collision.gameObject.SetActive(false);
                    isStage4Gimmick = true;
                    StageRoot.Instance.GorillaMusouStart();
                    break;
                case "Pond":
                    rateNow = pondSpeed;
                    pondImageObject.SetActive(true);
                    break;
                case "Teacup":
                    teacupImageObject.SetActive(true);
                    break;
            }
            isBound = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBound)
        {
            switch (collision.tag)
            {
                case "Pond":
                    rateNow = 1;
                    pondImageObject.SetActive(false);
                    break;
                case "Teacup":
                    teacupImageObject.SetActive(false);
                    break;
            }
            isBound = false;
        }
    }
    public void ButtonPause()
    {
        isPause = true;
        Time.timeScale = 0;
        StageRoot.Instance.PauseOpen();
    }

    public void IsPauseChange(bool value)
    {
        isPause = value;
    }

    public void IsStage4GimmickFalse()
    {
        isStage4Gimmick = false;
    }
}
