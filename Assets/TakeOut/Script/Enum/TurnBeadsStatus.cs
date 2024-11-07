using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnBeadsStatus 
{
    WaitTurnBeads, //等待轉珠中
    DeleteBeads, //消除珠子中
    WaitProcessFinish, //等待流程結束
    IntoNextBattle,  //進入下一個battle
    WaitGenerate,  //等待產生
    PlayerDead  //玩家死亡
}
