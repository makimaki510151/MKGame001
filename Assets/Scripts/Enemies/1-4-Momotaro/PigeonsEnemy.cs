using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonsEnemy : ParentsEnemy
{
    [SerializeField]
    private List<MoveType> moveTypes = new List<MoveType>();
    [SerializeField]
    private GameObject beansObject = null;
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float firstTime = 0f;
    [SerializeField]
    private float throwingCT = 1f;


    private float timer = 0;
    private int moveTypeValue = 0;
    private int direction = -1;

    private Rigidbody2D myRigidbody2D;
    private Transform playerTransform;

    private Vector2 setMoveDirection;
    private Vector3 summonPos = Vector3.zero;
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
        if (timer < 0)
        {
            summonPos = myRigidbody2D.position;
            summonPos.x -= transform.localScale.x;
            GameObject tempObject = Instantiate(beansObject, summonPos, transform.rotation);
            tempObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 0) * force, ForceMode2D.Impulse);
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
