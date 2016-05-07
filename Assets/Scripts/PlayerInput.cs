using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
    private float delayPassed;

    public float shortHopMaxFrames;
    private int jumpFramesHeld;
    private bool jumped;
    private bool spaceReleased;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        if(jumped && movement.IsGrounded())
        {
            jumpFramesHeld = 0;
            jumped = false;
        }

        if(!jumped)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                spaceReleased = true;
            }
            if(spaceReleased)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    jumpFramesHeld++;
                    if (jumpFramesHeld > shortHopMaxFrames)
                    {
                        movement.FullHop();
                        spaceReleased = false;
                        jumped = true;
                    }
                }
                else
                {
                    if (jumpFramesHeld > 0 && jumpFramesHeld <= shortHopMaxFrames)
                    {
                        movement.ShortHop();
                        spaceReleased = false;
                        jumped = true;
                    }
                    else if (jumpFramesHeld > 0)
                    {
                        movement.FullHop();
                        spaceReleased = false;
                        jumped = true;
                    }
                }
            }
        }
			
		if (Input.GetKey (KeyCode.RightArrow)) {
			this.movement.MovementDirection = Direction.RIGHT;
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			this.movement.MovementDirection = Direction.LEFT;
		} else {
			this.movement.MovementDirection = Direction.NONE;
		}
    }

	public void bounceOnEnemy() {
		this.jumpFramesHeld = 1;
		this.jumped = false;
		this.spaceReleased = true;
	}

}
