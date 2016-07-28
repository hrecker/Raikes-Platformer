using UnityEngine;

public class RosenbaumSpike : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public float destroyTime;

    private HorizontalDirection horizontalDirection;
    private VerticalDirection verticalDirection;
    private float currentSpeed;

    void Start ()
    {
        Destroy(gameObject, destroyTime);
    }
	
	void Update ()
    {
	    if(horizontalDirection != HorizontalDirection.NONE)
        {
            currentSpeed += (acceleration * Time.deltaTime);
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
            transform.position += new Vector3(((float)horizontalDirection * currentSpeed * Time.deltaTime), 0, 0);
        }
        if (verticalDirection != VerticalDirection.NONE)
        {
            currentSpeed += (acceleration * Time.deltaTime);
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
            transform.position += new Vector3(0, ((float)verticalDirection * currentSpeed * Time.deltaTime), 0);
        }
    }

    public void SetDirection(HorizontalDirection direction)
    {
        horizontalDirection = direction;
    }

    public void SetDirection(VerticalDirection direction)
    {
        verticalDirection = direction;
    }
}
