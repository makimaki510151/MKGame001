using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_IcePack : ParentsEnemy
{
    [SerializeField]
    private GameObject icePackObject = null;
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float firstTime = 0f;
    [SerializeField]
    private float throwingCT = 1f;
    [SerializeField]
    private float direction = -1;

    private float timer = 0;

    private Rigidbody2D myRigidbody2D;
    private Transform playerTransform;


    public override void Start()
    {
        base.Start();
        playerTransform = StageRoot.Instance.GetPlayer();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime > 0)
        {
            firstTime -= Time.deltaTime;
            return;
        }

        timer -= Time.deltaTime;
        if (timer < 0&&playerTransform.position.x <= myRigidbody2D.position.x)
        {
            Instantiate(icePackObject, transform);
            timer = throwingCT;
        }
    }
}
