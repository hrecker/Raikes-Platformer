using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
    public float shortHopMaxFrames;
    public bool superFastfallActive;

    private int totalJumpFramesHeld;
    private int groundedJumpFramesHeld;
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
            groundedJumpFramesHeld = 0;
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
                    groundedJumpFramesHeld++;
                    if (groundedJumpFramesHeld > shortHopMaxFrames)
                    {
                        movement.FullHop(true);
                        spaceReleased = false;
                        jumped = true;
                    }
                }
                else
                {
                    if (groundedJumpFramesHeld > 0 && groundedJumpFramesHeld <= shortHopMaxFrames)
                    {
                        movement.ShortHop(true);
                        spaceReleased = false;
                        jumped = true;
                    }
                    else if (groundedJumpFramesHeld > 0)
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
            if(superFastfallActive)
            {
                movement.StartSuperFastFall();
            }
            else
            {
                movement.StartFastFall();
            }
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            movement.StopFastFall();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            totalJumpFramesHeld++;
        }
        else
        {
            totalJumpFramesHeld = 0;
        }
    }

	public void TrampolinePlatformHop(float jumpSpeed, int framesToJumpOnTrampoline)
    {
		//Jumps frame held must be greater than 0 because the player must
		//actually be holding the spacebar.
		if (totalJumpFramesHeld > 0 && totalJumpFramesHeld <= framesToJumpOnTrampoline)
        {
			movement.TrampolinePlatformHop (jumpSpeed, false);
		} else
        {
			movement.TrampolinePlatformHop (jumpSpeed, true);
		}
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
