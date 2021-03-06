﻿using UnityEngine;

public abstract class CollisionBox : MonoBehaviour
{
    protected IMessenger objectMessenger;
    protected Collider2D[] boxColliders;
    protected bool active;

    public Collider2D[] BoxColliders
    {
        get { return boxColliders; }
    }

    void Awake()
    {
        active = true;
        boxColliders = GetComponents<Collider2D>();
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

    public void Deactivate()
    {
        active = false;
    }

    public void Activate()
    {
        active = true;
        // disable and reenable each collider to cause OnTriggerEnter events to fire
        foreach(Collider2D boxCollider in boxColliders)
        {
            boxCollider.enabled = false;
            boxCollider.enabled = true;
        }
    }

    public bool IsActive()
    {
        return active;
    }
}
