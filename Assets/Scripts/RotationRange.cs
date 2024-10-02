using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRange : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 0.5f;
    private float lifeTimer;

    private float lifeRate;
    private SpriteRenderer mySpriteRenderer;
    private Color myColor;
    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myColor = GetComponent<SpriteRenderer>().color;
        lifeTimer = lifeTime;
    }
    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        lifeRate = lifeTimer / lifeTime;
        mySpriteRenderer.color = new Color(myColor.r, myColor.g, myColor.b, lifeRate);
        if (lifeRate > 1)
        {
            Destroy(gameObject);
        }
    }
}
