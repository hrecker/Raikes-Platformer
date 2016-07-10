using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour 
{
	// Load level one
	public void StartGame () 
	{
		Application.LoadLevel ("level-1");
	}
	
	// Quit game
	public void Exit () 
	{
		Application.Quit();
	}
}
