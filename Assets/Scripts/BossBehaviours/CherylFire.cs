using UnityEngine;

public class CherylFire : MonoBehaviour
{
    public float fireDelay;
    public GameObject laserPrefab;
    public float laserYOffset;
    public float laserXScale;
    public float laserMargin;
    public bool highLaserActive; // Can cheryl shoot high lasers right now
    public int doubleInterval;

    // Delays for laser to expand
    public float lowExpandDelay; 
    public float highExpandDelay;
    public float doubleExpandDelay;
    public float laserDelayReduction; // How much does the delay reduce for final phase

    private int lasersFired;
    private float startLaserWidth;
    private float currentDelayPassed;

    void Start()
    {
        startLaserWidth = laserPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        currentDelayPassed += Time.deltaTime;
        if(currentDelayPassed >= fireDelay)
        {
            if(highLaserActive && lasersFired != 0 && lasersFired % doubleInterval == 0)
            {
                fireDoubleLaser();
            }
            // Choose to fire high or low
            else if (highLaserActive && Random.value > 0.5f)
            {
                fireHighLaser();
            }
            else
            {
                fireLowLaser();
            }
            currentDelayPassed = 0;

            if(highLaserActive)
            {
                lasersFired++;
            }
        }
    }

    private void fireLaser(float yOffset, float expandDelay)
    {
        GameObject newLaser = Instantiate(laserPrefab,
                new Vector3(transform.position.x - ((1 + laserXScale / 2) * startLaserWidth) - laserMargin, transform.position.y + yOffset, transform.position.z),
                Quaternion.identity) as GameObject;
        newLaser.GetComponent<CherylLaser>().xScale = laserXScale;
        newLaser.GetComponent<CherylLaser>().expandDelay = expandDelay;
    }

    private void fireLowLaser()
    {
        fireLaser(-laserYOffset, lowExpandDelay);
    }

    private void fireHighLaser()
    {
        fireLaser(laserYOffset, highExpandDelay);
    }

    private void fireDoubleLaser()
    {
        fireLaser(-laserYOffset, doubleExpandDelay);
        fireLaser(laserYOffset, doubleExpandDelay);
    }

    public void ReduceExpandDelay()
    {
        doubleExpandDelay -= laserDelayReduction;
        highExpandDelay -= laserDelayReduction;
        lowExpandDelay -= laserDelayReduction;
    }
}
