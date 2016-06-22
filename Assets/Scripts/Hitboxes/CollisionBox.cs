using UnityEngine;

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
        /*foreach (Collider2D boxCollider in boxColliders)
        {
            boxCollider.enabled = false;
        }*/
        active = false;
    }

    public void Activate()
    {
        /*foreach (Collider2D boxCollider in boxColliders)
        {
            boxCollider.enabled = true;
        }*/
        active = true;
    }

    public bool IsActive()
    {
        return active;
    }
}
