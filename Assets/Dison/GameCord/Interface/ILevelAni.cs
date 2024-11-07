using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelAni 
{
    /// <summary>
    /// 敵人死亡
    /// </summary>
    /// <param name="nowBattle"></param>
    /// <param name="atkOrderUI"></param>
    void EnemyDead(int nowBattle, int atkOrderUI);
}
