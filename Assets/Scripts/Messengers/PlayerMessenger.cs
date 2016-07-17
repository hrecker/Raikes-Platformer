using UnityEngine;

public class PlayerMessenger : MonoBehaviour, IMessenger
{
    public float invincibilityTime; // Time that player is invincible after getting hit
    public float powerupInvincibilityTime; // Time that player is invincible after picking up invincibility powerup

    private bool invulnerable;
    private bool invulnerablePowerup;
    private float currentTimePassed;
    private Hurtbox[] hurtboxes;
    private SpriteAlternator invincibilityAlternator;
    private PlayerSpriteChanger spriteChanger;
    private PlayerInput input;
    private Health health;
	private PlayerMovement movement;
    private AudioClipPlayer audioPlayer;
    private PickupController pickupController;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        invincibilityAlternator = GetComponent<SpriteAlternator>();
        spriteChanger = GetComponent<PlayerSpriteChanger>();
        hurtboxes = GetComponentsInChildren<Hurtbox>();
        health = GetComponent<Health>();
		movement = GetComponent<PlayerMovement> ();
        audioPlayer = GetComponent<AudioClipPlayer>();
        pickupController = GetComponent<PickupController>();
        makeVulnerable();
    }

    public void Invoke(Message msg, object[] args)
    {
        bool playSound = true;
        switch (msg)
        {
            case Message.HIT_OTHER:
                //Debug.Log("Player hit another object");
                if (((ColliderBoxType)args[0]) == ColliderBoxType.BOUNCE)
                {
                    input.BounceOnEnemy();
                }
                break;
		case Message.HIT_BY_OTHER:
	                //Debug.Log ("Player received hit");
				if (!invulnerable) {
					int damage = (int)args [1];
					health.TakeDamage (damage);
				}
				makeInvulnerable ();
                //SceneMessenger.Instance.Invoke(Message.HEALTH_UPDATED, new object[] { health.health, health.maxHealth, health.armor, health.maxArmor });
                break;
			case Message.INSTANT_KILL:
				if (!invulnerable) {
					health.InstantKill ();
				}
				break;
            case Message.PICKUP:
                playSound = pickupController.Pickup((PickupBox)args[0]);
                break;
            case Message.HEALTH_UPDATED:
                SceneMessenger.Instance.Invoke(Message.HEALTH_UPDATED, new object[] { health.health, health.maxHealth, health.armor, health.maxArmor });
                break;
            case Message.NO_HEALTH_REMAINING:
                //TODO: add logic for death
                Destroy(gameObject);
                break;
            case Message.NO_ARMOR_REMAINING:
                pickupController.DeactivatePowerups();
                break;
            case Message.STATE_CHANGE:
                spriteChanger.SetSprite((PlayerState)args[0]);
                break;
            case Message.DIRECTION_CHANGE:
                spriteChanger.FlipSprite((HorizontalDirection)args[0]);
                break;
            case Message.LANDED_ON_TRAMPOLINE_PLATFORM:
                //PlayerInput keeps track of how long the space bar has been pressed.
                //It uses it for short hops, but we can use it for bouncing
                input.TrampolinePlatformHop((float)args[0], (int)args[1]);
                break;
            case Message.INVINCIBILITY_PICKUP:
                activateInvulnerablePowerup();
                break;

            case Message.HIT_SIDE_OF_PLATFORM:
                this.movement.touchingSideOfPlatform = true;
                break;
        }

        if (audioPlayer != null && playSound)
        {
            audioPlayer.PlayClip(msg);
        }
    }

    void Update()
    {
        if (invulnerable || invulnerablePowerup)
        {
            currentTimePassed += Time.deltaTime;
            if((!invulnerablePowerup && invulnerable && currentTimePassed >= invincibilityTime) ||
                (invulnerablePowerup && currentTimePassed >= powerupInvincibilityTime))
            {
                makeVulnerable();
            }
        }
    }

    private void activateInvulnerablePowerup()
    {
        currentTimePassed = 0;
        invulnerablePowerup = true;
        makeInvulnerable();
    }

    private void makeInvulnerable()
    {
        currentTimePassed = 0;
        invulnerable = true;
        foreach(Hurtbox hurtbox in hurtboxes)
        {
            hurtbox.Deactivate();
        }
        spriteChanger.ActivateFlash();
    }

    private void makeVulnerable()
    {
        currentTimePassed = 0;
        invulnerable = false;
        invulnerablePowerup = false;
        foreach (Hurtbox hurtbox in hurtboxes)
        {
            hurtbox.Activate();
        }
        spriteChanger.DeactivateFlash();
    }
}