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
}
