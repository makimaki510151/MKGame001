using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Gimmick3_HomingWolf : MonoBehaviour
{
    [SerializeField]
    private float detectionSize = 5.0f;
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private GameObject hitRangeObject = null;

    private Vector2 zoreVector2 = new Vector2(0, 0);
    private Vector2 moveDirection = new Vector2(0, 0);
    private Rigidbody2D myRigidbody2D;
    private Transform playerTransform;
    private bool isHit = false;

    void Start()
    {
        playerTransform = StageRoot.Instance.GetPlayer();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        hitRangeObject.transform.localScale = new Vector3(detectionSize * 2, detectionSize * 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (detectionSize >= GetPlayerDistance() && !isHit)
        {
            isHit = true;
            moveDirection = ((Vector2)playerTransform.position - myRigidbody2D.position).normalized;
            myRigidbody2D.AddForce(moveDirection * speed,ForceMode2D.Impulse);
            hitRangeObject.SetActive(false);
        }
        else if (isHit && myRigidbody2D.velocity.y == 0)
        {
            myRigidbody2D.velocity = zoreVector2;
            isHit = false;
            hitRangeObject.SetActive(true);
        }
        Debug.Log(myRigidbody2D.velocity);
    }

    private float GetPlayerDistance()
    {
        return Vector2.Distance(playerTransform.position, myRigidbody2D.position);
    }
}
