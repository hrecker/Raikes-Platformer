using UnityEngine;

public class BuzzwordLetterMessenger : MonoBehaviour, IMessenger
{
    private IMessenger parentMessenger;

    void Start()
    {
        parentMessenger = GetComponentInParent<IMessenger>();
    }

    public void Invoke(string message, object[] args)
    {
        switch (message)
        {
            case "HitByOther":
                parentMessenger.Invoke("LetterDestroyed", null);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
