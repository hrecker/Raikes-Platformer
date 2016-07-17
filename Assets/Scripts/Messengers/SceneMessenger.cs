using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneMessenger : MonoBehaviour, IMessenger
{
    public static SceneMessenger Instance { get; private set; }
    private Dictionary<Message, List<Delegate>> callbacks;

    public delegate void HealthCallback (int currentHealth, int maxHealth, int currentArmor, int maxArmor);
	public delegate void PointsCallback (int gainedPoints);

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        if (callbacks == null)
        {
            callbacks = new Dictionary<Message, List<Delegate>>();
        }

    }

    public void Invoke(Message msg, object[] args)
    {
        if (callbacks.ContainsKey(msg))
        {
            switch(msg)
            {
                case Message.HEALTH_UPDATED:
                    foreach (Delegate callback in callbacks[msg])
                    {
                        callback.DynamicInvoke(args[0], args[1], args[2], args[3]);
                    }
                    break;
				case Message.POINTS_RECEIVED:
					foreach (Delegate callback in callbacks[msg])
					{
						callback.DynamicInvoke(args[0]);
					}
					break;
            }
        }
    }

    public void AddListener(Message msg, Delegate callback)
    {
        if(callbacks == null)
        {
            callbacks = new Dictionary<Message, List<Delegate>>();
        }
        if (!callbacks.ContainsKey(msg))
        {
            callbacks.Add(msg, new List<Delegate>());
        }
        callbacks[msg].Add(callback);
    }
		
	public static GameObject GetMainPlayerObject(GameObject obj)
	{
		//Some of the player's children can trigger the collision,
		//but we need the main player object so we can access its rigidbody object.
		while (obj.transform.parent != null) {
			obj = obj.transform.parent.gameObject;
		}
		return obj;
	}

}
