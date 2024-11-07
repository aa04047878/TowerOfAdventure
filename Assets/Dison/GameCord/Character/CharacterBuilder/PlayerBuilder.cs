using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBuilderParam : ICharacterBuildParam
{
    //不需要等級，所以這裡是空的
}

public class PlayerBuilder : ICharacterBuilder
{
    public PlayerBuilderParam m_BuildParam = null;
    public override void SetBuildParam(ICharacterBuildParam theParam)
    {
        m_BuildParam = theParam as PlayerBuilderParam;
    }

    public override void SetCharacterAttr(List<IPlayerCharacter> list_PlayerTeamMember)
    {
        IAttrFactory theAttrFactory = TOAFactory.GetAttrFactory();
        PlayerAttr thePlayerAttr = theAttrFactory.GetPlayerAttr();

        thePlayerAttr.SetAttrStrategy(new PlayerAttrStrategy(list_PlayerTeamMember));  //把數值計算方法記下來

        m_BuildParam.NewCharacter.SetCharacterAttr(thePlayerAttr);  //角色把數值記下來，並且在對數值進行初始化。
        Debug.Log("已設定角色素質");

    }

    public override void SetCharacterAttr()
    {
        //這是PlayerCharacterBuilder要做的事情
    }

    public override void SetAttackVFX(PlayerCharacter playerCharacter)
    {
        //PlayerBuilder不用做的事情
    }

    public override void SetAttackVFX(EnemyCharacter enemyCharacter)
    {
        //PlayerBuilder不用做的事情
    }

    public override void SetAtkSoundFx(PlayerCharacter playerCharacter)
    {
        //PlayerBuilder不用做的事情
    }

    public override void SetAtkSoundFx(EnemyCharacter enemyCharacter)
    {
        //PlayerBuilder不用做的事情
    }
}
