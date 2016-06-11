using UnityEngine;

public class ProjectileMessenger: MonoBehaviour, IMessenger
{
	private ProjectileMovement movement;

	public void Start()
    {
		movement = this.GetComponent<ProjectileMovement> ();
	}

	public void Invoke(Message message, object[] args)
    {
        switch (message)
        {
            case Message.HIT_OTHER:
                movement.Expire();
                break;
            case Message.HIT_BY_OTHER:
                movement.Expire();
                break;
            default:
                break;
        }
    }

}