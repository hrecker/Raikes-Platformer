using UnityEngine;

public class PlayerMessenger : MonoBehaviour, IMessenger
{
    public float invincibilityTime; // Time that player is invincible after getting hit

    private bool invulnerable;
    private float currentTimePassed;
    private Hurtbox[] hurtboxes;
    private SpriteAlternator invincibilityAlternator;

    void Start()
    {
        invincibilityAlternator = GetComponent<SpriteAlternator>();
        invincibilityAlternator.SetActive(false);
        hurtboxes = GetComponentsInChildren<Hurtbox>();
        invulnerable = false;
    }

    public void Invoke(string msg, object[] args)
    {
        switch(msg)
        {
            case "HitOther":
                Debug.Log("Player hit another object");
                break;
            case "HitByOther":
                Debug.Log("Player received hit");
                makeInvulnerable();
                break;
        }
    }

    void Update()
    {
        if (invulnerable)
        {
            currentTimePassed += Time.deltaTime;
            if(currentTimePassed >= invincibilityTime)
            {
                makeVulnerable();
            }
        }
    }

    private void makeInvulnerable()
    {
        currentTimePassed = 0;
        invulnerable = true;
        foreach(Hurtbox hurtbox in hurtboxes)
        {
            hurtbox.Deactivate();
        }
        invincibilityAlternator.SetActive(true);
    }

    private void makeVulnerable()
    {
        currentTimePassed = 0;
        invulnerable = false;
        foreach (Hurtbox hurtbox in hurtboxes)
        {
            hurtbox.Activate();
        }
        invincibilityAlternator.SetActive(false);
    }
}
