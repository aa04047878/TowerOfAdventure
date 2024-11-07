using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterAttrStrategy : IAttrStrategy
{
    public override void InitAttr(ICharacterAttr CharacterAttr)
    {
        EnemyCharacterAttr theEnemyCharacterAttr = CharacterAttr as EnemyCharacterAttr;
        if (theEnemyCharacterAttr == null)
        {
            return;
        }

        //敵人現在的等級
        int Lv = theEnemyCharacterAttr.GetEnemyCharacterLv();

        #region 計算怪物素質
        // 生命力有等級加乘
        int AddMaxHP = 0;
        if (Lv > 1)
        {
            AddMaxHP = (Lv - 1) * 1500 * Lv;
        }
        theEnemyCharacterAttr.SetAddMaxHP(AddMaxHP);
        theEnemyCharacterAttr.SetNowMaxHP();

        // 回復力無等級加乘，不用計算(怪物無回復力)

        // 攻擊力有等級加成
        int AddMaxATK = 0;
        if (Lv > 1)
        {
            AddMaxATK = (Lv - 1) * 2;
        }
        theEnemyCharacterAttr.SetAddMaxATK(AddMaxATK);
        theEnemyCharacterAttr.SetNowMaxATK();

        // 對玩家造成多少傷害
        int damage = 0;
        damage = theEnemyCharacterAttr.GetNowATK() * Lv;
        theEnemyCharacterAttr.SetDamage(damage);
        #endregion
    }
}
