using System;
using UnityEngine;

public class PowerupBlockMessenger: MonoBehaviour, IMessenger
{
	private SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Invoke(Message msg, object[] args) {
		switch (msg) {
		case Message.HIT_BY_OTHER:
			Debug.Log ("Powerup spawned!");
			break;
		}
	}
}

