using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingGimmick : MonoBehaviour
{
    [SerializeField]
    private float firstTime = 0;

    [SerializeField]
    private float intervalTime = 5;
    private float intervalTimer = 0;

    [SerializeField]
    private GameObject fallingObject = null;

    private Vector3 summonPos = Vector3.zero;

    void Start()
    {
        summonPos = gameObject.transform.position;
        summonPos.y += 6;
    }

    // Update is called once per frame
    void Update()
    {
        if(firstTime > 0)
        {
            firstTime -= Time.deltaTime;
            return;
        }
        intervalTimer += Time.deltaTime;
        if (intervalTimer >= intervalTime)
        {
            intervalTimer = 0;
            Instantiate(fallingObject, summonPos, Quaternion.identity);
        }
    }
}
