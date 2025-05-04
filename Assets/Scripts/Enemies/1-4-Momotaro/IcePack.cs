using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePack : ParentsEnemy
{
    [SerializeField]
    private GameObject damageObject = null;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float moveTime = 3;
    [SerializeField]
    private float damageTime = 2;
    [SerializeField]
    private Vector3 size = new(3,3,3);

    private float timer = 0;
    
    private bool isMove = false;

    private Rigidbody2D myRigidbody2D;
    private Transform playerTransform;

    public override void Start()
    {
        base.Start();
        playerTransform = StageRoot.Instance.GetPlayer();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.GetComponent<Rigidbody2D>().AddForce(speed * transform.right, ForceMode2D.Impulse);
        timer = moveTime;
        isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMove)
        {
            timer -= Time.deltaTime;
            if(timer<0)
            {
                myRigidbody2D.velocity = Vector3.zero;
                transform.localScale = size;
                GameObject tempObject = Instantiate(damageObject, transform);
                tempObject.transform.parent = transform;
                isMove = false;
                timer = damageTime;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
