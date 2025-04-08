using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class ParentsEnemy : MonoBehaviour
{
    
    public int maxHp = 1;
    
    public bool isGimmick0Wall = false;
    public int gimmick0WallValue = 0;
    public Gimmick0Wall gimmick0Wall = null;

    [System.NonSerialized]
    public int nowHp;

    [System.NonSerialized]
    public string mainCameraTagName = "MainCamera";
    [System.NonSerialized]
    public bool isCameraRendered = false;

    public virtual void Start()
    {
        nowHp = maxHp;
    }

    public virtual bool Damage(int damageValue)
    {
        nowHp -= damageValue;
        if (nowHp <= 0)
        {
            if (isGimmick0Wall)
            {
                gimmick0Wall.TrialClear(gimmick0WallValue);
            }
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public void OnWillRenderObject()
    {
        if (Camera.current.tag == mainCameraTagName)
        {
            isCameraRendered = true;
        }
    }
}
