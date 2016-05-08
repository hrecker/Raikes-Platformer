using UnityEngine;
using System.Collections;
using System;

public class MiniLeenKiatMessenger : MonoBehaviour, IMessenger
{
    public Hitbox bodyHitBox;
    public Hurtbox headHurtBox;
    public Sprite[] shortSprites;

    private MiniLeenKiatMovement movement;
    private BoxCollider2D bodyCollider;
    private SpriteAlternator spriteAlternator;

    void Start()
    {
        spriteAlternator = GetComponent<SpriteAlternator>();
        movement = GetComponent<MiniLeenKiatMovement>();
        bodyCollider = GetComponent<BoxCollider2D>();
    }

    public void Invoke(string msg, object[] args)
    {
        switch (msg)
        {
            case "HitOther":
                Debug.Log("MiniLeenKiat hit another object");
                break;
            case "HitByOther":
                Debug.Log("MiniLeenKiat received hit");
                if(!movement.Squished)
                {
                    squish();
                }
                break;
        }
    }

    private void squish()
    {
        movement.Squish();
        // move head hurtbox and body hitbox down
        headHurtBox.HurtboxCollider.offset /= 2;
        bodyHitBox.HitboxCollider.offset /= 2;

        // shrink body hitbox and regular collider
        Vector2 hitboxBoundSize = ((BoxCollider2D)bodyHitBox.HitboxCollider).size;
        Vector2 bodyBoundSize = bodyCollider.size;
        ((BoxCollider2D)bodyHitBox.HitboxCollider).size = new Vector2(hitboxBoundSize.x, hitboxBoundSize.y / 2);
        bodyCollider.size = new Vector2(bodyBoundSize.x, bodyBoundSize.y / 2);

        // change sprites
        spriteAlternator.sprites = shortSprites;
        spriteAlternator.SwitchSprite();
        spriteAlternator.switchTime /= 2;
    }
}
