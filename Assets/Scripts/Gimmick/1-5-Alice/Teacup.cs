using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teacup : MonoBehaviour
{
    [SerializeField]
    private List<MoveType> moveTypes = new();
    [SerializeField]
    private float firstTime = 0f;

    private int moveTypeValue = 0;

    private Vector2 setMoveDirection;
    private float setMoveSpeed;
    private float setMoveTime;
    private float setStopTime;

    void Start()
    {
        MoveSet(moveTypes[moveTypeValue]);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (firstTime > 0)
        {
            firstTime -= Time.deltaTime;
            return;
        }

        MoveUpdate();
    }
    private void MoveSet(MoveType _moveType)
    {
        setMoveDirection = _moveType.moveDirection.normalized;
        setMoveSpeed = _moveType.moveSpeed;
        setMoveTime = _moveType.moveTime;
        setStopTime = _moveType.stopTime;
    }
    private void MoveUpdate()
    {
        if (setMoveTime > 0)
        {
            setMoveTime -= 1;
            transform.position += (Vector3)(setMoveDirection * setMoveSpeed / 60);
        }
        else
        {
            setStopTime -= 1;
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
