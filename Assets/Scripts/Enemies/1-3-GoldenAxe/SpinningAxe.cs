using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAxe : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 10;
    [SerializeField]
    private float rotateSpeed = 10;

    private Transform myTransform = null;

    void Start()
    {
        myTransform = transform;
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        myTransform.Rotate(0, 0, rotateSpeed);
    }
}
