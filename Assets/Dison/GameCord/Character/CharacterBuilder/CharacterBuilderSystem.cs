using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuilderSystem : IGameSystem
{
    public CharacterBuilderSystem(TowerOfAdventureGame TOAGame) :base(TOAGame)
    {

    }

    public override void Initialize()
    { }

    public override void Update()
    { }

	public void Construct(ICharacterBuilder theBuilder, PlayerCharacter playerCharacter) //這裡明確定義物件組裝流程(角色組裝流程)
	{
		// 利用Builder產生各部份加入Product中		
		theBuilder.SetCharacterAttr();
		theBuilder.SetAttackVFX(playerCharacter);
		theBuilder.SetAtkSoundFx(playerCharacter);
	}

	public void Construct(ICharacterBuilder theBuilder, EnemyCharacter enemyCharacter) //這裡明確定義物件組裝流程(角色組裝流程)
	{
		// 利用Builder產生各部份加入Product中		
		theBuilder.SetCharacterAttr();
		theBuilder.SetAttackVFX(enemyCharacter);
		theBuilder.SetAtkSoundFx(enemyCharacter);
	}

	public void Construct(ICharacterBuilder theBuilder, List<IPlayerCharacter> list_playerTeamMember) //這裡明確定義物件組裝流程(角色組裝流程)
	{
		// 利用Builder產生各部份加入Product中		
		theBuilder.SetCharacterAttr(list_playerTeamMember);
	}
}
