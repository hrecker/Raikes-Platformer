using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour
{
    public ColliderBoxType boxType; // what type of collisions does this hitbox detect

    private IMessenger objectMessenger;
    private Collider2D hitboxCollider;

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
            (boxType == ColliderBoxType.ANY || otherHurtbox.boxType == ColliderBoxType.ANY || boxType == otherHurtbox.boxType))
        {
            objectMessenger.Invoke("HitOther", null);
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
}
