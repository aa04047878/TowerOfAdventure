using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_GameEvent
{
    None,
	AddBackpackContent,
}

public enum ENUM_Behavior
{
	None,
	DrawCard,
	Loading
}

public class GameEventSystem : IGameSystem
{
    public GameEventSystem(TowerOfAdventureGame TOAGame):base(TOAGame)
    {

    }

    //用字典來管理遊戲事件
    private Dictionary<ENUM_GameEvent, IGameEventSubject> m_GameEvents = new Dictionary<ENUM_GameEvent, IGameEventSubject>();

	// 替某一主題註冊一個觀測者  (針對一個遊戲事件產生一個對應主題後，加入觀察者(讓觀察者可以訂閱主題的方法))
	public void RegisterObserver(ENUM_GameEvent emGameEvnet, IGameEventObserver Observer)
	{
		// 取得事件
		IGameEventSubject Subject = GetGameEventSubject(emGameEvnet);
		if (Subject != null)
		{
			Subject.Attach(Observer);
			Observer.SetSubject(Subject);
		}
	}

	// 註冊一個事件 (針對一個遊戲事件產生一個對應主題)
	private IGameEventSubject GetGameEventSubject(ENUM_GameEvent emGameEvnet)
    {
		// 是否已經存在
		if (m_GameEvents.ContainsKey(emGameEvnet))
			return m_GameEvents[emGameEvnet];

		IGameEventSubject pSujbect = null;
		switch (emGameEvnet)
        {
			case ENUM_GameEvent.AddBackpackContent:
				pSujbect = new AddBackpackContentSubject();
				break;
			default:
				Debug.LogWarning("還沒有針對[" + emGameEvnet + "]指定要產生的Subject類別");
				return null;
		}

		// 加入後並回傳
		m_GameEvents.Add(emGameEvnet, pSujbect);
		return pSujbect;
	}
	
	/// <summary>
	/// 通知一個GameEvent更新 (通知主題設定參數(改變主題內容))
	/// </summary>
	/// <param name="emGameEvnet"></param>
	public void NotifySubject(ENUM_GameEvent emGameEvnet, int cardDataIndex, ENUM_Behavior behavior)
	{
		// 是否存在
		if (m_GameEvents.ContainsKey(emGameEvnet) == false)
			return;
		//Debug.Log("SubjectAddCount["+emGameEvnet+"]");
		m_GameEvents[emGameEvnet].SetParam(cardDataIndex, behavior);
	}

	
}
