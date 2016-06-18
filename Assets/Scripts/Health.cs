using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int armor;
    public int maxArmor;

    private IMessenger messenger;

    void Start()
    {
        health = maxHealth;
        messenger = GetComponent<IMessenger>();
        //messenger.Invoke(Message.HEALTH_UPDATED, new object[] { health, maxHealth, armor, maxArmor });
    }

    public void TakeDamage(int damage)
    {
        if (armor > 0)
        {
            armor -= damage;
            if (armor <= 0)
            {
                armor = 0;
                if (messenger != null)
                {
                    messenger.Invoke(Message.NO_ARMOR_REMAINING, null);
                }
            }
        }
        else
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
        }
        if (messenger != null)
        {
            messenger.Invoke(Message.HEALTH_UPDATED, new object[] { health, maxHealth, armor, maxArmor });
            messenger.Invoke(Message.HEALTH_LOST, null);
        }
    }

    public bool IncreaseHealth(int increaseVal)
    {
        if(health == maxHealth)
        {
            return false;
        }

        health += increaseVal;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if (messenger != null)
        {
            messenger.Invoke(Message.HEALTH_UPDATED, new object[] { health, maxHealth, armor, maxArmor });
        }
        return true;
    }

    public bool IncreaseArmor(int increaseVal)
    {
        if (armor == maxArmor)
        {
            return false;
        }

        armor += maxArmor;
        if (armor > maxArmor)
        {
            armor = maxArmor;
        }
        return true;
    }
}