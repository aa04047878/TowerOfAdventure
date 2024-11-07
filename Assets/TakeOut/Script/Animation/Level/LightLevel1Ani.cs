using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLevel1Ani : MonoBehaviour, ILevelAni
{
    public BallControl ballCtrl;

    public Animator ani_Battle;

    public Animator ani_LevelMagager;
    /// <summary>
    /// Battle01敵人動畫清單
    /// </summary>
    public List<Animator> list_EnemyAniBattle01;

    /// <summary>
    /// Battle02敵人動畫清單
    /// </summary>
    public List<Animator> list_EnemyAniBattle02;

    /// <summary>
    /// Battle03敵人動畫清單
    /// </summary>
    public List<Animator> list_EnemyAniBattle03;

    /// <summary>
    /// 敵人動畫清單
    /// </summary>
    public List<List<Animator>> list_EnemyAni;

    public float nowBattle;
    public float atkOrderUI;
    public bool checkBattleAniFinish;
    public bool intoNextBattle02;
    public bool intoNextBattle03;
    public bool checkBattlePlayFinish;
    public bool battle02EnemyAppear;
    private bool levelFinish;

    /// <summary>
    /// 所有的球出現(動畫事件)
    /// </summary>
    public void AllBallAppear()
    {
        //ballAni.SetTrigger("allballappear");  //這樣寫只有一顆球會做事情

        for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
        {
            ballCtrl.list_AllBall[i].GetComponent<Animator>().SetTrigger("ballappear");
        }
    }

    /// <summary>
    /// 播放Battle動畫(也有動畫事件)
    /// </summary>
    public void PlayBattleAni()
    {
        ani_Battle.SetTrigger("showbattleani");
    }

    /// <summary>
    /// Battle01敵人出現(動畫事件)
    /// </summary>
    public void EnamyAppearBattle01()
    {
        foreach (var enemy in list_EnemyAniBattle01)
        {
            enemy.SetTrigger("appear");
        }
    }

    /// <summary>
    /// 敵人死亡
    /// </summary>
    /// <param name="nowBattle">現在battle</param>
    /// <param name="atkOrderUI">現在OrderUI</param>
    public void EnemyDead(int nowBattle, int atkOrderUI)
    {
        Debug.Log($"nowBattle : {nowBattle}, atkOrderUI : {atkOrderUI}");
        list_EnemyAni[nowBattle - 1][atkOrderUI].SetTrigger("dead");

        //-------------
        if (LightLevel1Manager.inst.IntoNextBattle())
        {
            //可以進入下一個關卡代表最後一隻怪物死了，
            //開始檢查最後一隻死亡的動畫完成了沒。
            LightLevel1Manager.inst.ballCtrl.turnBeadsStatus = TurnBeadsStatus.IntoNextBattle;
            this.nowBattle = nowBattle;
            this.atkOrderUI = atkOrderUI;
            checkBattleAniFinish = true; //開始檢查
        }
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    public void PlayerDead()
    {
        LightLevel1Manager.inst.ballCtrl.turnBeadsStatus = TurnBeadsStatus.PlayerDead;
        ani_LevelMagager.SetTrigger("fail");
    }

    /// <summary>
    /// 確認battle死亡動畫完成
    /// </summary>
    public void CheckBattleDiedAniFinish()
    {
        if (checkBattleAniFinish)
        {
            AnimatorStateInfo info = list_EnemyAni[(int)nowBattle - 1][(int)atkOrderUI].GetCurrentAnimatorStateInfo(0);
            Debug.Log("動畫持續時間 : " + info.normalizedTime);

            if (info.normalizedTime >= 1.5f && nowBattle == 1)
            {
                //normalizedTime >> 動畫一play的時候就開始計算的時間 ，過了1秒以後代表動畫以播完(動畫只有1秒)
                //動畫播放完成
                //WaterLevel1Manager.inst.BattleAdd();
                //BattleOpen();
                intoNextBattle02 = true;
                Debug.Log("播放完成");
                //checkBattle01Finish = false; // 不能直接把計時的bool開關關掉，不然時間會順間快轉然後停止，屢試不爽
            }

            if (info.normalizedTime >= 1.5f && nowBattle == 2)
            {
                intoNextBattle03 = true;
                Debug.Log("播放完成");

            }

            if (info.normalizedTime >= 1.5f && nowBattle == 3)
            {
                levelFinish = true;
                Debug.Log("關卡完成");

            }
        }
    }

    /// <summary>
    /// 是否進入下一個Battle
    /// </summary>
    public void IntoNextBattle()
    {
        if (intoNextBattle02)
        {
            //intoNextBattle02 = false;
            Debug.Log("add了多少次battle : ");
            LightLevel1Manager.inst.BattleAdd();
            //PlayBattleAni();
            //checkBattlePlayFinish = true;
            EnterBattle2();
            intoNextBattle02 = false;
            checkBattleAniFinish = false;
        }

        if (intoNextBattle03)
        {
            Debug.Log("add了多少次battle : ");
            LightLevel1Manager.inst.BattleAdd();
            EnterBattle3();
            intoNextBattle03 = false;
            checkBattleAniFinish = false;
        }

        if (levelFinish)
        {
            Success();
            levelFinish = false;
            checkBattleAniFinish = false;
        }
    }

    /// <summary>
    /// Battle02敵人出現(動畫事件)
    /// </summary>
    public void EnamyAppearBattle02()
    {
        foreach (var enemy in list_EnemyAniBattle02)
        {
            enemy.SetTrigger("appear");
        }
    }

    /// <summary>
    /// Battle03敵人出現(動畫事件)
    /// </summary>
    public void EnamyAppearBattle03()
    {
        foreach (var enemy in list_EnemyAniBattle03)
        {
            enemy.SetTrigger("appear");

        }
    }

    /// <summary>
    /// 改變轉珠狀態(動畫事件)
    /// </summary>
    public void ChangeTurnBeadsStatus()
    {
        LightLevel1Manager.inst.CanTurnBeats();
    }

    /// <summary>
    /// 等待怪物珠子產生(動畫事件)
    /// </summary>
    public void WaitEnemyBeatsGenerate()
    {
        LightLevel1Manager.inst.ballCtrl.turnBeadsStatus = TurnBeadsStatus.WaitGenerate;
        LightLevel1Manager.inst.obj_CantTurnBeatsPanel.SetActive(true);
    }

    /// <summary>
    /// 產生完成(動畫事件)
    /// </summary>
    public void GenerateFinish()
    {
        LightLevel1Manager.inst.CanTurnBeats();
    }

    /// <summary>
    /// 進入battle2
    /// </summary>
    public void EnterBattle2()
    {
        ani_LevelMagager.SetTrigger("enterbattle2");
    }

    /// <summary>
    /// 進入battle3
    /// </summary>
    public void EnterBattle3()
    {
        ani_LevelMagager.SetTrigger("enterbattle3");
    }

    /// <summary>
    /// 成功
    /// </summary>
    private void Success()
    {
        ani_LevelMagager.SetTrigger("victory");
    }

    /// <summary>
    /// 播放戰鬥開始音樂(動畫事件)
    /// </summary>
    public void PlayBattleStartMusic()
    {
        GameLoop.inst.PlayBattleStartMusic();
    }

    /// <summary>
    /// 播放戰鬥Boss音樂(動畫事件)
    /// </summary>
    public void PlayBattleBossMusic()
    {
        GameLoop.inst.PlayBattleBossMusic();
    }

    /// <summary>
    /// 播放勝利音樂(動畫事件)
    /// </summary>
    public void PlayVictoryMusic()
    {
        GameLoop.inst.PlayVictoryMusic();
    }

    // Start is called before the first frame update
    void Start()
    {
        list_EnemyAni = new List<List<Animator>>();
        list_EnemyAni.Add(list_EnemyAniBattle01);
        list_EnemyAni.Add(list_EnemyAniBattle02);
        list_EnemyAni.Add(list_EnemyAniBattle03);
        LightLevel1Manager.inst.SetLevelAni(this);
    }

    // Update is called once per frame
    void Update()
    {
        CheckBattleDiedAniFinish();
        IntoNextBattle();
    }
}
