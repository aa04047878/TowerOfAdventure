using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManager 
{
    /// <summary>
    /// 轉珠時間到
    /// </summary>
    /// <returns></returns>
    bool TurnBeatsTimesUp();

    /// <summary>
    /// 顯示轉珠時間
    /// </summary>
    void ShowTurnBeatsTime();

    /// <summary>
    /// 減少轉珠時間
    /// </summary>
    void ReduceTime();

    /// <summary>
    /// 不能轉珠面板打開
    /// </summary>
    void CantTurnBeatsPanelOpen();

    /// <summary>
    /// 設定玩家角色傷害
    /// </summary>
    void SetPlayerCharacterDamage();

    /// <summary>
    /// 關閉轉珠時間
    /// </summary>
    /// <returns></returns>
    IEnumerator CoCloseTurnBeatsTime();

    /// <summary>
    /// 攻擊怪物
    /// </summary>
    /// <returns></returns>
    IEnumerator CoAttackMonster();

    /// <summary>
    /// 攻擊玩家
    /// </summary>
    /// <returns></returns>
    IEnumerator CoAttackPlayer();

    /// <summary>
    /// 誰死了
    /// </summary>
    /// <returns></returns>
    IEnumerator CoWhoDied();

    /// <summary>
    /// 重製轉珠時間
    /// </summary>
    /// <returns></returns>
    IEnumerator CoResetTime();

    /// <summary>
    /// 敵人角色1號受到傷害
    /// </summary>
    /// <param name="vfxName"></param>
    void EnemyCh01Hurt(string vfxName);

    /// <summary>
    /// 敵人角色2號受到傷害
    /// </summary>
    /// <param name="vfxName"></param>
    void EnemyCh02Hurt(string vfxName);

    /// <summary>
    /// Boss受到傷害
    /// </summary>
    /// <param name="vfxName"></param>
    void BossHurt(string vfxName);

    /// <summary>
    /// 玩家受到傷害
    /// </summary>
    /// <param name="vfxName"></param>
    void PlayerHurt(string vfxName);

    /// <summary>
    /// 播放轉珠音效
    /// </summary>
    void PlayTurnBeatsAudio();

    /// <summary>
    /// 播放轉珠2音效
    /// </summary>
    void PlayTurnBeats2Audio();

    /// <summary>
    /// 播放Combo音效
    /// </summary>
    void PlayComboAudio();
}
