using UnityEngine;

public class LeenKiatMovement : MonoBehaviour {

    public Direction horizontalDirection;
    public float normalHorizontalSpeed;
    public float squishedHorizontalSpeed;
    public float jumpSpeed;
    private Rigidbody2D rigidbodyObject;
    private float directionModifier;
    
    void Start ()
    {
        rigidbodyObject = GetComponent<Rigidbody2D>();
        directionModifier = horizontalDirection == Direction.LEFT ? -1 : 1;
        rigidbodyObject.velocity = new Vector2(directionModifier * normalHorizontalSpeed, rigidbodyObject.velocity.y);
	}
	
	void Update ()
    {
        rigidbodyObject.velocity = new Vector2(directionModifier * normalHorizontalSpeed, rigidbodyObject.velocity.y);
    }
}
