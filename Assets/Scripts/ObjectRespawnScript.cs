using UnityEngine;

public class ObjectRespawnScript : MonoBehaviour
{
    public GameObject respawnObject;
    public float respawnDelay;
    
    private GameObject currentObject;
    private float currentDelayPassed;

    void Start ()
    {
        spawnObject();
    }
	
	void Update ()
    {
	    if(currentObject == null)
        {
            currentDelayPassed += Time.deltaTime;
            if(currentDelayPassed >= respawnDelay)
            {
                spawnObject();
                currentDelayPassed = 0;
            }
        }
	}

    private void spawnObject()
    {
        currentObject = Instantiate(respawnObject, transform.position, Quaternion.identity) as GameObject;
    }
}
