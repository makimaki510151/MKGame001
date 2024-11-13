using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 5;
    private float lifeTimer = 0;

    [SerializeField]
    private float rotationValue = 600;

    private Rigidbody2D myRigidbody2D = null;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer += Time.deltaTime;
        if(lifeTimer > lifeTime )
        {
            lifeTimer = 0;
            Destroy(gameObject);
        }
        
        myRigidbody2D.rotation += rotationValue*Time.deltaTime;
    }
}
