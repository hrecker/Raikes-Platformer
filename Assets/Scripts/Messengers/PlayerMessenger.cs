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

    void Start()
    {
        input = GetComponent<PlayerInput>();
        invincibilityAlternator = GetComponent<SpriteAlternator>();
        spriteChanger = GetComponent<PlayerSpriteChanger>();
        hurtboxes = GetComponentsInChildren<Hurtbox>();
        makeVulnerable();
    }

    public void Invoke(string msg, object[] args)
    {
        switch(msg)
        {
            case "HitOther":
                Debug.Log("Player hit another object");
                input.BounceOnEnemy();
                break;
            case "HitByOther":
                Debug.Log("Player received hit");
                makeInvulnerable();
                break;
            case "StateChange":
                spriteChanger.SetSprite((PlayerState) args[0]);
                break;
            case "DirectionChange":
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