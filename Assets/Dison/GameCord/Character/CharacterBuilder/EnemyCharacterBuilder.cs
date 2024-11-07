using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterBuilderParam : ICharacterBuildParam
{
    public int Lv;
}


public class EnemyCharacterBuilder : ICharacterBuilder
{
    public EnemyCharacterBuilderParam m_BuildParam = null;

    public override void SetBuildParam(ICharacterBuildParam theParam)
    {
        m_BuildParam = theParam as EnemyCharacterBuilderParam;
    }

    public override void SetCharacterAttr() 
    {
        IAttrFactory theAttrFactory = TOAFactory.GetAttrFactory();
        int AttrID = m_BuildParam.NewCharacter.GetAttrID();
        EnemyCharacterAttr theEnemyCharacterAttr = theAttrFactory.GetEnemyCharacterAttr(AttrID);  //根據數值ID來給你相對應的角色數值

        //還有演算法的部分要寫
        theEnemyCharacterAttr.SetAttrStrategy(new EnemyCharacterAttrStrategy());  //把數值計算方法記下來

        //設定等級
        theEnemyCharacterAttr.SetEnemyCharacterLv(m_BuildParam.Lv);  //從外面輸入的參數

        // 設定給角色
        m_BuildParam.NewCharacter.SetCharacterAttr(theEnemyCharacterAttr); //角色把數值記下來，並且在對數值進行初始化。
    }

    public override void SetCharacterAttr(List<IPlayerCharacter> list_PlayerTeamMember)
    {
        //這是PlayerBulider要做的事情
    }

    public override void SetAttackVFX(PlayerCharacter playerCharacter)
    {
        //敵人的部分要再寫另一個function
    }

    public override void SetAttackVFX(EnemyCharacter enemyCharacter)
    {
        switch (enemyCharacter)
        {
            case EnemyCharacter.SkeletonSoldier:
                //取得特效資源
                GameObject obj_preSkeletonSoldierVFX = Resources.Load<GameObject>("Prefab/VFX/Projectile 2");

                //設定給角色
                m_BuildParam.NewCharacter.SetAttackVFX(obj_preSkeletonSoldierVFX);
                break;
            case EnemyCharacter.AngryScorpion:
                //取得特效資源
                GameObject obj_preAngryScorpionVFX = Resources.Load<GameObject>("Prefab/VFX/Projectile 3");

                //設定給角色
                m_BuildParam.NewCharacter.SetAttackVFX(obj_preAngryScorpionVFX);
                break;
            case EnemyCharacter.Tauren:
                //取得特效資源
                GameObject obj_preTaurenVFX = Resources.Load<GameObject>("Prefab/VFX/Projectile 7");

                //設定給角色
                m_BuildParam.NewCharacter.SetAttackVFX(obj_preTaurenVFX);
                break;
        }
    }

    public override void SetAtkSoundFx(PlayerCharacter playerCharacter)
    {
        //EnemyCharacterBuilder不用做的事情
    }

    public override void SetAtkSoundFx(EnemyCharacter enemyCharacter)
    {
        //還沒想好敵人的攻擊音效
    }
}
