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
        messenger.Invoke("HealthUpdated", new object[] { health, maxHealth });
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            if (messenger != null)
            {
                messenger.Invoke("NoHealthRemaining", null);
            }
        }
        if (messenger != null)
        {
            messenger.Invoke("HealthUpdated", new object[] { health, maxHealth });
            messenger.Invoke("HealthLost", null);
        }
    }
}