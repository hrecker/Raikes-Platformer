using UnityEngine;

public class CherylFire : MonoBehaviour
{
    public float fireDelay;
    public GameObject laserPrefab;
    public float laserXScale;
    public float laserMargin;

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
            GameObject newLaser = Instantiate(laserPrefab, 
                new Vector3(transform.position.x - ((1 + laserXScale / 2) * startLaserWidth) - laserMargin, transform.position.y, transform.position.z),
                Quaternion.identity) as GameObject;
            newLaser.GetComponent<CherylLaser>().xScale = laserXScale;
            currentDelayPassed = 0;
        }
    }
}
