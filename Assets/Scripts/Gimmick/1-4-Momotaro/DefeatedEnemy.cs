using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatedEnemy : MonoBehaviour
{
    [SerializeField]
    private Vector2 direction = Vector2.zero;
    [SerializeField]
    private float power = 100f;

    private Rigidbody2D myRigidbody2D = null;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Gorilla":
                myRigidbody2D.velocity = direction * power;
                break;
        }
    }
}
