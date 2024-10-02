using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;//進行スピード
    [SerializeField]
    private float deadTime = 3;//消えるまでのタイマー
    [SerializeField]
    private bool isDestroyGround = false;//地面にぶつかったら消えるか?

    private Rigidbody2D myRigidbody2D;//自身のRB2D

    void Start()
    { //自身のRBを代入
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //自身の上方向に向かって進む
        myRigidbody2D.velocity = transform.up * speed;
        deadTime -= Time.deltaTime;//タイマーを減らす
        if (deadTime < 0) Destroy(gameObject);//タイマーが0以下なら自分を削除
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
