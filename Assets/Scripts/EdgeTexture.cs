using System;
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
        int horizontalValue = (int)horizontalDirection;
        int verticalValue = (int)verticalDirection;

        if(!vertical && horizontal)
        {
            transform.Rotate(new Vector3(0, 0, -90 * (float)horizontalDirection));
            transform.localScale = new Vector3(1, grassYScale / parentXScale, transform.localScale.z);
            float xPos = (horizontalValue * 0.5f) - (horizontalValue * (float)Math.Round(transform.localScale.y, 2) / 2.0f);
            transform.localPosition = new Vector3(xPos, 0, -1);
        }
        else if(!horizontal && vertical)
        {
            transform.Rotate(new Vector3(0, 0, -90 + ((float)verticalDirection * 90)));
            transform.localScale = new Vector3(1, grassYScale / parentYScale, transform.localScale.z);
            float yPos = (verticalValue * 0.5f) - (verticalValue * (float)Math.Round(transform.localScale.y, 2) / 2.0f);
            transform.localPosition = new Vector3(0, yPos, -1);
        }
        else
        {
            Debug.LogError("Invalid combination of horizontal and vertical chosen for GrassTexture");
        }

        transform.localScale = new Vector3(transform.localScale.x, (float)Math.Round(transform.localScale.y, 2), transform.localScale.z);
	}
}
