using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedTowerBehaviour : MonoBehaviour {

	public float FireSpeed = 2.0f;
	
	public int FireDamage = 10;
	
	public int NumTargets = 1;
	
	public Object bulletPrefab;
	
	private float timer;
	
	private List<SimpleEnemyScript> colliders;
	
	
	void Start()
	{
		timer = FireSpeed;	
		colliders = new List<SimpleEnemyScript>();
	}
	
	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0 && colliders.Count > 0)
		{
			for (int i = 0; i < NumTargets && i < colliders.Count; i++)
			{
				SimpleEnemyScript ses = colliders[0];
				
				GameObject bullet = (GameObject)Instantiate(bulletPrefab);
				bullet.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				BulletBehaviour bb = (BulletBehaviour)bullet.GetComponent("BulletBehaviour");
				if (bb != null)
				{
					if (ses != null)bb.SetTarget(ses.gameObject);
					bb.Damage = FireDamage;
				}
				else Debug.Log("null");
		
				timer = FireSpeed;
			
			}
		}	
	}
	
	void OnTriggerEnter(Collider col)
	{
		SimpleEnemyScript ses = (SimpleEnemyScript) col.gameObject.GetComponent("SimpleEnemyScript");
		if ( ses != null)
			colliders.Add(ses);
		
		Debug.Log("hey");
		
	}
	
	void OnTriggerExit(Collider col)
	{
		SimpleEnemyScript ses = (SimpleEnemyScript) col.gameObject.GetComponent("SimpleEnemyScript");
		if ( ses != null)
			colliders.Remove(ses);	
		
	}
}
