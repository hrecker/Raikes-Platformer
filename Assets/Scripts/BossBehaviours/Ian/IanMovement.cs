using UnityEngine;

public class IanMovement : MonoBehaviour
{
    public Transform player;
    public float acceleration;
    public float verticalMargin;
    public float horizontalSpacing;
    public float verticalDrag;

    private float verticalSpeed;

    void Update()
    {
        // accelerate to player height
        if(transform.position.y < player.position.y - verticalMargin)
        {
            verticalSpeed += acceleration;
        }
        else if(transform.position.y > player.position.y + verticalMargin)
        {
            verticalSpeed -= acceleration;
        }

        // account for drag to slow down Ian
        if(verticalSpeed > 0)
        {
            verticalSpeed -= verticalDrag;
            if(verticalDrag < 0)
            {
                verticalDrag = 0;
            }
        }
        else if (verticalSpeed < 0)
        {
            verticalSpeed += verticalDrag;
            if (verticalDrag > 0)
            {
                verticalDrag = 0;
            }
        }

        // move Ian
        transform.position = new Vector3(player.position.x - horizontalSpacing, transform.position.y + verticalSpeed, transform.position.z);
    }
}
