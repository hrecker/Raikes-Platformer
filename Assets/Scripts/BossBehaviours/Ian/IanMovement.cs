using UnityEngine;

public class IanMovement : MonoBehaviour
{
    public Transform player;
    [Range(0.0f, 0.05f)]
    public float acceleration;
    public float verticalMargin;
    public float horizontalSpacing;
    [Range(0.0f, 0.05f)]
    public float verticalDrag;
    [Range(0.0f, 0.5f)]
    public float maxVerticalSpeed;

    private float verticalSpeed;

    void Update()
    {
        //verticalSpeed = 0;

        // accelerate to player height
        if(transform.position.y < player.position.y - verticalMargin)
        {
            verticalSpeed += acceleration;
            if(verticalSpeed > maxVerticalSpeed)
            {
                verticalSpeed = maxVerticalSpeed;
            }
        }
        else if(transform.position.y > player.position.y + verticalMargin)
        {
            verticalSpeed -= acceleration;
            if(verticalSpeed < -maxVerticalSpeed)
            {
                verticalSpeed = -maxVerticalSpeed;
            }
        }

        // account for drag to slow down Ian
        if(verticalSpeed > 0)
        {
            verticalSpeed -= verticalDrag;
            if(verticalSpeed < 0)
            {
                verticalSpeed = 0;
            }
        }
        else if (verticalSpeed < 0)
        {
            verticalSpeed += verticalDrag;
            if (verticalSpeed > 0)
            {
                verticalSpeed = 0;
            }
        }

        // move Ian
        transform.position = new Vector3(player.position.x - horizontalSpacing, transform.position.y + verticalSpeed, transform.position.z);
    }
}
