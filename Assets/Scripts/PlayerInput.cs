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
        if (Input.GetButtonDown("Fire1") && delayPassed >= jumpDelay && movement != null)
        {
            delayPassed = 0;
            movement.Jump();
        }
    }
}
