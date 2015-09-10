using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GUISkin TowerMenuSkin;
	
	public GUISkin PointsSkin;
	
	public GUISkin HPskin;
	
	public GUISkin PauseMenu;
	
	public GUISkin GameOverMenu;
	
	
	
	private GameLogic gl;
	
	void Start()
	{
		GameObject go = (GameObject)GameObject.Find("GameLogic");
		gl = (GameLogic) go.GetComponent("GameLogic");
	}
	
	void OnGUI()
	{
		GUI.skin = TowerMenuSkin;
		
		GUI.Box(new Rect(10, 10, 75, 133), 	"");
		if (GUI.Button (new Rect (19, 18, 50, 50), ""))
		{
			gl.BuyTower(1);
		}
		
		if (GUI.Button (new Rect (19, 78, 50, 50), ""))
		{
			gl.BuyTower (2);	
		}
		
		GUI.skin = HPskin;
		
		GUI.Box (new Rect(43,Screen.height - 30, gl.GetHP(), 16), "" );
		
		
		GUI.skin = PointsSkin;
		
		GUI.Box(new Rect(0, Screen.height - 50, 500, 50), "");
		GUI.TextField(new Rect(236, Screen.height - 30, 56, 15), "" +gl.Money);
		GUI.TextField(new Rect(382, Screen.height - 30, 56, 15), "" +gl.Score);
		
		
		if (gl.gameOver)
		{
			Vector2 tv = new Vector2(Screen.width/2 - 150.0f, Screen.height/2 - 85.5f);
			GUI.skin = GameOverMenu;
			
			GUI.Box(new Rect(tv.x, tv.y, 300.0f, 171.0f), "");
			if (GUI.Button (new Rect(tv.x + 75.0f, tv.y + 88.0f, 140.0f  , 28.0f ),""))
			{
				Application.LoadLevel("main");	
			}
			
			if (GUI.Button (new Rect(tv.x + 75.0f, tv.y + 125.0f, 95.0f  , 28.0f ),""))
			{
				Application.Quit ();	
			}
		}
		else if (gl.pause)
		{
			Vector2 tv = new Vector2(Screen.width/2 - 162.5f, Screen.height/2 - 101.5f);
			GUI.skin = PauseMenu;
			
			GUI.Box(new Rect(tv.x, tv.y, 325.0f, 203.0f), "");
			if (GUI.Button (new Rect(tv.x + 75.0f, tv.y + 88.0f, 202.0f  , 25.0f ),""))
			{
				gl.SetPause(false);
			}
			
			if (GUI.Button (new Rect(tv.x + 75.0f, tv.y + 125.0f, 200.0f  , 25.0f ),""))
			{
				Application.LoadLevel("main");	
			}
			
			if (GUI.Button (new Rect(tv.x + 75.0f, tv.y + 162.0f, 95.0f  , 25.0f ),""))
			{
				Application.Quit ();	
			}
			
			
			
		}
		
	}
}
