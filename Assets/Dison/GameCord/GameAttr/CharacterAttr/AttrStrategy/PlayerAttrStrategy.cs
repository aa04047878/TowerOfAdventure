using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttrStrategy : IAttrStrategy
{
    List<IPlayerCharacter> list_PlayerTeamMember;

    public PlayerAttrStrategy(List<IPlayerCharacter> list_PlayerTeamMember)
    {
        this.list_PlayerTeamMember = list_PlayerTeamMember;
    }

    public override void InitAttr(ICharacterAttr CharacterAttr)
    {
        PlayerAttr thePlayerAttr = CharacterAttr as PlayerAttr;
        int playerHP = 0;
        for (int i = 0; i < list_PlayerTeamMember.Count; i++)
        {
            playerHP += list_PlayerTeamMember[i].GetNowHP();
        }
        thePlayerAttr.SetPlayerHP(playerHP);

    }
}
