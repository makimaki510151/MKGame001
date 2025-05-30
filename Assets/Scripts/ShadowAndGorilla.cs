using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAndGorilla : MonoBehaviour
{
    [SerializeField]
    private Transform gorillaTransform = null;
    [SerializeField]
    private float fallingTime = 6.5f;
    [SerializeField]
    private float speed = 1f;

    private Transform myTransform = null;
    [NonSerialized]
    public bool isFalling = true;


    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            float time = Time.deltaTime;
            Vector3 ls = myTransform.localScale;
            myTransform.localScale = new(ls.x + time * 2, ls.y + time * 2, ls.z);
            if (myTransform.localScale.x > fallingTime)
            {
                Vector3 tempPos = gorillaTransform.localPosition;
                tempPos.y -= 10;
                gorillaTransform.localPosition = tempPos;
                if (gorillaTransform.localPosition.y == 0)
                {
                    isFalling = false;
                    StageRoot.Instance.CameraShake();
                }
            }
        }
        else
        {
            myTransform.position = new(myTransform.position.x + speed * Time.deltaTime, myTransform.position.y);
        }
    }
}
