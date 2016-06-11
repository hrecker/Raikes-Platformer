using UnityEngine;

public abstract class CollisionBox : MonoBehaviour
{
    protected IMessenger objectMessenger;
    protected Collider2D[] boxColliders;

    public Collider2D[] BoxColliders
    {
        get { return boxColliders; }
    }

    void Awake()
    {
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
        foreach (Collider2D boxCollider in boxColliders)
        {
            boxCollider.enabled = false;
        }
    }

    public void Activate()
    {
        foreach (Collider2D boxCollider in boxColliders)
        {
            boxCollider.enabled = true;
        }
    }
}
