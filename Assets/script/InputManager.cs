using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	private SimpleTowerBehaviour selected = null;
	private GameObject circle = null;
	
	private GameLogic gl;
	
	// Use this for initialization
	void Start () {
		GameObject go = (GameObject)GameObject.Find("GameLogic");
		gl = (GameLogic) go.GetComponent("GameLogic");
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			{
				
  				if (hit.transform.gameObject.GetComponent("SimpleTowerBehaviour") != null)
				{
					selected = (SimpleTowerBehaviour)hit.transform.gameObject.GetComponent("SimpleTowerBehaviour");
					if (circle == null) circle = (GameObject)Instantiate(Resources.Load ("RangeCircle"));
					RangeCircleBehaviour rcb = (RangeCircleBehaviour)circle.GetComponent ("RangeCircleBehaviour");
					rcb.SetParent(selected.gameObject);
					circle.transform.localScale = new Vector3(selected.rangeScale, selected.rangeScale, 1);
						
				}
		
			}
			else
			{
				if (circle != null) Destroy (circle);
				selected = null;
			}
		}
		
		if (Input.GetMouseButtonDown(1) && selected != null)
		{
			Vector3 clicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			selected.SetMoveLocation(clicked.x, clicked.y);	
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			gl.BuyTower(1);	
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			gl.BuyTower(2);	
		}
	
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log ("pause");
			gl.SetPause(!gl.pause);
		}
	}
}
