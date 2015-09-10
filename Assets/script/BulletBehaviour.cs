using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
	
	
	public float MoveSpeed = 1.0f;
	public float TurnRate = 10.0f;
	
	public float rangeScale = 0.0f;
	
	public int Damage = 1 ;
	
	private float curAngle;
	
	private GameObject target = null;
	private float timer = 5.0f;
	

	// Use this for initialization
	void Start () {
		
		curAngle = 270.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if ( target != null)
		{
			float dx, dy;
			dx = target.transform.position.x - transform.position.x;
			dy = target.transform.position.y - transform.position.y;
			
			Vector2 move = new Vector2(dx, dy);
			Vector2 velocity = new Vector2(dx, dy).normalized * MoveSpeed;
			
			float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
	
			if (angle < 0.0f) angle += 360.0f;
			
			angle = 360.0f - angle;
			
			if (curAngle != angle)
			{
				Quaternion newQuat;
				if (angle - curAngle > 180.0f || angle - curAngle < 0.0f)
				{
					if (Mathf.Abs (angle - curAngle) > TurnRate)
					{
						curAngle -= 	TurnRate ;
						if (curAngle < 0.0f) curAngle += 360.0f;
						newQuat = Quaternion.AngleAxis ( -TurnRate , Vector3.up);
					}
					else
					{
						newQuat = Quaternion.AngleAxis ( -Mathf.Abs (angle - curAngle) , Vector3.up);
						curAngle = angle;
					}
				}
				else
				{
					if (Mathf.Abs (angle - curAngle) > TurnRate)
					{
						newQuat = Quaternion.AngleAxis (TurnRate, Vector3.up);
						curAngle += TurnRate ;
						if  (curAngle > 360.0f) curAngle -= 360.0f;
					}
					else
					{
						newQuat = Quaternion.AngleAxis ( Mathf.Abs (angle - curAngle) , Vector3.up);
						curAngle = angle;	
					}
				}	
				
				transform.rotation = transform.rotation * newQuat;
				
			}
			
			
			
			if (move.magnitude > velocity.magnitude)
				transform.position = new Vector3 (transform.position.x + velocity.x,transform.position.y + velocity.y, transform.position.z);
			else transform.position = target.transform.position;
			
			
		}
		else
		{
			Vector2 velocity = new Vector2(0.0f, 0.0f);
			velocity.x = MoveSpeed * Mathf.Cos(curAngle * Mathf.Deg2Rad);
			velocity.y = MoveSpeed * Mathf.Sin(curAngle * Mathf.Deg2Rad);
			transform.position = new Vector3 (transform.position.x + velocity.x ,transform.position.y + velocity.y, transform.position.z);
				
			timer -= Time.deltaTime;
				
			if (timer <= 0.0f)
				Destroy (transform.gameObject);
		}
	}
	
	public void SetTarget (GameObject obj){
		target = obj;
	}
	
	void OnTriggerEnter(Collider col)
	{
		SimpleEnemyScript ses = (SimpleEnemyScript)col.gameObject.GetComponent("SimpleEnemyScript");
		if (ses != null)
		{
			ses.doDamage(Damage);
			Destroy (transform.gameObject);
		}
		
	}
}