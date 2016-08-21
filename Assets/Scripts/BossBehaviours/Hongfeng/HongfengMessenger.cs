using System;
using UnityEngine;

public class HongfengMessenger : MonoBehaviour, IMessenger
{
    public float rainDuration;
    public float waitDuration;
    public DatFileRain datFileRain;
    public float moveSpeedMultiplier;
    public float rainDurationMultiplier;
    public float turnChanceAdd; // how much to add to turn chance after each hit
    public int sceneToLoadOnDeath;
    public float loadSceneDelay = 1.0f;

    private float currentTimePassed;
    private bool waiting;
    private HongfengMovement movement;
    private Health health;

    void Awake()
    {
        movement = GetComponent<HongfengMovement>();
        health = GetComponent<Health>();
    }

    void Start()
    {
        movement.StartMovement();
        datFileRain.StartRain();
    }

    void Update()
    {
        currentTimePassed += Time.deltaTime;
        if(!waiting && currentTimePassed >= rainDuration)
        {
            stopAttack();
        }
        else if(waiting && currentTimePassed >= waitDuration)
        {
            startAttack();
        }

    }

    private void stopAttack()
    {
        datFileRain.StopRain();
        movement.StopMovement();
        currentTimePassed = 0;
        waiting = true;
    }

    private void startAttack()
    {
        waiting = false;
        movement.StartMovement();
        datFileRain.StartRain();
        currentTimePassed = 0;
    }

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.NO_HEALTH_REMAINING:
                SceneMessenger.Instance.LoadSceneWithDelay(sceneToLoadOnDeath, loadSceneDelay);
                Destroy(gameObject);
                break;
            case Message.HIT_BY_OTHER:
                health.TakeDamage(1);
                if(health.health > 0)
                {
                    movement.moveSpeed *= moveSpeedMultiplier;
                    rainDuration *= rainDurationMultiplier;
                    datFileRain.turnChance += turnChanceAdd;
                    startAttack();
                }
                break;
        }
    }
}
