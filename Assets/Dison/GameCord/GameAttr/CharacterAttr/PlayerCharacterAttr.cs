using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAttr : ICharacterAttr
{
	//最終角色數值存放處
	/// <summary>
	/// 角色等級
	/// </summary>
	protected int m_PlayerCharacterLv;

	/// <summary>
	/// 額外增加的HP
	/// </summary>
	protected int m_AddMaxHP;  

	/// <summary>
	/// 現在的HP
	/// </summary>
	protected int m_NowHP; 

	/// <summary>
	/// 額外增加的回覆力
	/// </summary>
	protected int m_AddMaxRecover;

	/// <summary>
	/// 現在的回覆力
	/// </summary>
	protected int m_NowRecover;

	/// <summary>
	/// 額外增加的攻擊力
	/// </summary>
	protected int m_AddMaxATK;

	/// <summary>
	/// 現在的攻擊力
	/// </summary>
	protected int m_NowATK;

	/// <summary>
	/// 對怪物造成的傷害
	/// </summary>
	protected int m_Damage;

	public void SetPlayerCharacterAttr(BaseAttr BaseAttr)  //這裡的等級之後會用到
	{
		// 共用元件
		SetBaseAttr(BaseAttr);

		// 外部參數
		m_PlayerCharacterLv = 1;
		m_AddMaxHP = 0;
	}

	/// <summary>
	/// 設定玩家角色等級
	/// </summary>
	/// <param name="Lv"></param>
	public void SetPlayerCharacterLv(int Lv)
	{
		m_PlayerCharacterLv = Lv;
	}

	/// <summary>
	/// 取得玩家角色等級
	/// </summary>
	/// <returns></returns>
	public int GetPlayerCharacterLv()
	{
		return m_PlayerCharacterLv;
	}

	/// <summary>
	/// 設定額外增加的HP
	/// </summary>
	/// <param name="AddMaxHP"></param>
	public void SetAddMaxHP(int AddMaxHP)
	{
		m_AddMaxHP = AddMaxHP;
	}

	/// <summary>
	/// 設定現在最大的HP
	/// </summary>
	public void SetNowMaxHP()
    {
		m_NowHP = m_BaseAttr.GetMaxHP() + m_AddMaxHP;
	}

	/// <summary>
	/// 取得現在最大的HP
	/// </summary>
	/// <returns></returns>
	public override int GetNowHP()
    {		
		return m_NowHP;
	}


    public override int GetPlayerHP()
    {
		return 0;
    }

    /// <summary>
    /// 設定額外增加的Recover
    /// </summary>
    /// <param name="AddMaxHP"></param>
    public void SetAddMaxRecover(int AddMaxRecover)
    {
		m_AddMaxRecover = AddMaxRecover;
	}

	/// <summary>
	/// 設定現在最大的回覆力
	/// </summary>
	public void SetNowMaxRecover()
    {
		m_NowRecover = m_BaseAttr.GetMaxRecover() + m_AddMaxRecover;
	}

	/// <summary>
	/// 取得現在的回覆力
	/// </summary>
	/// <returns></returns>
	public int GetNowRecover()
    {		
		return m_NowRecover;
	}

	/// <summary>
	/// 設定額外增加的攻擊力
	/// </summary>
	/// <param name="AddMaxATK"></param>
	public void SetAddMaxATK(int AddMaxATK)
    {
		m_AddMaxATK = AddMaxATK;
	}

	/// <summary>
	/// 設定現在最大的攻擊力
	/// </summary>
	public void SetNowMaxATK()
	{
		m_NowATK = m_BaseAttr.GetMaxATK() + m_AddMaxATK;
	}

	/// <summary>
	/// 取得現在的攻擊力
	/// </summary>
	public override int GetNowATK()
	{
		return m_NowATK;
	}

	/// <summary>
	/// 設定對怪物造成的傷害
	/// </summary>
	public override void SetDamage(int damage)
	{
		m_Damage = damage;
	}

	/// <summary>
	/// 取得對怪物造成的傷害
	/// </summary>
	/// <returns></returns>
	public override int GetDamage()
    {
		return m_Damage;

	}

    public override void Hurt(int damage)
    {
        //玩家受到傷害不是寫在這裡。
    }
}
