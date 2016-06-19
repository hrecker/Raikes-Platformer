using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public float jumpSpeedMultiplier;
    public float moveSpeedMultiplier;

    private Health health;
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private IMessenger messenger;
    private float previousJumpSpeed;
    private float previousMoveSpeed;
    private List<PickupType> activePowerups;

    void Start()
    {
        health = GetComponent<Health>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        messenger = GetComponent<IMessenger>();
        activePowerups = new List<PickupType>();
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
                    Debug.Log("Destroying pickup");
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
            }
        }
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

