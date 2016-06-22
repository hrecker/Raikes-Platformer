using UnityEngine;

public class ProjectileDestroyHitbox : Hitbox
{
    new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Destroy(gameObject);
    }
}
