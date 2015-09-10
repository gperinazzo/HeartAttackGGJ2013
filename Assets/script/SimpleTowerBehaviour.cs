using UnityEngine;
using System.Collections;


public class SimpleTowerBehaviour : MonoBehaviour {
	
	
	public float MoveSpeed = 10.0f;
	public float TurnRate = 0.0f;
	
	public float rangeScale = 1.0f;
	
	private float curAngle;
	
	public bool isMoving = false;
	
	private bool flagMovement = false;
	
	private Vector3 moveLocation;
	
	// Use this for initialization
	void Start () {
		if (!flagMovement)
			moveLocation = new Vector3(transform.position.x , transform.position.y, transform.position.z);
		curAngle = 270.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( transform.position.x != moveLocation.x || transform.position.y != moveLocation.y)
		{
			if (flagMovement) isMoving  = true;
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
				isMoving = false;
			}
			
			if (transform.position.x < -22.5f) transform.position = new Vector3(-22.5f, transform.position.y, transform.position.z);  
			else if (transform.position.x > 22.5f) transform.position = new Vector3(22.5f, transform.position.y, transform.position.z);
			if (transform.position.y < -9.85f) transform.position = new Vector3(transform.position.x, -9.85f, transform.position.z);
			else if (transform.position.y > 9.85f) transform.position = new Vector3(transform.position.x, 9.85f, transform.position.z);
		
		
		}
	
	}
	public void SetMoveLocation (float x, float y, bool flagMoving = true){
		moveLocation = new Vector3(x, y, transform.position.z);	
		flagMovement = flagMoving; 
	}
	
}
