using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonster1 : MonoBehaviour
{
	ILevelManager levelManager;
	void OnCollisionEnter(Collision hit)
	{
		//hit就是指碰撞到的Collision(打到的對方)
		//						碰撞到的物體的名稱
		Debug.Log("Enter : " + hit.collider.name);

		//if (hit.collider.name == "PlayerChAtkVfx1")
		//      {
		//	//改變UI的血量
		//      }
		levelManager.EnemyCh01Hurt(hit.collider.name);
	}

	public void SetLevelManager(ILevelManager levelManager)
    {
		this.levelManager = levelManager;
    }
}
