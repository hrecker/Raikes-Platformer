using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;

    private IMessenger messenger;

    void Start()
    {
        health = maxHealth;
        messenger = GetComponent<IMessenger>();
        messenger.Invoke(Message.HEALTH_UPDATED, new object[] { health, maxHealth });
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            if (messenger != null)
            {
                messenger.Invoke(Message.NO_HEALTH_REMAINING, null);
            }
        }
        if (messenger != null)
        {
            messenger.Invoke(Message.HEALTH_UPDATED, new object[] { health, maxHealth });
            messenger.Invoke(Message.HEALTH_LOST, null);
        }
    }
}