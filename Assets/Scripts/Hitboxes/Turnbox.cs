using UnityEngine;

public class Turnbox : CollisionBox
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (objectMessenger != null)
        {
            objectMessenger.Invoke(Message.TURN, null);
        }
    }
}
