using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public ColliderBoxType boxType; // what type of collisions does this hitbox detect

    private IMessenger objectMessenger;
    private Collider2D hitboxCollider;

	public HitboxHarmType harmType;
	public bool affectsPlatforms = false;

    public Collider2D HitboxCollider
    {
        get { return hitboxCollider; }
        set { hitboxCollider = value; }
    }

    void Awake()
    {
        hitboxCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
        objectMessenger = GetComponent<IMessenger>();
        if(objectMessenger == null)
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
        Hurtbox otherHurtbox = other.GetComponent<Hurtbox>();
        if (other.isTrigger && otherHurtbox != null && objectMessenger != null &&
            (boxType == ColliderBoxType.ANY || otherHurtbox.boxType == ColliderBoxType.ANY || boxType == otherHurtbox.boxType) &&
			this.canHarm(otherHurtbox))
        {
			//Debug.Log (this + "  " + this.harmType + " " + otherHurtbox.harmType);
            objectMessenger.Invoke(Message.HIT_OTHER, null);
        }
    }

    public void Deactivate()
    {
        hitboxCollider.enabled = false;
    }

    public void Activate()
    {
        hitboxCollider.enabled = true;
    }
		
	public bool canHarm(Hurtbox hurtbox) {
		return (this.harmType & hurtbox.harmType) != 0;
	}

}
