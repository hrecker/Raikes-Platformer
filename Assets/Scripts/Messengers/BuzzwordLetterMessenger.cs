using UnityEngine;

public class BuzzwordLetterMessenger : MonoBehaviour, IMessenger
{
    private IMessenger parentMessenger;

    public void SetParentMessenger(IMessenger messenger)
    {
        parentMessenger = messenger;
    }

    public void Invoke(string message, object[] args)
    {
        switch (message)
        {
            case "HitByOther":
                if (parentMessenger != null)
                {
                    parentMessenger.Invoke("LetterDestroyed", null);
                }
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
