using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;

    public float shortHopMaxFrames;
    private int jumpFramesHeld;
    private bool jumped;
    private bool bounce; // if bounce is true, player does not need to be grounded to jump
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
                        movement.FullHop(!bounce);
                        spaceReleased = false;
                        jumped = true;
                        bounce = false;
                    }
                }
                else
                {
                    if (jumpFramesHeld > 0 && jumpFramesHeld <= shortHopMaxFrames)
                    {
                        movement.ShortHop(!bounce);
                        spaceReleased = false;
                        jumped = true;
                        bounce = false;
                    }
                    else if (jumpFramesHeld > 0)
                    {
                        movement.FullHop(!bounce);
                        spaceReleased = false;
                        jumped = true;
                        bounce = false;
                    }
                }
            }
        }
			
		if (Input.GetKey (KeyCode.RightArrow))
        {
			movement.MovementDirection = Direction.RIGHT;
		}
        else if (Input.GetKey (KeyCode.LeftArrow))
        {
			movement.MovementDirection = Direction.LEFT;
		}
        else
        {
			movement.MovementDirection = Direction.NONE;
		}
    }

	public void BounceOnEnemy()
    {
		jumpFramesHeld = 1;
		jumped = false;
		spaceReleased = true;
        bounce = true;
	}

}
