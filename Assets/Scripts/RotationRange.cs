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
    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        lifeTimer += Time.deltaTime;
        lifeRate = lifeTimer / lifeTime;
        mySpriteRenderer.color = new Color(1, 0.5f, 0, lifeRate);
        if (lifeRate > 1)
        {
            Destroy(gameObject);
        }
    }
}
