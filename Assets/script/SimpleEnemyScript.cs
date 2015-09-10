using UnityEngine;
using System.Collections;

public class SimpleEnemyScript : MonoBehaviour {

	public int Health = 10;
	public int Damage = 1;
	public int Blood = 1;
	public int Score = 1;
	public float MoveSpeed = 10.0f;
	public float TurnRate = 10.0f;
	
	
	
	
	private float curAngle;
	
	private Vector3 moveLocation;
	private bool dead = false;
	
	// Use this for initialization
	void Start () {
		curAngle = 270.0f;
		//if (moveLocation == null) moveLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (dead) return;
		
		if ( transform.position.x != moveLocation.x || transform.position.y != moveLocation.y)
		{
			float dx, dy;
			dx = moveLocation.x - transform.position.x;
			dy = moveLocation.y - transform.position.y;
			
			Vector2 move = new Vector2(dx, dy);
			Vector2 velocity = new Vector2(dx, dy).normalized * MoveSpeed * Time.deltaTime;
			
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
			else
			{
				transform.position = moveLocation;
			}
		}
	}
	
	public void SetMoveLocation (float x, float y){
		moveLocation = new Vector3(x, y, transform.position.z);	

	}
	
	
	void OnTriggerEnter(Collider col)
	{
		if (col.name == "Heart")
		{
			HeartBehaviour hb = (HeartBehaviour) col.gameObject.GetComponent("HeartBehaviour");
			hb.DoDamage(Damage);
			
			die ();
		}
	}
	
	
	public bool doDamage(int dmg)
	{
		Health -= dmg;
		if (Health <= 0)
		{
			GameObject go = (GameObject)GameObject.Find("GameLogic");
			GameLogic gl = (GameLogic) go.GetComponent("GameLogic");
		
			gl.Money += Blood;
			gl.Score += Score;
			die ();	
			return true;
		}
		return false;
	}
	
	private void die ()
	{	
		transform.position = new Vector3 (0.0f, 0.0f, -50.0f);
		dead = true;
	}
	
	void OnTriggerExit(Collider col)
	{
		
		if (dead) Destroy(transform.gameObject);

	}
	
	public bool isDead()
	{
		return dead;	
	}
}