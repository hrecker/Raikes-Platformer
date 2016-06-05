using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;

    public float shortHopMaxFrames;
    private int jumpFramesHeld;
    private bool jumped;
    //private bool bounce; // if bounce is true, player does not need to be grounded to jump
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
			movement.MovementDirection = HorizontalDirection.RIGHT;
		}
        else if (Input.GetKey (KeyCode.LeftArrow))
        {
			movement.MovementDirection = HorizontalDirection.LEFT;
		}
        else
        {
			movement.MovementDirection = HorizontalDirection.NONE;
		}

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.StartFastFall();
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            movement.StopFastFall();
        }
    }

	public void TrampolinePlatformHop(float jumpSpeed, int framesToJumpOnTrampoline) {
		//Jumps frame held must be greater than 0 because the player must
		//actually be holding the spacebar.
		if (this.jumpFramesHeld > 0 && this.jumpFramesHeld <= framesToJumpOnTrampoline) {
			this.movement.TrampolinePlatformHop (jumpSpeed, false);
		} else {
			this.movement.TrampolinePlatformHop (jumpSpeed, true);
		}
		this.jumpFramesHeld = 0;
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
