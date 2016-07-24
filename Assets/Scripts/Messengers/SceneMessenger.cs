using System;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersistence
{
	public int health;
	public int armor;
	public List<PickupType> activePowerups;
	public int points;

	public ScenePersistence(int health, int armor, List<PickupType> activePowerups, int points)
	{
		this.health = health;
		this.armor = armor;
		this.activePowerups = activePowerups;
		this.points = points;
	}

}

public class SceneMessenger : MonoBehaviour, IMessenger
{
    public static SceneMessenger Instance { get; private set; }
    private Dictionary<Message, List<Delegate>> callbacks;

    public delegate void HealthCallback (int currentHealth, int maxHealth, int currentArmor, int maxArmor);
	public delegate void PointsCallback (int gainedPoints);
    public delegate void VoidCallback ();

	private static ScenePersistence playerData = null;

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
                case Message.BOSS_RECEIVED_HIT:
                    foreach(Delegate callback in callbacks[msg])
                    {
                        callback.DynamicInvoke();
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

	public void LoadScene(int sceneToLoad)
	{
		SavePlayerState ();
		UnityEngine.SceneManagement.SceneManager.LoadScene (sceneToLoad);
	}

	private System.Collections.IEnumerator LoadSceneAfterDelay(int sceneToLoad, float seconds)
	{
		yield return new WaitForSeconds (seconds);
		LoadScene (sceneToLoad);
	}

	public void LoadSceneWithDelay(int sceneToLoad, float seconds)
	{
		StartCoroutine (LoadSceneAfterDelay (sceneToLoad, seconds));
	}

	public void SavePlayerState()
	{
		GameObject obj = SceneMessenger.GetMainPlayerObject (GameObject.FindWithTag ("Player"));
		int health = obj.GetComponent<Health> ().health;
		int armor = obj.GetComponent<Health> ().armor;
		List<PickupType> activePickups = obj.GetComponent<PickupController> ().GetActivePowerups ();
		int points = GameObject.FindGameObjectWithTag ("UIController").GetComponent<UIController> ().Points;
		SceneMessenger.playerData = new ScenePersistence (health, armor, activePickups, points);
	}

	public void LoadPlayerState()
	{
		GameObject obj = SceneMessenger.GetMainPlayerObject (GameObject.FindWithTag ("Player"));
		if (obj != null && SceneMessenger.playerData != null) {
			int health = SceneMessenger.playerData.health;
			int armor = SceneMessenger.playerData.armor;
			int points = SceneMessenger.playerData.points;
			int maxHealth = obj.GetComponent<Health> ().maxHealth;
			int maxArmor = obj.GetComponent<Health> ().maxArmor;
			obj.GetComponent<Health> ().health = health;
			obj.GetComponent<Health> ().armor = armor;
			obj.GetComponent<PickupController> ().SetActivePowerups (SceneMessenger.playerData.activePowerups);
			GameObject.FindGameObjectWithTag ("UIController").GetComponent<UIController> ().Points = points;

			Invoke (Message.HEALTH_UPDATED, new object[] { health, maxHealth, armor, maxArmor });
			Invoke (Message.POINTS_RECEIVED, new object[] { 0 });

			SceneMessenger.playerData = null;
		}
	}

}
