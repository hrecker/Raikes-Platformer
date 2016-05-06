using UnityEngine;
using System.Collections.Generic;

public class BlueScreenComputerMessenger : MonoBehaviour, IMessenger {

    private Hitbox[] hitBoxes;
    private Hurtbox[] hurtBoxes;

    void Start()
    {
        hitBoxes = GetComponentsInChildren<Hitbox>();
        hurtBoxes = GetComponentsInChildren<Hurtbox>();
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
                break;
            case "StoppedMovement":
                ActivateHitBox();
                break;
            case "StartedMovement":
                ActivateHurtBox();
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
