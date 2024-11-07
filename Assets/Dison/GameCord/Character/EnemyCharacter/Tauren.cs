using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tauren : IEnemyCharacter
{
    public Tauren()
    {
        m_EnemyCharacter = EnemyCharacter.Tauren;
        m_AttrID = 3;
    }
}
