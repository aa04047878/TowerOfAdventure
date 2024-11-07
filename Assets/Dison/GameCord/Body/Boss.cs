using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	ILevelManager levelManager;
	void OnCollisionEnter(Collision hit)
	{
		//hit就是指碰撞到的Collision(打到的對方)
		//						碰撞到的物體的名稱
		Debug.Log("Enter : " + hit.collider.name);
		//Debug.Log("Enter : " + hit.transform.tag);

		levelManager.BossHurt(hit.collider.name);		
	}

	public void SetLevelManager(ILevelManager levelManager)
	{
		this.levelManager = levelManager;
	}
}
