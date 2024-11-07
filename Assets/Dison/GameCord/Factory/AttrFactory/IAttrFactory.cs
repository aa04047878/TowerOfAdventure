using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttrFactory 
{
    public abstract PlayerCharacterAttr GetPlayerCharacterAttr(int AttrID);

    public abstract PlayerAttr GetPlayerAttr();

    public abstract EnemyCharacterAttr GetEnemyCharacterAttr(int AttrID);
}
