using UnityEngine;
using System.Collections;

public class RangeCircleBehaviour : MonoBehaviour {

	private GameObject follow = null;	
	// Update is called once per frame
	void Update () {
		if (follow == null)
		{
			Destroy (transform.gameObject);
			return;
		}
		
		transform.position = follow.transform.position;
		
	}
	
	public void  SetParent(GameObject obj)
	{
		follow = obj;
	}
}
