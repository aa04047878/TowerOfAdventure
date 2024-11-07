using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrFactory : IAttrFactory
{
    private Dictionary<int, BaseAttr> m_PlayerCharacterAttrDB = null;
    private Dictionary<int, BaseAttr> m_EnemyCharacterAttrDB = null;

    public AttrFactory()
    {
        InitPlayerCharacterAttr();
        InitEnemyCharacterAttr();
    }

    /// <summary>
    /// 建立所有玩家角色的數值(1等)
    /// </summary>
    private void InitPlayerCharacterAttr()
    {
        m_PlayerCharacterAttrDB = new Dictionary<int, BaseAttr>();
                                                                  //HP, Recover, ATK
        m_PlayerCharacterAttrDB.Add(1, new PlayerCharacterBaseAttr(150, 150, 200)); //1 = Alice
        m_PlayerCharacterAttrDB.Add(2, new PlayerCharacterBaseAttr(170, 140, 190)); //2 = Rogritte
        m_PlayerCharacterAttrDB.Add(3, new PlayerCharacterBaseAttr(160, 200, 110)); //3 = Keli
        m_PlayerCharacterAttrDB.Add(4, new PlayerCharacterBaseAttr(200, 130, 220)); //4 = LonelySnow
        m_PlayerCharacterAttrDB.Add(5, new PlayerCharacterBaseAttr(190, 160, 180)); //5 = Yuna
    }

    /// <summary>
    /// 建立所有敵人角色的數值(1等)
    /// </summary>
    private void InitEnemyCharacterAttr()
    {
        m_EnemyCharacterAttrDB = new Dictionary<int, BaseAttr>();
                                                                //HP, Recover, ATK
        m_EnemyCharacterAttrDB.Add(1, new EnemyCharacterBaseAttr(7000, 0, 10));  //1 = SkeletonSoldier (骷髏士兵)
        m_EnemyCharacterAttrDB.Add(2, new EnemyCharacterBaseAttr(8000, 0, 15));  //2 = AngryScorpion (憤怒蠍子)
        m_EnemyCharacterAttrDB.Add(3, new EnemyCharacterBaseAttr(40000, 0, 60));  //3 = Tauren  (牛頭人)
    }

    /// <summary>
    /// 取得PlayerCharacter的數值
    /// </summary>
    /// <param name="AttrID"></param>
    /// <returns></returns>
    public override PlayerCharacterAttr GetPlayerCharacterAttr(int AttrID)
    {
        if (m_PlayerCharacterAttrDB.ContainsKey(AttrID) == false)
        {
            Debug.LogWarning("GetPlayerCharacterAttr:AttrID[" + AttrID + "]數值不存在");
            return null;
        }

        PlayerCharacterAttr newAttr = new PlayerCharacterAttr();
        newAttr.SetPlayerCharacterAttr(m_PlayerCharacterAttrDB[AttrID]);
        return newAttr;
    }


    public override PlayerAttr GetPlayerAttr()
    {
        PlayerAttr newAttr = new PlayerAttr(); //建立一個空素質，到時候初始化在寫素質邏輯
        return newAttr;
    }


    /// <summary>
    /// 取得敵人角色的數值
    /// </summary>
    /// <param name="AttrID"></param>
    /// <returns></returns>
    public override EnemyCharacterAttr GetEnemyCharacterAttr(int AttrID)
    {
        if (m_EnemyCharacterAttrDB.ContainsKey(AttrID) == false)
        {
            Debug.LogWarning("GetEnemyCharacterAttr:AttrID[" + AttrID + "]數值不存在");
            return null;
        }

        EnemyCharacterAttr newAttr = new EnemyCharacterAttr();
        newAttr.SetEnemyCharacterAttr(m_EnemyCharacterAttrDB[AttrID]);
        return newAttr;
    }
}
