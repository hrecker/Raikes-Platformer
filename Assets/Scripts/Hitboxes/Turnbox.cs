using UnityEngine;

public class Turnbox : MonoBehaviour
{
    private IMessenger objectMessenger;
    private Collider2D[] turnboxColliders;

    public Collider2D[] TurnboxColliders
    {
        get { return turnboxColliders; }
    }

    void Awake()
    {
        turnboxColliders = GetComponents<Collider2D>();
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
        if (objectMessenger != null)
        {
            objectMessenger.Invoke(Message.TURN, null);
        }
    }

    public void Deactivate()
    {
        foreach (Collider2D turnboxCollider in turnboxColliders)
        {
            turnboxCollider.enabled = false;
        }
    }

    public void Activate()
    {
        foreach (Collider2D turnboxCollider in turnboxColliders)
        {
            turnboxCollider.enabled = true;
        }
    }
}
