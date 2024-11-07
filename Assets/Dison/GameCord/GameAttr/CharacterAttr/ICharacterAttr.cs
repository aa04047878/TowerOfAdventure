using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterAttr 
{
    protected BaseAttr m_BaseAttr = null;   // 基本角色數值
	protected IAttrStrategy m_AttrStrategy = null;// 數值的計算策略

	

	/// <summary>
	/// 設定基礎數值
	/// </summary>
	/// <param name="BaseAttr"></param>
	public void SetBaseAttr(BaseAttr BaseAttr)
	{
		m_BaseAttr = BaseAttr;
	}

	/// <summary>
	/// 設定數值的計算策略
	/// </summary>
	/// <param name="theAttrStrategy"></param>
	public void SetAttrStrategy(IAttrStrategy theAttrStrategy)
	{
		m_AttrStrategy = theAttrStrategy;
	}

	/// <summary>
	/// 初始化數值
	/// </summary>
	public virtual void InitAttr()
	{
		m_AttrStrategy.InitAttr(this);
		//FullNowHP();
	}

	public abstract int GetPlayerHP();

	public abstract int GetNowHP();
	public abstract int GetNowATK();

	public abstract int GetDamage();

	public abstract void SetDamage(int damage);

	/// <summary>
	/// 受到傷害
	/// </summary>
	/// <param name="damage"></param>
	public abstract void Hurt(int damage);
}
