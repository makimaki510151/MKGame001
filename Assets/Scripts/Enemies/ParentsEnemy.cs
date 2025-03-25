using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParentsEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHp = 1;
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
