using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;//�i�s�X�s�[�h
    [SerializeField]
    private float deadTime = 3;//������܂ł̃^�C�}�[
    [SerializeField]
    private bool isDestroyGround = false;//�n�ʂɂԂ�����������邩?

    private Rigidbody2D myRigidbody2D;//���g��RB2D

    void Start()
    { //���g��RB����
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //���g�̏�����Ɍ������Đi��
        myRigidbody2D.velocity = transform.up * speed;
        deadTime -= Time.deltaTime;//�^�C�}�[�����炷
        if (deadTime < 0) Destroy(gameObject);//�^�C�}�[��0�ȉ��Ȃ玩�����폜
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroyGround)//isDestroyGround��T�Ȃ�
        { //�����������C���[��Platform���m�F
            if (collision.gameObject.tag == "Wall")
            {
                Destroy(gameObject);//���g���폜
            }
        }
    }
}
