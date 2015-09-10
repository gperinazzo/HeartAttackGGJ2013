using UnityEngine;
using System.Collections;


public class GameLogic : MonoBehaviour {
	
	
	
	
	public int Money = 0;
	
	public float EnemyStartDistance = 50.0f;
	
	public int Score = 0;
	
	public AudioClip HeartBeat;
	
	public bool pause = false;
	
	
	public bool gameOver = false;
	
	private float timer = 5.0f;
	
	private int waves = 0;
	
	private HeartBehaviour hb = null;
	
	private Object[] towers;
	
	private Object[] monsters;
	
	
	// Use this for initialization
	void Start () {
		hb = (HeartBehaviour) GameObject.Find("Heart").GetComponent("HeartBehaviour");
		towers = new Object[2];
		towers[0] = Resources.Load ("MeleeTower");
		towers[1] = Resources.Load ("RangedTower");
		Random.seed = (int)System.DateTime.Now.Ticks;
		
		monsters = new Object[2];
		monsters[0] = Resources.Load ("SimpleEnemy");
		monsters[1] = Resources.Load ("Virus");
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hb.Health <= 0)
		{
			gameOver = true;
		}
		
		timer -= Time.deltaTime;
		
		if (timer <= 0.0f && !gameOver)
		{
			waves++;
			SpawnWave();
			
			timer = 10.0f + 0.1f * hb.Health;
		}
		
		
		
	}
	
	public bool BuyTower(int t)
	{
		Debug.Log ("buyTower " + t);
		
		int price = 0;
		Object blueprint = null;
		switch (t)
		{
			case 1:
				price = 10;
				blueprint = towers[0];
				break;
			case 2:
				price = 20;
				blueprint = towers[1];
				break;
		}
		
		if (Money < price || blueprint == null) return false;
		
		Money -= price;
		GameObject newTower = (GameObject) Instantiate(blueprint);
		newTower.transform.position = new Vector3(hb.gameObject.transform.position.x, hb.gameObject.transform.position.y, newTower.transform.position.z);
		Random.seed = (int)System.DateTime.Now.Ticks;
		Vector2 move = new Vector2(0.0f, 0.0f);
		float angle = Random.Range (0.0f, 360.0f) * Mathf.Deg2Rad;
		float dist = Random.Range (3.0f, 4.5f);
		
		Debug.Log (angle + " " + dist);
		
		move.x = dist * Mathf.Cos (angle);
		move.y = dist * Mathf.Sin (angle);
		
		Debug.Log (move.x + "  " + move.y);
		
		SimpleTowerBehaviour stb = (SimpleTowerBehaviour)newTower.GetComponent("SimpleTowerBehaviour");
		if (stb != null)
			stb.SetMoveLocation(newTower.transform.position.x + move.x, newTower.transform.position.y + move.y);
		else Debug.Log ("Error");
		return true;
	}
	
	private void SpawnWave()
	{
		float angle;
		float dist;
		int mob;
		
		AudioSource.PlayClipAtPoint(HeartBeat, Camera.main.transform.position);
		
		for(int i = 0; i < 3 + (waves /2); i++)
		{
			mob = (i + waves + Random.Range (0,1)) % 2;
			GameObject enemy = (GameObject) Instantiate(monsters[mob]);
			if (enemy == null)Debug.Log ("ahhhh");
			Vector2 pos = new Vector3(0.0f,0.0f);
			
			angle = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;
			dist = Random.Range(0.0f, 4.5f);
			pos.x = Mathf.Cos(angle) * (EnemyStartDistance + dist);
			pos.y = Mathf.Sin(angle) * (EnemyStartDistance + dist);
			
			enemy.transform.position = new Vector3(pos.x, pos.y, enemy.transform.position.z);
			
			SimpleEnemyScript ses = (SimpleEnemyScript)enemy.gameObject.GetComponent ("SimpleEnemyScript");
			if (ses != null)
				ses.SetMoveLocation(hb.gameObject.transform.position.x, hb.gameObject.transform.position.y);
			else Debug.Log ("error");
		}
	}
	
	public int GetHP()
	{
		return hb.Health;
	}
	
	
	public void SetPause(bool state)
	{
		if (state) Time.timeScale = 0.0f;
		else Time.timeScale = 1.0f;
		
		pause = state;
	
	
	}
}
