using UnityEngine;
using System.Collections.Generic;

public class BlueScreenComputerMessenger : MonoBehaviour, IMessenger {

    private Hitbox[] hitBoxes;
    private Hurtbox[] hurtBoxes;
    private SpriteRenderer spriteRenderer;

    public Sprite movingSprite;
    public Sprite stoppedSprite;

    void Start()
    {
        hitBoxes = GetComponentsInChildren<Hitbox>();
        hurtBoxes = GetComponentsInChildren<Hurtbox>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("StartedMovement", null);
    }

    public void Invoke(string msg, object[] args)
    {
		
        switch (msg)
        {
            case "HitOther":
                Debug.Log("Blue screen computer hit another object");
                break;
            case "HitByOther":
                Debug.Log("Blue screen compuer received hit");
                Destroy(gameObject);
                break;

            case "StoppedMovement":
                ActivateHitBox();
                spriteRenderer.sprite = stoppedSprite;
                break;
            case "StartedMovement":
                ActivateHurtBox();
                spriteRenderer.sprite = movingSprite;
                break;
             
        }
    }

    private void ActivateHitBox()
    {
        foreach(Hitbox hitbox in hitBoxes)
		{
            //Hitbox exists regardless of the state,
            //it's just bigger or smaller, so we don't
            //invoke Activate()
            //hitbox.HitboxCollider.offset += new Vector2 (0.0f, 0.12f);
            //((BoxCollider2D)hitbox.HitboxCollider).size += new Vector2 (0.0f, 0.24f);
            hitbox.Activate();
        }
        foreach (Hurtbox hurtbox in hurtBoxes)
        {
            hurtbox.Deactivate();
        }
    }

    private void ActivateHurtBox()
    {
        foreach (Hitbox hitbox in hitBoxes)
		{
            //Hitbox exists regardless of the state,
            //it's just bigger or smaller, so we don't
            //invoke Deactivate()
            //hitbox.HitboxCollider.offset -= new Vector2 (0.0f, 0.12f);
            //((BoxCollider2D)hitbox.HitboxCollider).size -= new Vector2 (0.0f, 0.24f);
            hitbox.Deactivate();
        }
        foreach (Hurtbox hurtbox in hurtBoxes)
        {
            hurtbox.Activate();
        }
    }
}
