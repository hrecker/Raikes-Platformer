using UnityEngine;

public class Turnbox : CollisionBox
{
    private float turnDelay = 1.0f;
    private float turnTimePassed;
    private bool triggerActive;

    void OnTriggerEnter2D(Collider2D other)
    {
        triggerActive = true;
        // Only turn on "Ground" collisions if the center of the ground is higher than the bottom of this collider
        if (active && objectMessenger != null && other.tag != "SpawnCollider" && 
            (other.tag != "Ground" || other.bounds.center.y >= GetComponent<Collider2D>().bounds.min.y))
        {
            objectMessenger.Invoke(Message.TURN, null);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(turnTimePassed >= turnDelay)
        {
            OnTriggerEnter2D(other);
            turnTimePassed = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        triggerActive = false;
        turnTimePassed = 0;
    }

    void Update()
    {
        if(triggerActive && turnTimePassed < turnDelay)
        {
            turnTimePassed += Time.deltaTime;
        }
    }

    public void DeactiveLeftmostBox()
    {
        //Debug.Log("deactivating leftmost");
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
        //Debug.Log("deactivating rightmost");
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
