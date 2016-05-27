using UnityEngine;

public class BuzzwordEnemyMessenger : MonoBehaviour, IMessenger
{
    public void Invoke(string message, object[] args)
    {
        switch (message)
        {
            case "HitByOther":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

}
