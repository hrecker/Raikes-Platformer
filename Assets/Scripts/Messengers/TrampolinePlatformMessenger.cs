using UnityEngine;

public class TrampolinePlatformMessenger: MonoBehaviour, IMessenger
{
	private TrampolineBounce bounce;

	public void Start()
    {
        bounce = GetComponent<TrampolineBounce>();
	}

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.PLATFORM_LANDED_ON:
                if (args[0] is Behaviour)
                {
                    Behaviour behaviour = ((Behaviour)args[0]);
                    IMessenger argMessenger = behaviour.GetComponentInHierarchy<IMessenger>();
                    argMessenger.Invoke(Message.LANDED_ON_TRAMPOLINE_PLATFORM,
                        new object[] { bounce.bounciness, bounce.framesToJumpOnTrampoline });
                }
                break;
            default:
                break;
        }
    }

}

