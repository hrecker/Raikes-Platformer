using UnityEngine;
using System.Collections;
using System;

public class MiniLeenKiatMessenger : MonoBehaviour, IMessenger
{
    private MiniLeenKiatMovement movement;

    void Start()
    {
        movement = GetComponent<MiniLeenKiatMovement>();
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
                if (movement != null)
                {
                    movement.Squish();
                }
                break;
        }
    }
}
