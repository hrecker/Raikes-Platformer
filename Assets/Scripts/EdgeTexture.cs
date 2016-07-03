using UnityEngine;

public class EdgeTexture : MonoBehaviour
{
    public Transform parentTexture;
    public float grassYScale = 2;
    public bool vertical = true;
    public VerticalDirection verticalDirection = VerticalDirection.UP;
    public bool horizontal;
    public HorizontalDirection horizontalDirection;
    
	void Start ()
    {
        float parentYScale = parentTexture.localScale.y;
        float parentXScale = parentTexture.localScale.x;

        if(!vertical && horizontal)
        {
            transform.Rotate(new Vector3(0, 0, -90 * (float)horizontalDirection));
            transform.position = new Vector3(
                parentTexture.position.x + ((float)horizontalDirection * parentXScale / 2.0f) - (float)horizontalDirection,
                parentTexture.position.y, transform.position.z);
            transform.localScale = new Vector3(1, grassYScale / parentXScale, transform.localScale.z);
        }
        else if(!horizontal && vertical)
        {
            transform.Rotate(new Vector3(0, 0, -90 + ((float)verticalDirection * 90)));
            transform.position = new Vector3(parentTexture.position.x,
                parentTexture.position.y + ((float)verticalDirection * parentYScale / 2.0f) - (float)verticalDirection,
                transform.position.z);
            transform.localScale = new Vector3(1, grassYScale / parentYScale, transform.localScale.z);
        }
        else
        {
            Debug.LogError("Invalid combination of horizontal and vertical chosen for GrassTexture");
        }
	}
}
