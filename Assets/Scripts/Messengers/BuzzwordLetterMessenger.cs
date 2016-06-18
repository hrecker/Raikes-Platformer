using UnityEngine;

public class BuzzwordLetterMessenger : MonoBehaviour, IMessenger
{
	private IMessenger parentMessenger;
	public int pointsReceived = 10;

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
				SceneMessenger.Instance.Invoke (Message.POINTS_RECEIVED, new object[] { this.pointsReceived });
                break;
            default:
                break;
        }
    }
}
