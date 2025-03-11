using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick1_StraightWolf : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 5;
    private float lifeTimer = 0;

    private Rigidbody2D myRigidbody2D = null;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject,lifeTime);
    }

    void Update()
    {
        //lifeTimer += Time.deltaTime;
        //if (lifeTimer > lifeTime)
        //{
        //    lifeTimer = 0;
        //    Destroy(gameObject);
        //}
    }
}
