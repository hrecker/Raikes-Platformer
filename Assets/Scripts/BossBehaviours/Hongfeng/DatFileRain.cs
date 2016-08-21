using UnityEngine;

public class DatFileRain : MonoBehaviour
{
    public GameObject datFilePrefab;
    public Transform leftSpawnPoint;
    public float spawnOffset;
    public int numSpawnPoints;
    public int gapSize;
    public float transitionTime;
    public float spawnTime;
    public float turnChance;

    private bool[] spawnPointsActive;
    private HorizontalDirection gapDirection;
    private int gapLeftBound;
    private float currentTransitionTimePassed;
    private float currentSpawnTimePassed;
    private bool rainActive;

    void Start()
    {
        StartRain();
    }

    public void StartRain()
    {
        gapDirection = HorizontalDirection.RIGHT;
        gapLeftBound = 0;
        spawnPointsActive = new bool[numSpawnPoints];
        for (int i = 0; i < numSpawnPoints; i++)
        {
            spawnPointsActive[i] = (i >= gapSize);
        }

        rainActive = true;
        currentSpawnTimePassed = spawnTime + 1.0f;
        currentTransitionTimePassed = 0;
    }

    public void StopRain()
    {
        rainActive = false;
    }

    void Update()
    {
        if(rainActive)
        {
            // transition the gap in spawn points
            currentTransitionTimePassed += Time.deltaTime;
            if (currentTransitionTimePassed >= transitionTime)
            {
                currentTransitionTimePassed = 0;
                transitionSpawns();
            }

            // spawn dat files
            currentSpawnTimePassed += Time.deltaTime;
            if (currentSpawnTimePassed >= spawnTime)
            {
                currentSpawnTimePassed = 0;
                spawnDatFiles();
            }
        }
    }

    // move the gap in active spawn points
    private void transitionSpawns()
    {
        // gap is at right most spawn point, move left
        if (gapLeftBound + gapSize >= numSpawnPoints)
        {
            gapDirection = HorizontalDirection.LEFT;
        }
        // gap is at left most spawn point, move right
        else if (gapLeftBound <= 0)
        {
            gapDirection = HorizontalDirection.RIGHT;
        }
        // random chance to turn gap
        else if(Random.value <= turnChance)
        {
            // turn gap around
            gapDirection = (HorizontalDirection) (-1.0f * (float)gapDirection);
        }

        if(gapDirection == HorizontalDirection.RIGHT)
        {
            spawnPointsActive[gapLeftBound] = true;
            spawnPointsActive[gapLeftBound + gapSize] = false;
            gapLeftBound++;
        }
        else if (gapDirection == HorizontalDirection.LEFT)
        {
            gapLeftBound--;
            spawnPointsActive[gapLeftBound + gapSize] = true;
            spawnPointsActive[gapLeftBound] = false;
        }
    }

    // spawn dat files at spawn points
    private void spawnDatFiles()
    {
        for(int i = 0; i < numSpawnPoints; i++)
        {
            if(spawnPointsActive[i])
            {
                // spawn dat file
                Instantiate(datFilePrefab, leftSpawnPoint.position + (Vector3.right * spawnOffset * i), Quaternion.identity);
            }
        }
    }
}
