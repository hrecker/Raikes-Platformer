using UnityEngine;

public class PlayerMessenger : MonoBehaviour, IMessenger
{
    public float invincibilityTime; // Time that player is invincible after getting hit

    private bool invulnerable;
    private float currentTimePassed;
    private Hurtbox[] hurtboxes;
    private SpriteAlternator invincibilityAlternator;
    private PlayerSpriteChanger spriteChanger;
    private PlayerInput input;
    private Health health;
	private PlayerMovement movement;
    private AudioClipPlayer audioPlayer;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        invincibilityAlternator = GetComponent<SpriteAlternator>();
        spriteChanger = GetComponent<PlayerSpriteChanger>();
        hurtboxes = GetComponentsInChildren<Hurtbox>();
        health = GetComponent<Health>();
		movement = GetComponent<PlayerMovement> ();
        audioPlayer = GetComponent<AudioClipPlayer>();
        makeVulnerable();
    }

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.HIT_OTHER:
                Debug.Log("Player hit another object");
                input.BounceOnEnemy();
                break;
            case Message.HIT_BY_OTHER:
                Debug.Log("Player received hit");
                makeInvulnerable();
                //TODO: allow for variable damage taken?
                health.TakeDamage(1);
                SceneMessenger.Instance.Invoke(Message.HEALTH_UPDATED, new object[] { health.health, health.maxHealth });
                break;
            case Message.HEALTH_PICKUP:
                //TODO: allow for variable increase in health?
                HealthPickupBox pickupBox = (HealthPickupBox)args[0];
                if(health.IncreaseHealth(1))
                {
                    Debug.Log("Destroying pickup");
                    SceneMessenger.Instance.Invoke(Message.HEALTH_UPDATED, new object[] { health.health, health.maxHealth });
                    pickupBox.DestroyPickup();
                }
                break;
            case Message.NO_HEALTH_REMAINING:
                //TODO: add logic for death
                Destroy(gameObject);
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
        }

        if (audioPlayer != null)
        {
            audioPlayer.PlayClip(msg);
        }
    }

    void Update()
    {
        if (invulnerable)
        {
            currentTimePassed += Time.deltaTime;
            if(currentTimePassed >= invincibilityTime)
            {
                makeVulnerable();
            }
        }
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
        foreach (Hurtbox hurtbox in hurtboxes)
        {
            hurtbox.Activate();
        }
        spriteChanger.DeactivateFlash();
    }
}