using UnityEngine;

public class Turnbox : CollisionBox
{
    public float turnDelay = 1.0f;
    private float turnTimePassed;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (active && objectMessenger != null && other.tag != "SpawnCollider" && other.tag != "Ground")
        {
            objectMessenger.Invoke(Message.TURN, null);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(turnTimePassed >= turnDelay)
        {
            turnTimePassed = 0;
            OnTriggerEnter2D(other);
        }
    }

    void Update()
    {
        if(turnTimePassed < turnDelay)
        {
            turnTimePassed += Time.deltaTime;
        }
    }

    public void DeactiveLeftmostBox()
    {
        Debug.Log("deactivating leftmost");
        int leftMostIndex = 0;
        for(int i = 1; i < boxColliders.Length; i++)
        {
            if(boxColliders[i].offset.x < boxColliders[leftMostIndex].offset.x)
            {
                leftMostIndex = i;
            }
            else
            {
                boxColliders[i].enabled = true;
            }
        }
        boxColliders[leftMostIndex].enabled = false;
        if(leftMostIndex != 0)
        {
            boxColliders[0].enabled = true;
        }
    }

    public void DeactivateRightmostBox()
    {
        Debug.Log("deactivating rightmost");
        int rightMostIndex = 0;
        for (int i = 1; i < boxColliders.Length; i++)
        {
            if (boxColliders[i].offset.x > boxColliders[rightMostIndex].offset.x)
            {
                rightMostIndex = i;
            }
            else
            {
                boxColliders[i].enabled = true;
            }
        }
        boxColliders[rightMostIndex].enabled = false;
        if (rightMostIndex != 0)
        {
            boxColliders[0].enabled = true;
        }
    }
}
