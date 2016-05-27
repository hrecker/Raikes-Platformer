using UnityEngine;
using System;

public class IDEMessenger : MonoBehaviour, IMessenger
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(sprites.Length == 0)
        {
            Debug.LogError("No sprites set for IDE enemy");
        }
        else
        {
            spriteRenderer.sprite = sprites[(int)Math.Floor(UnityEngine.Random.Range(0, sprites.Length - 0.001f))];
        }
    }

    public void Invoke(string msg, object[] args)
    {

        switch (msg)
        {
            case "HitOther":
                Debug.Log("IDE logo hit another object");
                break;
            case "HitByOther":
                Debug.Log("IDE logo received hit");
                Destroy(gameObject);
                break;
        }
    }
}
