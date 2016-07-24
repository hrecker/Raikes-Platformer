using UnityEngine;

public class RosenbaumAttackScript : MonoBehaviour
{
    public GameObject spikePrefab;
    public float defaultAttackDuration;
    public float defaultAttackDelay;
    public float strongAttackDuration;
    public float strongAttackDelay;
    [Range(0.0f, 0.1f)]
    public float defaultAttackSpawnRate;
    [Range(0.0f, 0.2f)]
    public float strongAttackSpawnRate;
    public float maxDefaultAttackOffset;
    public float maxStrongAttackOffset;
    public int numDefaultAttacksBeforeStrong;
    public Transform[] defaultSpawnPoints;
    public Transform player;

    private float currentTimePassed;
    private enum ActiveAttack
    {
        DEFAULT_DELAY,
        DEFAULT,
        STRONG_DELAY,
        STRONG
    };
    private ActiveAttack activeAttack;
    private int numDefaultAttacksDone;
    private Transform defaultAttackSpawnPoint;
    
    void Start ()
    {
        activeAttack = ActiveAttack.DEFAULT_DELAY;
	}
	
	void Update ()
    {
        currentTimePassed += Time.deltaTime;
	    switch(activeAttack)
        {
            case ActiveAttack.DEFAULT_DELAY:
                if(currentTimePassed >= defaultAttackDelay)
                {
                    startDefaultAttack();
                }
                break;
            case ActiveAttack.DEFAULT:
                if(currentTimePassed >= defaultAttackDuration)
                {
                    activeAttack = ActiveAttack.DEFAULT_DELAY;
                }
                else
                {
                    if(Random.value <= defaultAttackSpawnRate)
                    {
                        spawnDefaultAttackSpike();
                    }
                }
                break;
            case ActiveAttack.STRONG_DELAY:
                if(currentTimePassed >= strongAttackDelay)
                {

                }
                break;
            case ActiveAttack.STRONG:
                if(currentTimePassed >= strongAttackDuration)
                {

                }
                break;
        }
	}

    private void startDefaultAttack()
    {
        // Choose spawn point that is closes to player
        int minDistanceIndex = 0;
        float minDistance = Vector2.Distance(player.position, defaultSpawnPoints[0].position);
        for(int i = 1; i < defaultSpawnPoints.Length; i++)
        {
            if (Vector2.Distance(player.position, defaultSpawnPoints[i].position) < minDistance)
            {
                minDistance = Vector2.Distance(player.position, defaultSpawnPoints[i].position);
                minDistanceIndex = i;
            }
        }
        defaultAttackSpawnPoint = defaultSpawnPoints[minDistanceIndex];
        activeAttack = ActiveAttack.DEFAULT;
        currentTimePassed = 0;
    }

    private void spawnDefaultAttackSpike()
    {
        Vector3 spawnPosition = new Vector3(defaultAttackSpawnPoint.position.x +
            ((Random.value - 0.5f) * 2 * maxDefaultAttackOffset), 
            defaultAttackSpawnPoint.position.y, defaultAttackSpawnPoint.position.z);
        GameObject newSpike = Instantiate(spikePrefab, spawnPosition, Quaternion.identity) as GameObject;
        newSpike.GetComponent<RosenbaumSpike>().SetDirection(VerticalDirection.UP);
    }
}
