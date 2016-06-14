using UnityEngine;

public class ShariCatMessenger : MonoBehaviour, IMessenger
{
    private ShariCatMovement movement;

    void Start()
    {
        movement = GetComponent<ShariCatMovement>();
    }

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.HIT_OTHER:
                Debug.Log("Shari's cat hit another object");
                break;
            case Message.HIT_BY_OTHER:
                Debug.Log("Shari's cat received hit");
                Destroy(gameObject);
                break;
            case Message.TURN:
                movement.Turn();
                break;
            case Message.BOUNCE:
                movement.Jump();
                break;
        }
    }
}