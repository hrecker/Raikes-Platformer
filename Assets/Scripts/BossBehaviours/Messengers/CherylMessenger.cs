using UnityEngine;

public class CherylMessenger : MonoBehaviour, IMessenger
{
    public float throwVelocity;

    public void Invoke(Message msg, object[] args)
    {
        switch(msg)
        {
            case Message.HIT_BY_OTHER:
                Collider2D otherCollider = (Collider2D)args[2];
                otherCollider.GetComponentInParent<Rigidbody2D>().velocity += (-throwVelocity * Vector2.right);
                break;
        }
    }
}
