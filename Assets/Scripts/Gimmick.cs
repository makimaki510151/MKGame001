using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 1;
    private int nowHp;

    
    private void Start()
    {
        nowHp = maxHp;
    }

    public void Damage(int damageValue)
    {
        nowHp -= damageValue;
        if(nowHp <= 0)
        {
            StageRoot.Instance.LastGimmickCheck();
            Destroy(gameObject);
        }
    }
}
