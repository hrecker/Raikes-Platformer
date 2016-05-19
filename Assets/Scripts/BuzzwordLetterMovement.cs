using UnityEngine;
using System.Collections;

public class BuzzwordLetterMovement : MonoBehaviour {

    public float speed;
    public Direction direction;
    private Rigidbody2D rigidbodyObject;

    void Start()
    {
        rigidbodyObject = GetComponent<Rigidbody2D>();

        rigidbodyObject.velocity = getVelocity();
    }

    void Update()
    {
        rigidbodyObject.velocity = getVelocity();
    }

    private Vector2 getVelocity()
    {
        if (direction == Direction.UP || direction == Direction.DOWN)
        {
            return new Vector2(0, speed * 0.5f * (float)direction);
        }
        else
        {
            return new Vector2((float)direction * speed, 0);
        }
    }
}
