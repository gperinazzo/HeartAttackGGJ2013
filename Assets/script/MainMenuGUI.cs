using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {
	
	public GUISkin MainMenu;
	public GUISkin Tutorial;
	
	private int position = 0;
	
	void OnGUI()
	{
		if (position == 0)
		{
			Vector2 tv = new Vector2(Screen.width/2 - 167.5f, Screen.height/2 - 101.5f);
			
			GUI.skin = MainMenu;
			
			GUI.Box (new Rect(tv.x, tv.y, 325.0f, 203.0f), "");
			if (GUI.Button (new Rect(tv.x + 76.0f, tv.y + 87.0f, 145.0f,  26.0f), ""))
			{
				Application.LoadLevel("main");
				
			}
			
			if (GUI.Button (new Rect(tv.x + 76.0f, tv.y + 125.0f, 204.0f,  26.0f), ""))
			{
				position = 1;
				
			}
			
			if (GUI.Button (new Rect(tv.x + 76.0f, tv.y + 162.0f, 96.0f,  26.0f), ""))
			{
				Application.Quit ();
				
			}
		}
		if (position == 1)
		{
			GUI.skin = Tutorial;
			GUI.Box (new Rect(0.0f, 0.0f, Screen.width, Screen.height), "");
			if (GUI.Button (new Rect(0.0f, 0.0f, Screen.width, Screen.height), ""))
			{
				position = 0;
			}
		
			if (Input.GetKeyDown(KeyCode.Escape))
				position = 0;
		}	
		
		
	}
}
