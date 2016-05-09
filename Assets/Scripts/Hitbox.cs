using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {

    private IMessenger objectMessenger;
    private Collider2D hitboxCollider;

    public Collider2D HitboxCollider
    {
        get { return hitboxCollider; }
        set { hitboxCollider = value; }
    }

    void Start()
    {
        hitboxCollider = GetComponent<Collider2D>();
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
        if (other.isTrigger && other.GetComponent<Hurtbox>() != null && objectMessenger != null)
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
