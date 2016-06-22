using UnityEngine;

public class PickupBox : CollisionBox
{
    public PickupType type;

    void OnTriggerStay2D(Collider2D other)
    {
        if (active && other.gameObject.tag == "Player")
        {
            IMessenger otherMessenger = other.GetComponent<IMessenger>();
            if(otherMessenger != null)
            {
                otherMessenger.Invoke(Message.PICKUP, new object[] { this });
            }
        }
    }

    public void DestroyPickup()
    {
        Destroy(transform.parent.gameObject);
    }
}
