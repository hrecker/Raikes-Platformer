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

    void Start()
    {
        input = GetComponent<PlayerInput>();
        invincibilityAlternator = GetComponent<SpriteAlternator>();
        spriteChanger = GetComponent<PlayerSpriteChanger>();
        hurtboxes = GetComponentsInChildren<Hurtbox>();
        health = GetComponent<Health>();
        makeVulnerable();
    }

    public void Invoke(Message msg, object[] args)
    {
        switch(msg)
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
                break;
            case Message.NO_HEALTH_REMAINING:
                //TODO: add logic for death
                Destroy(gameObject);
                break;
            case Message.STATE_CHANGE:
                spriteChanger.SetSprite((PlayerState) args[0]);
                break;
            case Message.DIRECTION_CHANGE:
                spriteChanger.FlipSprite((HorizontalDirection) args[0]);
                break;
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