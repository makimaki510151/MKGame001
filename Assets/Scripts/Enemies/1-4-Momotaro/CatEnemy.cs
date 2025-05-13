using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEnemy : ParentsEnemy
{
    [SerializeField]
    private GameObject goldObject = null;
    [SerializeField]
    private Vector2 summonPos = new(0,1.5f);
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float firstTime = 0f;
    [SerializeField]
    private float throwingCT = 1f;

    private float timer = 0;
    private int invertNumber = 1;

    private Rigidbody2D myRigidbody2D;
    private Transform playerTransform;

    public override void Start()
    {
        base.Start();
        playerTransform = StageRoot.Instance.GetPlayer();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Instantiate(goldObject,(Vector2)transform.position + summonPos * invertNumber, Quaternion.identity);
            
            invertNumber *= -1;

            timer = throwingCT;
        }
    }
}
