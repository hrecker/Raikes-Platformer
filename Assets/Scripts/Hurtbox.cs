using UnityEngine;
using System.Collections;

public class Hurtbox : MonoBehaviour {

    private IMessenger objectMessenger;
    private Collider2D hurtboxCollider;

    public Collider2D HurtboxCollider
    {
        get { return hurtboxCollider; }
        set { hurtboxCollider = value; }
    }

    void Start()
    {
        hurtboxCollider = GetComponent<Collider2D>();
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
        if (other.isTrigger && other.GetComponent<Hitbox>() != null && objectMessenger != null)
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
}
