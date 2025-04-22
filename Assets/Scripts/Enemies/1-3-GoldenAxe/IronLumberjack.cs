using UnityEngine;

public class IronLumberjack : ParentsEnemy
{
    [SerializeField]
    private float detectionSize = 5.0f;
    [SerializeField]
    private float rushWaitingTime = 1.5f;
    [SerializeField]
    private float rushSpeed = 100.0f;
    [SerializeField]
    private float distanceMagnification = 1.2f;
    [SerializeField]
    private float reRushTime = 2.0f;
    [SerializeField]
    private GameObject searchRangeObject = null;

    private float timer = 0;
    private Rigidbody2D myRigidbody2D;
    private Transform playerTransform;
    private bool isHit = false;
    private bool isRush = false;
    private bool isRrRush = false;
    private Vector3 hitPos = Vector3.zero;
    private Vector3 moveEndPos = Vector3.zero;
    private Vector3 startPos = Vector3.zero;
    private float moveRange = 0f;
    private float deltaTime = 0f;
    private float rotateValue;
    private float angle;

    public override void Start()
    {
        base.Start();
        playerTransform = StageRoot.Instance.GetPlayer();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        searchRangeObject.transform.localScale = new Vector3(detectionSize * 2, detectionSize * 2, 1);

    }

    void Update()
    {
        deltaTime = Time.deltaTime;
        if (detectionSize >= GetPlayerDistance() && !isHit &&!isRush&& !isRrRush)
        {
            timer = rushWaitingTime;
            isHit = true;
            hitPos = playerTransform.position;
            searchRangeObject.SetActive(false);

            Vector3 forwardDirection = -transform.right;
            Vector3 dir = (hitPos - transform.position).normalized;

            angle = Vector3.SignedAngle(forwardDirection, dir, Vector3.forward);
        }
        else if (isHit && !isRush)
        {
            timer -= deltaTime;
            rotateValue = angle / rushWaitingTime * deltaTime;
            transform.Rotate(new Vector3(0f, 0f, rotateValue));
            
            if (timer <= 0)
            {
                startPos = myRigidbody2D.position;
                moveEndPos = hitPos + (hitPos - (Vector3)myRigidbody2D.position) * (distanceMagnification - 1);
                moveRange = Vector2.Distance(moveEndPos, startPos);
                isRush = true;
                isHit = false;
            }
        }
        else if (isRush)
        {
            myRigidbody2D.velocity = (rushSpeed * deltaTime * (moveEndPos - startPos).normalized);
            if (Vector2.Distance(startPos, myRigidbody2D.position) >= moveRange)
            {
                RushStop();
            }
        }
        else if (isRrRush)
        {
            timer -= Time.deltaTime;
            rotateValue = 180 / reRushTime * deltaTime;

            transform.Rotate(new Vector3(0f, 0f, rotateValue));

            if (timer <= 0)
            {
                isRrRush = false;
                searchRangeObject.SetActive(true);
            }
        }
    }

    private float GetPlayerDistance()
    {
        return Vector2.Distance(playerTransform.position, myRigidbody2D.position);
    }


    private void RushStop()
    {
        myRigidbody2D.velocity = Vector2.zero;
        isRush = false;
        timer = reRushTime;
        isRrRush = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tagName = collision.transform.tag;
        Debug.Log(tagName);
        if (tagName == "Wall")
        {
            RushStop();
        }
    }
}
