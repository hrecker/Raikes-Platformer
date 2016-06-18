using UnityEngine;

public class LaundryMessenger : MonoBehaviour, IMessenger
{
	public int pointsReceived = 10;

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.HIT_OTHER:
                break;
            case Message.HIT_BY_OTHER:
				Destroy(gameObject);
				SceneMessenger.Instance.Invoke (Message.POINTS_RECEIVED, new object[] { this.pointsReceived });
                break;
        }
    }
}