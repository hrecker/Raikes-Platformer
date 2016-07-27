using UnityEngine;

public class RosenbaumAttackScript : MonoBehaviour
{
    public GameObject spikePrefab;
    public GameObject helmetPrefab;
    public Transform helmetSpawnLocation;
    public float defaultAttackDuration;
    public float defaultAttackDelay;
    public float strongAttackDuration;
    public float strongAttackDelay;
    public float longDelay; // Delay after strong attacks
    [Range(0.0f, 0.1f)]
    public float defaultAttackSpawnRate;
    [Range(0.0f, 1f)]
    public float strongAttackSpawnRate;
    public float maxDefaultAttackOffset;
    public float maxStrongAttackOffset;
    public int numDefaultAttacksBeforeStrong;
    public Transform[] defaultSpawnPoints;
    public Transform[] strongAttackSpawnPoints;
    public float minStrongSpikeYPosition; // Lowest y value that spikes can spawn at during the strong attack
    public Transform player;

    private float currentTimePassed;
    private enum ActiveAttack
    {
        DEFAULT_DELAY,
        DEFAULT,
        STRONG_DELAY,
        STRONG,
        LONG_DELAY // Used for delay after strong attacks
    };
    private ActiveAttack activeAttack;
    private int numDefaultAttacksDone;
    private Transform defaultAttackSpawnPoint;
    private IMessenger messenger;
    private PickupController playerPickupController;
    private GameObject currentHelmet;
    
    void Start ()
    {
        activeAttack = ActiveAttack.DEFAULT_DELAY;
        messenger = GetComponent<IMessenger>();
        if(messenger == null)
        {
            messenger = GetComponentInParent<IMessenger>();
        }
        if(messenger == null)
        {
            messenger = GetComponentInChildren<IMessenger>();
        }
        playerPickupController = player.GetComponent<PickupController>();
	}
	
	void Update ()
    {
        currentTimePassed += Time.deltaTime;
	    switch(activeAttack)
        {
            case ActiveAttack.DEFAULT_DELAY:
                if(currentTimePassed >= defaultAttackDelay)
                {
                    if(numDefaultAttacksDone >= numDefaultAttacksBeforeStrong)
                    {
                        numDefaultAttacksDone = 0;
                        startStrongAttackDelay();
                    }
                    else
                    {
                        startDefaultAttack();
                    }
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
                    startStrongAttack();
                }
                break;
            case ActiveAttack.STRONG:
                if (currentTimePassed >= strongAttackDuration)
                {
                    activeAttack = ActiveAttack.LONG_DELAY;
                    currentTimePassed = 0;
                }
                else
                {
                    foreach(Transform spawnPoint in strongAttackSpawnPoints)
                    {
                        if (Random.value <= defaultAttackSpawnRate)
                        {
                            spawnStrongAttackSpike(spawnPoint);
                        }
                    }
                }
                break;
            case ActiveAttack.LONG_DELAY:
                if(currentTimePassed >= longDelay)
                {
                    activeAttack = ActiveAttack.DEFAULT_DELAY;
                    currentTimePassed = 0;
                    playerPickupController.DeactivateHelmetPowerup();
                }
                break;
        }
	}

    private void startDefaultAttack()
    {
        // Choose spawn point that is closes to player
        int minDistanceIndex = 0;
        if (player != null)
        {
            float minDistance = Vector2.Distance(player.position, defaultSpawnPoints[0].position);
            for (int i = 1; i < defaultSpawnPoints.Length; i++)
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
            numDefaultAttacksDone++;
        }
    }

    private void spawnDefaultAttackSpike()
    {
        Vector3 spawnPosition = new Vector3(defaultAttackSpawnPoint.position.x +
            ((Random.value - 0.5f) * 2 * maxDefaultAttackOffset), 
            defaultAttackSpawnPoint.position.y, defaultAttackSpawnPoint.position.z);
        GameObject newSpike = Instantiate(spikePrefab, spawnPosition, Quaternion.identity) as GameObject;
        newSpike.GetComponent<RosenbaumSpike>().SetDirection(VerticalDirection.UP);
    }

    private void startStrongAttackDelay()
    {
        Debug.Log("sending message now");
        messenger.Invoke(Message.START_ATTACK_DELAY, new object[] { strongAttackDelay });
        activeAttack = ActiveAttack.STRONG_DELAY;
        numDefaultAttacksDone = 0;
        currentTimePassed = 0;
        if(currentHelmet == null)
        {
            currentHelmet = Instantiate(helmetPrefab, helmetSpawnLocation.position, Quaternion.identity) as GameObject;
        }
    }

    private void startStrongAttack()
    {
        activeAttack = ActiveAttack.STRONG;
        currentTimePassed = 0;
    }

    private void spawnStrongAttackSpike(Transform spawnPoint)
    {
        float yPos = spawnPoint.position.y + ((Random.value - 0.5f) * 2 * maxStrongAttackOffset);
        if(yPos < minStrongSpikeYPosition)
        {
            yPos = minStrongSpikeYPosition;
        }
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, yPos, spawnPoint.position.z);
        GameObject newSpike = Instantiate(spikePrefab, spawnPosition, Quaternion.identity) as GameObject;
        newSpike.GetComponent<RosenbaumSpike>().SetDirection(HorizontalDirection.LEFT);
        newSpike.GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
    }
}
