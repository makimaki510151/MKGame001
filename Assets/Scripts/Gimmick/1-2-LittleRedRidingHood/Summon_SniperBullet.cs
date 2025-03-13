using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_SniperBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;//�i�s�X�s�[�h
    [SerializeField]
    private float deadTime = 5;//������܂ł̃^�C�}�[
    [SerializeField]
    private bool isDestroyGround = false;//�n�ʂɂԂ�����������邩?

    private Rigidbody2D myRigidbody2D;//���g��RB2D

    void Start()
    { //���g��RB����
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, deadTime);
    }
    void Update()
    {
        //���g�̏�����Ɍ������Đi��
        myRigidbody2D.velocity = -transform.right * speed;
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
