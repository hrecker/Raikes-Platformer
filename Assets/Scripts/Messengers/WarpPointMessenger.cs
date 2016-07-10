using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpPointMessenger: MonoBehaviour, IMessenger
{

	private WarpPointMovement movement;

	public void Start()
	{
		movement = GetComponent<WarpPointMovement> ();
	}

	public void Invoke(Message msg, object[] args) {
		switch (msg) {
		case Message.WARPED:
			SceneManager.LoadScene ((int)args[0]);
			break;
		default:
			break;
		}
	}
}

