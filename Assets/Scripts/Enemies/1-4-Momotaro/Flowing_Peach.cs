using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowing_Peach : ParentsEnemy
{
    [SerializeField]
    private List<MoveType> moveTypes = new();
    [SerializeField]
    private float firstTime = 0f;
    [SerializeField]
    private float lifeTime = 10f;


    private float timer = 0;
    private int moveTypeValue = 0;

    private Rigidbody2D myRigidbody2D;

    private Vector2 setMoveDirection;
    private float setMoveSpeed;
    private float setMoveTime;
    private float setStopTime;

    public override void Start()
    {
        base.Start();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        MoveSet(moveTypes[moveTypeValue]);
        Destroy(gameObject, lifeTime);
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
