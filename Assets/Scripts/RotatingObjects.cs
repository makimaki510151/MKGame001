using UnityEngine;

public class RotatingObjects : MonoBehaviour
{
    [SerializeField]
    private Transform coreTransform = null;

    [SerializeField]
    private Transform imageTransform = null;

    [SerializeField]
    private float rotatingSpeed = 1;
    [SerializeField]
    private bool isRightRotation = true;

    private Transform myTransform;
    private Vector3 rotationDirection;
    void Start()
    {
        myTransform = transform;
        if (isRightRotation)
        {
            rotationDirection = myTransform.forward;
        }
        else
        {
            rotationDirection = -myTransform.forward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.RotateAround(coreTransform.position, rotationDirection, rotatingSpeed * Time.deltaTime);
        imageTransform.rotation = Quaternion.identity;
    }
}
