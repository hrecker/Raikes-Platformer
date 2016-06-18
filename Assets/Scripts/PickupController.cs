using UnityEngine;

public class PickupController : MonoBehaviour
{
    private Health health;

    void Start()
    {
        health = GetComponent<Health>();
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
        }
        return effectActivated;
    }

    public void DeactivatePowerups()
    {

    }
}

