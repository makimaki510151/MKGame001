using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeons : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float moveTime = 6;
    [SerializeField]
    private float damageTime = 2;

    private Rigidbody2D myRigidbody2D;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.GetComponent<Rigidbody2D>().AddForce(speed * -transform.right, ForceMode2D.Impulse);
        Destroy(gameObject, moveTime);
    }
}
