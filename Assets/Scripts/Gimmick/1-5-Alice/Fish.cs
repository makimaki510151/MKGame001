using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private List<MoveType> moveTypes = new List<MoveType>();
    [SerializeField]
    private float firstTime = 0f;

    private int moveTypeValue = 0;

    private Rigidbody2D myRigidbody2D;
    
    private Vector2 setMoveDirection;
    private float setMoveSpeed;
    private float setMoveTime;
    private float setStopTime;

    void Start()
    {
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
