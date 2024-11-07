using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterFactory 
{
	/// <summary>
	/// 創造玩家角色
	/// </summary>
	/// <param name="Lv">角色等級</param>
	/// <returns></returns>
	public abstract IPlayerCharacter CreatePlayerCharacter(PlayerCharacter playerCharacter, int Lv); //第一個參數不好，要改善(已完成)

	/// <summary>
	/// 創造玩家
	/// </summary>
	/// <param name="list_playerTeamMember"></param>
	/// <returns></returns>
	public abstract IPlayerCharacter CreatePlayerCharacter(List<IPlayerCharacter> list_playerTeamMember);

	/// <summary>
	/// 創造敵人角色
	/// </summary>
	/// <param name="enemyCharacter"></param>
	/// <param name="Lv"></param>
	/// <returns></returns>
	public abstract IEnemyCharacter CreateEnemyCharacter(EnemyCharacter enemyCharacter, int Lv);


}
