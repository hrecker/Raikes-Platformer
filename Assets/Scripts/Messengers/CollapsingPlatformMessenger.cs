using UnityEngine;

public class CollapsingPlatformMessenger: MonoBehaviour, IMessenger
{
	private CollapsingPlatformMovement movement;
	private PlatformHitbox hitbox;
	private BoxCollider2D boxCollider;
	private SpriteRenderer spriteRenderer;

	public void Start() {
		movement = GetComponent<CollapsingPlatformMovement>();
		hitbox = GetComponentInChildren<PlatformHitbox> ();
		boxCollider = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

    public void Invoke(Message msg, object[] objects)
    {
        switch (msg)
        {
            case Message.PLATFORM_LANDED_ON:
                movement.BeginCollapse();
                break;
            case Message.PLATFORM_COLLAPSED:
                hitbox.Deactivate();
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                break;
            case Message.PLATFORM_RESPAWNED:
                hitbox.Activate();
                boxCollider.enabled = true;
                spriteRenderer.enabled = true;
                break;
            default:
                break;
        }
    }
}

