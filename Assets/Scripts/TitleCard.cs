using System;
using UnityEngine;

public class TitleCard: MonoBehaviour
{
	private float time = 0.0f;
	public float visibleDuration = 1.5f;

	void Update()
	{
		time += Time.deltaTime;
		if (time >= visibleDuration) {
			Destroy (gameObject);
		}
	}
}

