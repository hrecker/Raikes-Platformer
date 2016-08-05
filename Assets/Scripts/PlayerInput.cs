using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private PlayerMovement movement;
    private ObjectSpawner gunProjectileSpawner;
    private IMessenger messenger;
    public float shortHopMaxFrames;
    public bool superFastfallActive;
    public bool gunActive;
    public float gunFireDelay;

    private float currentGunTimePassed;
    private int totalJumpFramesHeld;
    private int groundedJumpFramesHeld;
    private int framesSinceJumpPress;
    private bool jumped;
    private bool spaceReleased;

	public bool inputDisabled = false;

    void Start()
	{
        movement = GetComponent<PlayerMovement>();
        gunProjectileSpawner = GetComponent<ObjectSpawner>();
        messenger = GetComponent<IMessenger>();
        currentGunTimePassed = gunFireDelay;

		SceneMessenger.Instance.LoadPlayerState ();
    }
    
    void Update()
    {
        framesSinceJumpPress++;

		if (inputDisabled) {
			return;
		}
        // jumping
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
			
        // horizontal movement
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

        // fast fall
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

        // lasergun
        if(gunActive)
        {
            currentGunTimePassed += Time.deltaTime;
            if (Input.GetKey(KeyCode.Z) && currentGunTimePassed > gunFireDelay)
            {
                GameObject projectile = gunProjectileSpawner.SpawnObject(transform.position, gunProjectileSpawner.spawnOffset, movement.facingDirection);
                projectile.GetComponent<ProjectileMovement>().MoveInDirection(movement.facingDirection);
                currentGunTimePassed = 0;
                messenger.Invoke(Message.PROJECTILE_FIRED, null);
            }

        }

        // count jump frames
        if (Input.GetKey(KeyCode.Space))
        {
            totalJumpFramesHeld++;
            framesSinceJumpPress = 0;
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
		if (framesSinceJumpPress <= framesToJumpOnTrampoline)
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

    public void DeactivateGun()
    {
        gunActive = false;
        currentGunTimePassed = 0;
    }
}
