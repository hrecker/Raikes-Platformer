using System;
using UnityEngine;

public class SceneChanger: MonoBehaviour
{
	public int sceneToLoad;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Hitbox> () == null) {
			//We don't want the far away colliders that
			//spawn enemies to trigger the scene change.
			return;
		}

		GameObject parent = SceneMessenger.GetMainPlayerObject (other.gameObject);
		if (parent.tag == "Player") {
			SceneMessenger.Instance.LoadScene (sceneToLoad);
		}
	}

}

