using System;
using UnityEngine;

public class CherylPlatform : MonoBehaviour
{
    public float startYOffset;
    private Vector3 startPosition;
    private bool reset;

    void Start()
    {
        startPosition = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + startYOffset, transform.position.z);
        SceneMessenger.Instance.AddListener(Message.BOSS_RECEIVED_HIT, new SceneMessenger.VoidCallback(ResetPosition));
    }

    public void ResetPosition()
    {
        if(!reset)
        {
            transform.position = startPosition;
            reset = true;
        }
    }
}
