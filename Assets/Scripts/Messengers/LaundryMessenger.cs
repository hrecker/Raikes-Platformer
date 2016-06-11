using UnityEngine;

public class LaundryMessenger : MonoBehaviour, IMessenger
{
    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.HIT_OTHER:
                break;
            case Message.HIT_BY_OTHER:
                Destroy(gameObject);
                break;
        }
    }
}