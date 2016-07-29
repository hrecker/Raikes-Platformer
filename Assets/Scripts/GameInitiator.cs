using System;
using UnityEngine;

public class GameInitiator: MonoBehaviour
{
	
	public void StartGame()
	{
		int sceneToLoad = 0;
		if (PlayerPrefs.HasKey (SceneMessenger.currentSceneKey)) {
			sceneToLoad = PlayerPrefs.GetInt (SceneMessenger.currentSceneKey);
		}
		PlayerPrefs.SetInt (SceneMessenger.currentSceneKey, 0);
		UnityEngine.SceneManagement.SceneManager.LoadScene (sceneToLoad);
	}

	public void StartGameFromBeginning()
	{
		PlayerPrefs.SetInt (SceneMessenger.currentSceneKey, 0);
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}

}
