using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public float baseHeight; // lowest height that this camera can go to
    public float verticalMotionLine; // how high does the player have to be for the camera to follow it verically

    void Start()
	{
        transform.position = new Vector3(transform.position.x, baseHeight, transform.position.z);
    }

    void Update()
    {
        /*if (transform.position.y < baseHeight)
        {
            transform.position = new Vector3(player.position.x, baseHeight, transform.position.z);
        }*/
        if(player != null)
        {
            if (player.position.y >= verticalMotionLine)
            {
                transform.position = new Vector3(player.position.x, baseHeight + player.position.y - verticalMotionLine, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(player.position.x, baseHeight, transform.position.z);
            }
        }
    }
}