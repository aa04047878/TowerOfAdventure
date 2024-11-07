using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAttrStrategy : IAttrStrategy
{
    public override void InitAttr(ICharacterAttr CharacterAttr)
    {
        PlayerCharacterAttr thePlayerCharacterAttr = CharacterAttr as PlayerCharacterAttr;
        if (thePlayerCharacterAttr == null)
        {
            return;
        }

        //取得現在的等級
        int Lv = thePlayerCharacterAttr.GetPlayerCharacterLv();

        #region 人類
        // 生命力有等級加乘
        int AddMaxHP = 0;       
        if (Lv > 1)
        {
            AddMaxHP = (Lv - 1) * 37;
        }   
        thePlayerCharacterAttr.SetAddMaxHP(AddMaxHP); 
        thePlayerCharacterAttr.SetNowMaxHP();  

        // 回復力有等級加乘
        int AddMaxRecover = 0;
        if (Lv > 1)
        {
            AddMaxRecover = (Lv - 1) * 2;
        }
        thePlayerCharacterAttr.SetAddMaxRecover(AddMaxRecover);
        thePlayerCharacterAttr.SetNowMaxRecover();

        //攻擊力有等級加成
        int AddMaxATK = 0;
        if (Lv > 1)
        {
            AddMaxATK = (Lv - 1) * 14;
        }
        thePlayerCharacterAttr.SetAddMaxATK(AddMaxATK);
        thePlayerCharacterAttr.SetNowMaxATK();
        #endregion

    }
}
