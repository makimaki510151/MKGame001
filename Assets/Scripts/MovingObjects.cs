using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{

    [SerializeField]
    private Vector2 movePos = new(-1, -1);
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float destroyTime = 5;

    private Rigidbody2D myRigidbody2D;
    private float deltaTime;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;
        myRigidbody2D.MovePosition(myRigidbody2D.position + movePos * moveSpeed * deltaTime);
        destroyTime -= deltaTime;
        if (destroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
