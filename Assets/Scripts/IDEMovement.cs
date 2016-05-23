using UnityEngine;

public class IDEMovement : MonoBehaviour
{
    public HorizontalDirection horizontalDirection;
    public float speed;
    private Rigidbody2D rigidbodyObject;

    void Start()
    {
        rigidbodyObject = GetComponent<Rigidbody2D>();
        rigidbodyObject.velocity = new Vector2((float)horizontalDirection * speed, rigidbodyObject.velocity.y);
    }

    void Update()
    {
        rigidbodyObject.velocity = new Vector2((float)horizontalDirection * speed, rigidbodyObject.velocity.y);
    }
}