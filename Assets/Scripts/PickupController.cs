using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public float jumpSpeedMultiplier;
    public float moveSpeedMultiplier;
    public float helmetHeight;
    public SpriteRenderer helmetSpriteRenderer;

    private Health health;
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private IMessenger messenger;
    private float previousJumpSpeed;
    private float previousMoveSpeed;
    private List<PickupType> activePowerups;
    private BoxCollider2D bodyHurtboxCollider;

    void Start()
    {
        health = GetComponent<Health>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        messenger = GetComponent<IMessenger>();
        activePowerups = new List<PickupType>();
        Hurtbox bodyHurtbox = GetComponentInChildren<Hurtbox>();
        if(bodyHurtbox != null)
        {
            bodyHurtboxCollider = bodyHurtbox.GetComponent<BoxCollider2D>();
        }
        helmetSpriteRenderer.enabled = false;
    }

    public bool Pickup(PickupBox pickupBox)
    {
        // did the pickup actually cause its effect
        bool effectActivated = false;
        switch (pickupBox.type)
        {
            case PickupType.HEALTH:
                //TODO: allow for variable increase in health?
                if (health != null && health.IncreaseHealth(1))
                {
                    //Debug.Log("Destroying pickup");
                    pickupBox.DestroyPickup();
                    effectActivated = true;
                }
                break;
            case PickupType.ARMOR:
                //TODO: allow for variable increase in armor?
                if(health != null && health.IncreaseArmor(3))
                {
                    pickupBox.DestroyPickup();
                    effectActivated = true;
                }
                break;
            case PickupType.POISON:
                if(health != null)
                {
                    health.TakeDamage(1);
                    pickupBox.DestroyPickup();
                }
                break;
            case PickupType.SPEED:
                if(playerMovement != null && (activePowerups == null || !activePowerups.Contains(PickupType.SPEED)))
                {
                    previousJumpSpeed = playerMovement.jumpSpeed;
                    previousMoveSpeed = playerMovement.moveSpeed;
                    health.IncreaseArmor(1);
                    playerMovement.jumpSpeed *= jumpSpeedMultiplier;
                    playerMovement.moveSpeed *= moveSpeedMultiplier;
                    activePowerups.Add(PickupType.SPEED);
                    pickupBox.DestroyPickup();
                    effectActivated = true;
                }
                break;
            case PickupType.SUPER_FASTFALL:
                if (playerInput != null && (activePowerups == null || !activePowerups.Contains(PickupType.SUPER_FASTFALL)))
                {
                    playerInput.superFastfallActive = true;
                    health.IncreaseArmor(1);
                    activePowerups.Add(PickupType.SUPER_FASTFALL);
                    pickupBox.DestroyPickup();
                    effectActivated = true;
                }
                break;
            case PickupType.INVINCIBILITY:
                if(messenger != null)
                {
                    messenger.Invoke(Message.INVINCIBILITY_PICKUP, null);
                    pickupBox.DestroyPickup();
                    effectActivated = true;
                }
                break;
            case PickupType.GUN:
                if(playerInput != null && (activePowerups == null || !activePowerups.Contains(PickupType.GUN)))
                {
                    playerInput.gunActive = true;
                    health.IncreaseArmor(1);
                    pickupBox.DestroyPickup();
                    activePowerups.Add(PickupType.GUN);
                    effectActivated = true;
                }
                break;
            case PickupType.HELMET:
                if(bodyHurtboxCollider != null && (activePowerups == null || !activePowerups.Contains(PickupType.HELMET)))
                {
                    bodyHurtboxCollider.size = new Vector2(bodyHurtboxCollider.size.x, bodyHurtboxCollider.size.y - helmetHeight);
                    bodyHurtboxCollider.offset = new Vector2(bodyHurtboxCollider.offset.x, bodyHurtboxCollider.offset.y - (helmetHeight / 2));
                    helmetSpriteRenderer.enabled = true;
                    activePowerups.Add(PickupType.HELMET);
                    pickupBox.DestroyPickup();
                    effectActivated = true;
                }
                break;
        }
        return effectActivated;
    }

    public void DeactivatePowerups()
    {
        foreach(PickupType powerup in activePowerups)
        {
            switch(powerup)
            {
                case PickupType.SPEED:
                    playerMovement.jumpSpeed = previousJumpSpeed;
                    playerMovement.moveSpeed = previousMoveSpeed;
                    break;
                case PickupType.SUPER_FASTFALL:
                    playerInput.superFastfallActive = false;
                    break;
                case PickupType.GUN:
                    playerInput.DeactivateGun();
                    break;
            }
        }
        activePowerups.Clear();
    }

    public void DeactivateHelmetPowerup()
    {
        bodyHurtboxCollider.size = new Vector2(bodyHurtboxCollider.size.x, bodyHurtboxCollider.size.y + helmetHeight);
        bodyHurtboxCollider.offset = new Vector2(bodyHurtboxCollider.offset.x, bodyHurtboxCollider.offset.y + (helmetHeight / 2));
        helmetSpriteRenderer.enabled = false;
        activePowerups.Remove(PickupType.HELMET);
    }

    private void ActivatePowerup(PickupType type)
    {
        if(activePowerups == null)
        {
            activePowerups = new List<PickupType>();
        }
        activePowerups.Add(type);
    }
}

