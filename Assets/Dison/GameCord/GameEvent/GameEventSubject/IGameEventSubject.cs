using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGameEventSubject //主題介面
{
    private List<IGameEventObserver> m_Observers = new List<IGameEventObserver>(); // 觀測者

	// 加入 (主題提供訂閱的方法)
	public void Attach(IGameEventObserver theObserver)
	{
		m_Observers.Add(theObserver);
	}

	// 取消 (主題提供取消訂閱的方法)
	public void Detach(IGameEventObserver theObserver)
	{
		m_Observers.Remove(theObserver);
	}

	// 通知 (通知所有訂閱者)
	public void Notify()
	{
		foreach (IGameEventObserver theObserver in m_Observers)
			theObserver.Update();
	}

	/// <summary>
	/// 設定參數
	/// </summary>
	public virtual void SetParam(int cardDataIndex, ENUM_Behavior behavior)
    {

    }
}
