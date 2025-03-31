using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick2_GrannySniper : MonoBehaviour
{
    [SerializeField]
    private float rotaSpeed = 1;
    [SerializeField]
    private Transform rotaTransform = null;
    [SerializeField]
    private GameObject rangeObject = null;
    [SerializeField]
    private GameObject bulletObject = null;
    [SerializeField]
    private float rotaValue = 135;
    [SerializeField]
    private LayerMask playerLayerMask;
    [SerializeField,Header("Œ©‚Â‚¯‚é")]
    private float hitTime = 0.2f;
    [SerializeField, Header("‘_‚¢’è‚ß‚é")]
    private float preparationTime = 0.2f;
    [SerializeField, Header("’e‚±‚ß‚é")]
    private float firingTime = 1f;
    [SerializeField]
    private int hitDirection = -1;


    private int rotationDirection = 1;
    private float rayRange = 0;
    private float timer = 0;
    
    private enum Phase
    {
        Normal,
        Hit,
        Preparation,
        Firing
    }
    private Phase nowPhase = Phase.Normal;
    void Start()
    {
        rayRange = rangeObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(nowPhase == Phase.Normal)
        {
            rotaTransform.Rotate(0, 0, rotationDirection * rotaSpeed*Time.timeScale);
            if (360 - (rotaValue / 2) > rotaTransform.eulerAngles.z && rotaTransform.eulerAngles.z > 180) rotationDirection *= -1;
            else if (rotaTransform.eulerAngles.z - (rotaValue / 2) > 0 && rotaTransform.eulerAngles.z < 180) rotationDirection *= -1;

            RaycastHit2D hit = Physics2D.Raycast(rotaTransform.position, hitDirection*rotaTransform.right, rayRange, playerLayerMask);
            //Debug.DrawRay(rotaTransform.position, -rotaTransform.right, Color.red, rayRange);
            if (hit)
            {
                nowPhase = Phase.Hit;
                timer = hitTime;
            }
        }
        else if(nowPhase == Phase.Hit)
        {
            timer-= Time.deltaTime;
            if(timer < 0)
            {
                rangeObject.SetActive(false);
                timer = preparationTime;
                nowPhase = Phase.Preparation;
            }
        }
        else if (nowPhase == Phase.Preparation)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Instantiate(bulletObject,rotaTransform.position,rotaTransform.rotation);
                nowPhase = Phase.Firing;
                timer= firingTime;
            }
        }
        else if (nowPhase == Phase.Firing)
        {
            timer-=Time.deltaTime;
            if (timer < 0)
            {
                nowPhase = Phase.Normal;
                rangeObject.SetActive(true);
            }
        }

    }


}
