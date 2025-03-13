using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_SniperBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;//進行スピード
    [SerializeField]
    private float deadTime = 5;//消えるまでのタイマー
    [SerializeField]
    private bool isDestroyGround = false;//地面にぶつかったら消えるか?

    private Rigidbody2D myRigidbody2D;//自身のRB2D

    void Start()
    { //自身のRBを代入
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, deadTime);
    }
    void Update()
    {
        //自身の上方向に向かって進む
        myRigidbody2D.velocity = -transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroyGround)//isDestroyGroundがTなら
        { //当たったレイヤーがPlatformか確認
            if (collision.gameObject.tag == "Wall")
            {
                Destroy(gameObject);//自身を削除
            }
        }
    }
}
