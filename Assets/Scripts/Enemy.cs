using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 1;
    private int nowHp;

    [SerializeField]
    private float attackIntervalTime = 3;
    private float attackIntervalTimer = 0;

    [SerializeField]
    private Slider timeDisplay = null;

    [SerializeField]
    private GameObject attackBullet = null;

    private string mainCameraTagName = "MainCamera";
    private bool isCameraRendered = false;
    private Transform playerTransform = null;
    private Transform myTransform = null;

    private void Start()
    {
        nowHp = maxHp;
        playerTransform = StageRoot.Instance.GetPlayer();
        myTransform = transform;
        timeDisplay.maxValue = attackIntervalTime;
    }

    private void Update()
    {
        if (isCameraRendered)
        {
            attackIntervalTimer += Time.deltaTime;
            timeDisplay.value = attackIntervalTimer;
            if (attackIntervalTimer > attackIntervalTime)
            {
                attackIntervalTimer = 0;
                Vector3 diff = (playerTransform.position - myTransform.position).normalized;
                var angle = (Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg) - 90;
                Instantiate(attackBullet, myTransform.position, Quaternion.Euler(0, 0, angle));
            }
            isCameraRendered = false;
        }
    }

    public void Damage(int damageValue)
    {
        nowHp -= damageValue;
        if (nowHp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnWillRenderObject()
    {
        if (Camera.current.tag == mainCameraTagName)
        {
            isCameraRendered = true;
        }
    }
}
