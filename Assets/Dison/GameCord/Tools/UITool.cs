using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UITool 
{
    private static GameObject m_CanvasObj = null; // 場景上的2D畫布物件

	// 找尋限定在Canvas畫布下的UI界面
	public static GameObject FindUIGameObject(string UIName)
	{
		if (m_CanvasObj == null)
			m_CanvasObj = UnityTool.FindGameObject("Canvas");
		if (m_CanvasObj == null)
			return null;
		return UnityTool.FindChildGameObject(m_CanvasObj, UIName);
	}

	// 取得UI元件
	public static T GetUIComponent<T>(GameObject Container, string UIName) where T : UnityEngine.Component
	{
		// 找出子物件 
		GameObject ChildGameObject = UnityTool.FindChildGameObject(Container, UIName);
		if (ChildGameObject == null)
			return null;

		T tempObj = ChildGameObject.GetComponent<T>();
		if (tempObj == null)
		{
			Debug.LogWarning("元件[" + UIName + "]不是[" + typeof(T) + "]");
			return null;
		}
		return tempObj;
	}

	// 取得UI元件
	public static T GetUIComponent<T>(string UIName) where T : UnityEngine.Component
	{
		// 取得Canvas
		GameObject UIRoot = GameObject.Find("Canvas");
		if (UIRoot == null)
		{
			Debug.LogWarning("場景上沒有UI Canvas");
			return null;
		}
		return GetUIComponent<T>(UIRoot, UIName);
	}

	//------------------------------------------以下為自己寫的-------------------------------------

	public static T FindHiddenChildComponent<T>(string parentName, string childName)
    {
		GameObject parentObject = GameObject.Find(parentName);
		GameObject ChildObject = parentObject.transform.Find(childName).gameObject;
		return ChildObject.GetComponent<T>();
	}


	public static GameObject FindHiddenChildObject(string parentName, string childName)
	{
		GameObject parentObject = GameObject.Find(parentName);
		return parentObject.transform.Find(childName).gameObject;
	}

	public static T FindGameComponent<T>(string objectName) 
    {
		GameObject ObjectName = GameObject.Find(objectName);
		return ObjectName.GetComponent<T>();
	}

	public static T FindChildObjectComponent<T>(string parentName, string childName) where T : UnityEngine.Component
    {
		GameObject parentObject = GameObject.Find(parentName);
		GameObject ChildObject = parentObject.transform.Find(childName).gameObject;
		return ChildObject.GetComponent<T>();
	}

	public static T FindChildChildObjectComponent<T>(string parentName, string childName, string childchildName) where T : UnityEngine.Component
	{
		GameObject parentObject = GameObject.Find(parentName);
		GameObject ChildObject = parentObject.transform.Find(childName).gameObject;
		GameObject childchildObject = ChildObject.transform.Find(childchildName).gameObject;
		return childchildObject.GetComponent<T>();
	}
}
