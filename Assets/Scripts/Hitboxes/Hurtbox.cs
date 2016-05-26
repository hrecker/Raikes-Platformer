using UnityEngine;
using System.Collections;

public class Hurtbox : MonoBehaviour
{
    public ColliderBoxType boxType; // what type of collisions does this hurtbox detect

    private IMessenger objectMessenger;
	private Collider2D hurtboxCollider;

	public HitboxHarmType harmType;

    public Collider2D HurtboxCollider
    {
        get { return hurtboxCollider; }
        set { hurtboxCollider = value; }
    }

    void Awake()
    {
        hurtboxCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
        objectMessenger = GetComponent<IMessenger>();
        if (objectMessenger == null)
        {
            objectMessenger = GetComponentInParent<IMessenger>();
        }
        if (objectMessenger == null)
        {
            objectMessenger = GetComponentInChildren<IMessenger>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Hitbox otherHitbox = other.GetComponent<Hitbox>();
        if (other.isTrigger && otherHitbox != null && objectMessenger != null &&
            (boxType == ColliderBoxType.ANY || otherHitbox.boxType == ColliderBoxType.ANY || boxType == otherHitbox.boxType) &&
			this.canReceiveHarm(otherHitbox))
        {
            objectMessenger.Invoke("HitByOther", null);
        }
    }

    public void Deactivate()
    {
        hurtboxCollider.enabled = false;
    }

    public void Activate()
    {
        hurtboxCollider.enabled = true;
	}

	public bool canReceiveHarm(Hitbox hitbox) {
		return (this.harmType & hitbox.harmType) != 0;
	}

}
