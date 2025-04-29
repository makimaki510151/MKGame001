using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class MoveType
{
    public Vector2 moveDirection = new(1, 0);
    public float moveSpeed = 1.5f;
    public float moveTime = 1.5f;
    public float stopTime = 1f;
}

public class GoldLumberjack : ParentsEnemy
{
    [SerializeField]
    private List<MoveType> moveTypes = new List<MoveType>();
    [SerializeField]
    private GameObject summonAxeObject = null;
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float firstTime = 0f;
    [SerializeField]
    private float throwingCT = 1f;
    [SerializeField]
    private Vector2 randomRnage = new (0.2f, 1.0f);


    private float timer = 0;
    private int moveTypeValue = 0;

    private Rigidbody2D myRigidbody2D;
    private Transform playerTransform;

    private Vector2 setMoveDirection;
    private float setMoveSpeed;
    private float setMoveTime;
    private float setStopTime;

    public override void Start()
    {
        base.Start();
        playerTransform = StageRoot.Instance.GetPlayer();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        MoveSet(moveTypes[moveTypeValue]);
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime > 0)
        {
            firstTime -= Time.deltaTime;
            return;
        }

        MoveUpdate(Time.deltaTime);
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            float direction = (playerTransform.position.x - myRigidbody2D.position.x);
            if (direction < 0)
            {
                direction = -1;
            }
            else
            {
                direction = +1;
            }
            float randomForce = Random.Range(randomRnage.x, randomRnage.y);
            GameObject tempObject = Instantiate(summonAxeObject, transform);
            tempObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomForce * direction,1) * force, ForceMode2D.Impulse);
            timer = throwingCT;
        }
    }
    private void MoveSet(MoveType _moveType)
    {
        setMoveDirection = _moveType.moveDirection.normalized;
        setMoveSpeed = _moveType.moveSpeed;
        setMoveTime = _moveType.moveTime;
        setStopTime = _moveType.stopTime;
    }
    private void MoveUpdate(float deltaTime)
    {
        if (setMoveTime > 0)
        {
            setMoveTime -= deltaTime;
            myRigidbody2D.velocity = setMoveDirection * setMoveSpeed;
        }
        else
        {
            myRigidbody2D.velocity = Vector2.zero;
            setStopTime -= deltaTime;
            if (setStopTime <= 0)
            {
                moveTypeValue++;
                if (moveTypeValue >= moveTypes.Count)
                {
                    moveTypeValue = 0;
                }
                MoveSet(moveTypes[moveTypeValue]);
            }
        }
    }
}
