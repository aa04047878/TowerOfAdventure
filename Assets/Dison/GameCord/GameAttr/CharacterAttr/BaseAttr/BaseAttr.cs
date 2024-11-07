using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttr
{
	public abstract int GetMaxHP();
	public abstract int GetMaxRecover();
	public abstract int GetMaxATK();
	
}

// 實作可以被共用的玩家角色基本數值
public class PlayerCharacterBaseAttr : BaseAttr
{
    private int m_MaxHP;  // 最高HP值
    private int m_Recover; //回復力
    private int m_ATK;     //攻擊力
    public PlayerCharacterBaseAttr(int MaxHP, int Recover, int ATK)
    {
        m_MaxHP = MaxHP;
        m_Recover = Recover;
        m_ATK = ATK;
    }

    public override int GetMaxHP()
    {
        return m_MaxHP;
    }

    public override int GetMaxRecover()
    {
        return m_Recover;
    }

    public override int GetMaxATK()
    {
        return m_ATK;
    }
}

// 實作可以被共用的敵人角色基本數值
public class EnemyCharacterBaseAttr : BaseAttr
{
    private int m_MaxHP;  // 最高HP值
    private int m_Recover; //回復力
    private int m_ATK;     //攻擊力

    public EnemyCharacterBaseAttr(int MaxHP, int Recover, int ATK)
    {
        m_MaxHP = MaxHP;
        m_Recover = Recover;
        m_ATK = ATK;
    }

    public override int GetMaxHP()
    {
        return m_MaxHP;
    }

    public override int GetMaxRecover()
    {
        return m_Recover;
    }

    public override int GetMaxATK()
    {
        return m_ATK;
    }
}