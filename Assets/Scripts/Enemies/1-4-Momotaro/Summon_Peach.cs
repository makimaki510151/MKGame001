using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_Peach : MonoBehaviour
{
    [SerializeField]
    private float firstTime = 0;

    [SerializeField]
    private float intervalTime = 5;
    private float intervalTimer = 0;

    [SerializeField]
    private Vector2 posMisalignment = new(0, 6);

    [SerializeField]
    private GameObject summonObject = null;

    private Vector3 summonPos = Vector3.zero;

    void Start()
    {
        summonPos = gameObject.transform.position;
        summonPos += (Vector3)posMisalignment;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime > 0)
        {
            firstTime -= Time.deltaTime;
            return;
        }
        intervalTimer += Time.deltaTime;
        if (intervalTimer >= intervalTime)
        {
            intervalTimer = 0;
            Instantiate(summonObject, summonPos, Quaternion.identity);
        }
    }
}
