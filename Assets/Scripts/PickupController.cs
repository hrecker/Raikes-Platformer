using UnityEngine;

public class PickupController : MonoBehaviour
{
    private Health health;
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private IMessenger messenger;

    void Start()
    {
        health = GetComponent<Health>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        messenger = GetComponent<IMessenger>();
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
        }
        return effectActivated;
    }

    public void DeactivatePowerups()
    {

    }
}

