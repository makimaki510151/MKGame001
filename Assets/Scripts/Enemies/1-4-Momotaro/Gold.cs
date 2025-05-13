using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField]
    private GameObject damageObject = null;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float moveTime = 6;
    [SerializeField]
    private float damageTime = 2;


    private float timer = 0;

    private Rigidbody2D myRigidbody2D;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.GetComponent<Rigidbody2D>().AddForce(speed * -transform.right, ForceMode2D.Impulse);
        timer = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
