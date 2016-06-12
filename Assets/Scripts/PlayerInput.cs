using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
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
                        movement.FullHop(true);
                        spaceReleased = false;
                        jumped = true;
                    }
                }
                else
                {
                    if (jumpFramesHeld > 0 && jumpFramesHeld <= shortHopMaxFrames)
                    {
                        movement.ShortHop(true);
                        spaceReleased = false;
                        jumped = true;
                    }
                    else if (jumpFramesHeld > 0)
                    {
                        movement.FullHop(true);
                        spaceReleased = false;
                        jumped = true;
                    }
                }
            }
        }
			
		if (Input.GetKey (KeyCode.RightArrow))
        {
			movement.horizontalDirection = HorizontalDirection.RIGHT;
		}
        else if (Input.GetKey (KeyCode.LeftArrow))
        {
			movement.horizontalDirection = HorizontalDirection.LEFT;
		}
        else
        {
			movement.horizontalDirection = HorizontalDirection.NONE;
		}

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.StartFastFall();
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            movement.StopFastFall();
        }

        if (!Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.Space))
        {
            jumpFramesHeld = 0;
        }
    }

	public void TrampolinePlatformHop(float jumpSpeed, int framesToJumpOnTrampoline)
    {
		//Jumps frame held must be greater than 0 because the player must
		//actually be holding the spacebar.
		if (jumpFramesHeld > 0 && jumpFramesHeld <= framesToJumpOnTrampoline)
        {
			movement.TrampolinePlatformHop (jumpSpeed, false);
		} else
        {
			movement.TrampolinePlatformHop (jumpSpeed, true);
		}
		jumpFramesHeld = 0;
	}

	public void BounceOnEnemy()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            movement.FullHop(false);
        }
        else
        {
            movement.ShortHop(false);
        }
	}
}
