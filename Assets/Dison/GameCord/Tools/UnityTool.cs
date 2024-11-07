using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityTool 
{
	// 找到場景上的物件
	public static GameObject FindGameObject(string GameObjectName)
	{
		// 找出對應的GameObject
		GameObject tTmpGameObj = GameObject.Find(GameObjectName);
		if (tTmpGameObj == null)
		{
			Debug.LogWarning("場景中找不到GameObject[" + GameObjectName + "]物件");
			return null;
		}
		return tTmpGameObj;
	}

	// 取得子物件
	public static GameObject FindChildGameObject(GameObject Container, string gameobjectName)
	{
		//Container >> "要找尋的子物件"的父物件   gameobjectName >> 子物件名子
		if (Container == null)
		{
			Debug.LogError("NGUICustomTools.GetChild : Container =null");
			return null;
		}

		Transform tGameObjectTF = null; //= Container.transform.FindChild(gameobjectName);											


		// 是不是Container本身
		if (Container.name == gameobjectName)
			tGameObjectTF = Container.transform;
		else
		{
			// 找出所有子元件						
			Transform[] allChildren = Container.transform.GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren)
			{
				if (child.name == gameobjectName)
				{
					if (tGameObjectTF == null)
						tGameObjectTF = child;
					else
						Debug.LogWarning("Container[" + Container.name + "]下找出重覆的元件名稱[" + gameobjectName + "]");
				}
			}
		}

		// 都沒有找到
		if (tGameObjectTF == null)
		{
			Debug.LogError("元件[" + Container.name + "]找不到子元件[" + gameobjectName + "]");
			return null;
		}

		return tGameObjectTF.gameObject;
	}


}
