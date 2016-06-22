﻿using UnityEngine;

public class Turnbox : CollisionBox
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (active && objectMessenger != null && other.tag != "SpawnCollider")
        {
            objectMessenger.Invoke(Message.TURN, null);
        }
    }
}
