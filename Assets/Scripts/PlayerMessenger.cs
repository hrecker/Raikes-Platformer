using UnityEngine;

public class PlayerMessenger : MonoBehaviour, IMessenger
{
    PlayerInput input;

    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    public void Invoke(string msg, object[] args)
    {
        switch (msg)
        {
            case "HitOther":
                if (input != null)
                {
                    input.BounceOnEnemy();
                }
                break;
            case "HitByOther":
                Debug.Log("Player received hit");
                break;
        }
    }
}
