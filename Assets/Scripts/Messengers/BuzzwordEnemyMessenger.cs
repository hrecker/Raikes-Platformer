using UnityEngine;

public class BuzzwordEnemyMessenger : MonoBehaviour, IMessenger
{
    public void Invoke(Message message, object[] args)
    {
        switch (message)
        {
            case Message.HIT_BY_OTHER:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

}
