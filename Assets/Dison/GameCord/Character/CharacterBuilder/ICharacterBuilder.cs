using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterBuildParam
{
    public ICharacter NewCharacter = null;
}

public abstract class ICharacterBuilder 
{
    // 設定建立參數
    public abstract void SetBuildParam(ICharacterBuildParam theParam);


    public abstract void SetCharacterAttr();

    public abstract void SetCharacterAttr(List<IPlayerCharacter> list_PlayerTeamMember);

    //這裡到時候要改成抽象
    public abstract void SetAttackVFX(PlayerCharacter playerCharacter);

    public abstract void SetAttackVFX(EnemyCharacter enemyCharacter);

    public abstract void SetAtkSoundFx(PlayerCharacter playerCharacter);

    public abstract void SetAtkSoundFx(EnemyCharacter enemyCharacter);
}
