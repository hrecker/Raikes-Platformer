using UnityEngine;

public class PlayerMessenger : MonoBehaviour, IMessenger
{
    public void Invoke(string msg, object[] args)
    {
        switch(msg)
        {
		case "HitOther":
			PlayerInput input = GetComponentInParent<PlayerInput> ();
			if (input != null) {
				input.bounceOnEnemy ();
			}
            break;
        case "HitByOther":
            Debug.Log("Player received hit");
            break;
        }
    }
}
