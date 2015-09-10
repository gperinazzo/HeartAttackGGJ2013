using UnityEngine;
using System.Collections;

public class HeartBehaviour : MonoBehaviour {

	
	public int Health = 100;
	
	private int stage = 1;
	
	public void DoDamage(int dmg)
	{
		Health -= dmg;
		if (Health <= 100 - (stage * 20))
		{
			stage++;
			if (stage <= 5)
				renderer.material.mainTextureOffset = new Vector2(stage * 0.2f, 0.0f);
			
		}
		
	}
	
}
