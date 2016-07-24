using UnityEngine;

public class CherylMessenger : MonoBehaviour, IMessenger
{
    public Vector2 throwVelocity;
    private CherylFire fireComponent;
    private Health health;
	public int sceneToLoadOnDeath;
	public float loadSceneDelay = 1.0f;

    void Start()
    {
        fireComponent = GetComponent<CherylFire>();
        health = GetComponent<Health>();
    }

    public void Invoke(Message msg, object[] args)
    {
        switch(msg)
        {
			case Message.HIT_BY_OTHER:
				health.TakeDamage (1);
				SceneMessenger.Instance.Invoke (Message.BOSS_RECEIVED_HIT, null);
				if (health.health > 0)
				{
					Collider2D otherCollider = (Collider2D)args [2];
					otherCollider.GetComponentInParent<Rigidbody2D> ().velocity += throwVelocity;
					if (fireComponent.highLaserActive) {
						fireComponent.doubleInterval--;
					} else {
						fireComponent.highLaserActive = true;
					}
				}
				else
				{
					SceneMessenger.Instance.LoadSceneWithDelay (sceneToLoadOnDeath, loadSceneDelay);
				}
                break;
            case Message.NO_HEALTH_REMAINING:
                Destroy(gameObject);
                break;
        }
    }
}
