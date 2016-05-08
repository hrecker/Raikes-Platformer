using UnityEngine;
using System.Collections;

public class Hurtbox : MonoBehaviour {

    private IMessenger objectMessenger;

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
        if (other.isTrigger && other.GetComponent<Hitbox>() != null && objectMessenger != null)
        {
            objectMessenger.Invoke("HitByOther", null);
        }
    }

    public void Deactivate()
    {
        foreach (Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }
    }

    public void Activate()
    {
        foreach (Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = true;
        }
    }
}
