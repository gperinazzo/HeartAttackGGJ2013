using UnityEngine;
using System.Collections;

public class EdgeCollision : MonoBehaviour {

	void OnTriggerStay(Collider col)
	{
		Debug.Log ("Entrou edge");
		SimpleTowerBehaviour stb = (SimpleTowerBehaviour)col.gameObject.GetComponent("SimpleTowerBehaviour");
		if (stb != null)
		{
			
			Vector2 dist = new Vector2(transform.position.x, transform.position.y);
			float dis = dist.magnitude;
			dist.Normalize();
			dist *= (dis - 5);
			stb.SetMoveLocation(dist.x, dist.y, false);
		}

	}
	
	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("Colision: " + col.name);	
		
	}
	
	void OnCollisionEnter (Collision col)
	{
		
		Debug.Log ("Col: ");
	}
}
