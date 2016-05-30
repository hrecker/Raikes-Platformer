using UnityEngine;

public class BuzzwordLetterMessenger : MonoBehaviour, IMessenger
{
    private IMessenger parentMessenger;

    public void SetParentMessenger(IMessenger messenger)
    {
        parentMessenger = messenger;
    }

    public void Invoke(Message message, object[] args)
    {
        switch (message)
        {
            case Message.HIT_BY_OTHER:
                if (parentMessenger != null)
                {
                    parentMessenger.Invoke(Message.LETTER_DESTROYED, null);
                }
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
