using System;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene: MonoBehaviour
{
	private static float TextBubbleWidth = 128.0f;
	private GameObject player = null;
	public Text textObject;
	private bool running = false;
	public string[] textBubbles;
	public GameObject[] objects;
	public int[] textBubbleOwners;
	private int textBubbleIndex = 0;
	//If we only check the space for key up, it can trigger
	//during a jump (basically skipping the first text bubble).
	//If we verify that the space was pressed down during the
	//cutscene, we prevent that.
	private bool keyPressedDuringCutscene = false;
	public Texture2D texture;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			player = WarpPointMovement.GetMainPlayerObject (other.gameObject);
			StartCutscene ();
		}
	}

	public void StartCutscene()
	{
		//Stop physics simulation.
		Time.timeScale = 0.0f;
		/*
		Canvas c = GetComponentInChildren<Canvas> ();
//		c.transform.position = player.transform.position;
		Text t = Instantiate (textObject);
		t.text = "Cooper";
		t.rectTransform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		t.transform.SetParent (c.transform);
		*/
		running = true;
	}

	public void EndCutscene()
	{
		Time.timeScale = 1.0f;
		running = false;
		Destroy (gameObject);
	}

	public void Update()
	{
		if (running) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				keyPressedDuringCutscene = true;
			} else if (Input.GetKeyUp (KeyCode.Space) && keyPressedDuringCutscene) {
				keyPressedDuringCutscene = false;
				textBubbleIndex++;
				if (textBubbleIndex >= textBubbles.Length) {
					EndCutscene ();
				}
			}
		}
	}

	public void OnGUI()
	{
		if (running) {
			string text = textBubbles [textBubbleIndex];
			Bounds bounds = objects [textBubbleOwners [textBubbleIndex]].GetComponent<BoxCollider2D>().bounds;
			RenderTextAt (text, bounds);
		}
	}

	private void RenderTextAt(string text, Bounds atPosition)
	{
		if (Camera.current == null) {
			return;
		}
		Vector3 coords = Camera.current.WorldToScreenPoint(atPosition.center);
		coords.y = Screen.height - coords.y;

		GUIStyle style = new GUIStyle ();
		style.alignment = TextAnchor.UpperCenter;
		style.normal.textColor = Color.white;
		style.wordWrap = true;
		GUIContent content = new GUIContent (text);
		float textBubbleHeight = style.CalcHeight (content, TextBubbleWidth);
		float positionHeight = ScreenHeightOfBounds (atPosition);
		//Subtract the positionHeight, because screen coordinates start in the upper left corner.
		coords = new Vector3 (coords.x - TextBubbleWidth / 2.0f, coords.y - textBubbleHeight / 2.0f - positionHeight, coords.z);
		GUI.Label(new Rect (coords.x - 10.0f, coords.y - 10.0f, TextBubbleWidth + 20.0f, textBubbleHeight + 20.0f), texture, style);
		GUI.Label(new Rect (coords.x, coords.y, TextBubbleWidth, textBubbleHeight), content, style);
	}

	private float ScreenHeightOfBounds(Bounds bounds)
	{
		if (Camera.current == null) {
			return 0.0f;
		}
		Vector3 lower = Camera.current.WorldToScreenPoint(new Vector3(bounds.center.x, bounds.center.y - bounds.extents.y, bounds.center.z));
		Vector3 upper = Camera.current.WorldToScreenPoint(new Vector3(bounds.center.x, bounds.center.y + bounds.extents.y, bounds.center.z));
		return upper.y - lower.y;
	}

}

