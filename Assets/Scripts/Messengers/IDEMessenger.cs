using UnityEngine;
using System;

public class IDEMessenger : MonoBehaviour, IMessenger
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private IDEMovement movement;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<IDEMovement>();
        if(sprites.Length == 0)
        {
            Debug.LogError("No sprites set for IDE enemy");
        }
        else
        {
            spriteRenderer.sprite = sprites[(int)Math.Floor(UnityEngine.Random.Range(0, sprites.Length - 0.001f))];
        }
    }

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.HIT_OTHER:
                Debug.Log("IDE logo hit another object");
                break;
            case Message.HIT_BY_OTHER:
                Debug.Log("IDE logo received hit");
                Destroy(gameObject);
                break;
            case Message.TURN:
                movement.Turn();
                break;
            case Message.BOUNCE:
                movement.Jump();
                break;
        }
    }
}
