using UnityEngine;

public class QueenOfHearts : ParentsEnemy
{
    [SerializeField]
    private GameObject trumpSoldierObject = null;
    [SerializeField]
    private Vector2 summonPos = new(-5, 0);
    [SerializeField]
    private float summonTime = 3;
    private float timer = 0;


    private Transform playerTransform = null;
    private Transform myTransform = null;

    public override void Start()
    {
        base.Start();
        timer = summonTime;
        myTransform = transform;
        playerTransform = StageRoot.Instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(100> GetPlayerDistance())
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                timer = summonTime;
                Instantiate(trumpSoldierObject, summonPos, Quaternion.identity);
            }
        }
    }

    private float GetPlayerDistance()
    {
        return Vector2.Distance(playerTransform.position, myTransform.position);
    }
}
