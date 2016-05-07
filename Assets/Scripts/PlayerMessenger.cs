using UnityEngine;

public class PlayerMessenger : MonoBehaviour, IMessenger
{
    public void Invoke(string msg, object[] args)
    {
        switch(msg)
        {
            case "HitOther":
                Debug.Log("Player hit another object");
                break;
            case "HitByOther":
                Debug.Log("Player received hit");
                break;
        }
    }
}
