using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float jumpDelay;

    private Movement movement;
    private float delayPassed;

    void Start()
    {
        movement = GetComponent<Movement>();
    }
    
    void Update()
    {
        delayPassed += Time.deltaTime;
		if (Input.GetAxis ("Vertical") > 0.0 && delayPassed >= jumpDelay && movement != null) {
			delayPassed = 0;
			movement.Jump ();
		} else {
			movement.TryShortHop (Input.GetAxis("Vertical"));
		}
		this.movement.HorizontalFactor = Input.GetAxis ("Horizontal");
    }
}
