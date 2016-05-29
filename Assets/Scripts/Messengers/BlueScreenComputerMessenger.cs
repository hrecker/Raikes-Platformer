﻿using UnityEngine;
using System.Collections.Generic;

public class BlueScreenComputerMessenger : MonoBehaviour, IMessenger {

    private Hitbox[] hitBoxes;
    private Hurtbox[] hurtBoxes;
    private Turnbox turnBox;
    private SpriteRenderer spriteRenderer;
    private BlueScreenComputerMovement movement;

    public Sprite movingSprite;
    public Sprite stoppedSprite;

    void Start()
    {
        hitBoxes = GetComponentsInChildren<Hitbox>();
        hurtBoxes = GetComponentsInChildren<Hurtbox>();
        turnBox = GetComponentInChildren<Turnbox>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<BlueScreenComputerMovement>();
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
            case "Turn":
                movement.Turn();
                break;

            case "StoppedMovement":
                ActivateHitBox();
                turnBox.Deactivate();
                spriteRenderer.sprite = stoppedSprite;
                break;
            case "StartedMovement":
                ActivateHurtBox();
                turnBox.Activate();
                spriteRenderer.sprite = movingSprite;
                break;
             
        }
    }

    private void ActivateHitBox()
    {
        foreach(Hitbox hitbox in hitBoxes)
		{
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
            hitbox.Deactivate();
        }
        foreach (Hurtbox hurtbox in hurtBoxes)
        {
            hurtbox.Activate();
        }
    }
}
