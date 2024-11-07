using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSoldier : IEnemyCharacter
{
    public SkeletonSoldier()
    {
        m_EnemyCharacter = EnemyCharacter.SkeletonSoldier;
        m_AttrID = 1;
    }
}
