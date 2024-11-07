using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理所有的工廠(確保每個工廠都只會被產生出一個(用靜態的class管理每個工廠))
/// </summary>
public static class TOAFactory 
{
    private static ICharacterFactory m_CharacterFactory = null;
    private static IAttrFactory m_AttrFactory = null;

	// 遊戲角色工廠
	public static ICharacterFactory GetCharacterFactory()
	{
		if (m_CharacterFactory == null)
			m_CharacterFactory = new CharacterFactory();
		return m_CharacterFactory;
	}

	public static IAttrFactory GetAttrFactory()
	{
		if (m_AttrFactory == null)
			m_AttrFactory = new AttrFactory();
		return m_AttrFactory;
	}
}
