using UnityEngine;

public class LaundryMessenger : MonoBehaviour, IMessenger
{
    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.HIT_OTHER:
                Debug.Log("Laundry machine hit another object");
                break;
            case Message.HIT_BY_OTHER:
                Debug.Log("Laundry machine received hit");
                Destroy(gameObject);
                break;
        }
    }
}