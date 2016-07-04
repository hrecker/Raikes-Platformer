using UnityEngine;

public class ProjectileDestroyHitbox : Hitbox
{
    public int framesToDestroy;
    private bool destroy;
    private int framesPassed;

    new void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "SpawnCollider")
        {
            base.OnTriggerEnter2D(other);
            destroy = true;
        }
    }

    void Update()
    {
        if(destroy)
        {
            framesPassed++;
            if(framesPassed >= framesToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
