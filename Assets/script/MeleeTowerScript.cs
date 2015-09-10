using UnityEngine;
using System.Collections;

public class MeleeTowerScript : MonoBehaviour {
	
	public float FireRate = 0.4f;
	public int Damage = 4;
	public float AttackRadius = 2.0f;
	
	
	private GameObject target = null;
	
	private SimpleTowerBehaviour mySTB;
	private float timer;
	private bool attacked = false;
	private bool canAttack = true;
	private bool hadTarget = false;
	
	// Use this for initialization
	void Start () {
		mySTB = (SimpleTowerBehaviour) transform.gameObject.GetComponent("SimpleTowerBehaviour");
	}
	
	// Update is called once per frame
	void Update () {
		if (attacked == true)
		{
			canAttack = false;
			timer = FireRate;
			attacked = false;
		}
		else
		{
			timer -= Time.deltaTime;
			if (timer <= 0.0f)
				canAttack = true;
		}
		
		if (target != null && !mySTB.isMoving)
		{
			hadTarget = true;
			Vector3 dist = transform.position - target.transform.position;
			
			if (dist.magnitude > 0.5f && target.transform.position.z > -10.0f)
			{
				mySTB.SetMoveLocation(target.transform.position.x, target.transform.position.y,false );
			}
			else mySTB.SetMoveLocation(transform.position.x, transform.position.y, false);
		}
		else if (hadTarget == true && !mySTB.isMoving)
		{
			hadTarget =false;
			mySTB.SetMoveLocation(transform.position.x, transform.position.y, false);
		}
	}
	
	void OnTriggerEnter (Collider col)
	{
		if (target == null)
			if (col.gameObject.GetComponent("SimpleEnemyScript") != null)
				target = col.gameObject;
	}
	
	void OnTriggerStay(Collider col)
	{
		SimpleEnemyScript ses = (SimpleEnemyScript)col.gameObject.GetComponent("SimpleEnemyScript");
		if (ses != null)
		{
			if (target == null) target = col.gameObject;
			
			if (!canAttack) return;
		
			Vector3 dist = transform.position - col.transform.position;
			if (dist.magnitude <= AttackRadius)
			{
				ses.doDamage(Damage);
				attacked = true;
				
			}
			
		}
		
	}
}
