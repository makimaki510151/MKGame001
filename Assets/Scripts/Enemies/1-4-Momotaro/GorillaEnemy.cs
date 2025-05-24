using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaEnemy : ParentsEnemy
{
    [SerializeField]
    private GameObject goldObject = null;
    [SerializeField]
    private Vector2 summon1Pos = new(-5, 5f);
    [SerializeField]
    private Vector2 summon2Pos = new(-5, -5f);
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float firstTime = 0f;
    [SerializeField]
    private float throwingCT = 3f;
    [SerializeField]
    private float dodonCT = 0.5f;

    private float timer = 0;
    private int dodonCouner = 0;

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
        if (timer < 0 && dodonCouner == 0)
        {
            Instantiate(goldObject, (Vector2)transform.position + summon1Pos, Quaternion.identity);

            dodonCouner = 1;

            timer = dodonCT;
        }
        else if (timer < 0 && dodonCouner == 1)
        {
            Instantiate(goldObject, (Vector2)transform.position + summon2Pos, Quaternion.identity);

            dodonCouner = 0;

            timer = throwingCT;
        }
    }
}
