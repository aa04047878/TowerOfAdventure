using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttr : ICharacterAttr
{
	//最終角色數值存放處
	/// <summary>
	/// 玩家的HP
	/// </summary>
	protected int playerHP;

    #region 用不到的參數
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
    #endregion

    /// <summary>
    /// 設定玩家的HP
    /// </summary>
    /// <param name="playerHP"></param>
    public void SetPlayerHP(int playerHP)
    {
		this.playerHP = playerHP;
    }


    public override int GetPlayerHP()
    {
		return playerHP;

	}

    public override void Hurt(int damage)
    {
		playerHP -= damage;
		if (playerHP <= 0)
        {
			playerHP = 0;

		}
	}

    #region 繼承介面需要實作的部分(但是PlayerAttr不需要)
    /// <summary>
    /// 取得現在最大的HP
    /// </summary>
    /// <returns></returns>
    public override int GetNowHP()
	{
		return m_NowHP;
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
    #endregion
}
