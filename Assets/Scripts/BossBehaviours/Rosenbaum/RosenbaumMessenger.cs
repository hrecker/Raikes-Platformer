using System;
using UnityEngine;

public class RosenbaumMessenger : MonoBehaviour, IMessenger
{
    public RosenbaumUIController uiController;
    public Health health;

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.HEALTH_UPDATED:
                uiController.SetBossHealthWidth((int)args[0], (int)args[1]);
                break;
            case Message.NO_HEALTH_REMAINING:
                Destroy(gameObject);
                break;
            case Message.START_ATTACK_DELAY:
                Debug.Log("start attack delay");
                uiController.ActivateWarning((float)args[0]);
                break;
            case Message.HIT_BY_OTHER:
                health.TakeDamage(1);
                break;
        }
    }
}
