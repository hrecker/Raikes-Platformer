using UnityEngine;

public class ShariCatMessenger : MonoBehaviour, IMessenger
{
    private ShariCatMovement movement;

    void Start()
    {
        movement = GetComponent<ShariCatMovement>();
    }

    public void Invoke(string msg, object[] args)
    {
        switch (msg)
        {
            case "HitOther":
                Debug.Log("Shari's cat hit another object");
                break;
            case "HitByOther":
                Debug.Log("Shari's cat received hit");
                Destroy(gameObject);
                break;
            case "Turn":
                movement.Turn();
                break;
        }
    }
}