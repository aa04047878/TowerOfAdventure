using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevel1Manager : MonoBehaviour, ILevelManager
{
    public static WaterLevel1Manager inst;
    public BallControl ballCtrl;
    public WaterLevel1Ani level1Ani;
    ILevelAni levelAni;
    public GameObject obj_CantTurnBeatsPanel;
    public GameObject obj_LevelFinishPanel;
    public PlayerData playerData;
    private string j_PlayerData;
    //472行 792行

    #region 返回主選單
    [Header("返回主選單")]
    public Button btn_VictoryReturnMainMenu;
    private bool returnMainMenu;

    public Button btn_FailReturnMainMenu;
    #endregion

    #region 關卡經驗值
    [Header("關卡經驗值")]
    public float levelExp;
    public Text txt_LevelExp;
    #endregion

    #region 玩家
    [Header("玩家")]
    public PlayerBody playerBody;
    /// <summary>
    /// 玩家隊伍成員清單
    /// </summary>
    List<IPlayerCharacter> list_PlayerTeamMember;
    /// <summary>
    /// 玩家
    /// </summary>
    IPlayerCharacter player;

    /// <summary>
    /// 玩家的總血量
    /// </summary>
    private float playerMaxHP;

    /// <summary>
    /// 玩家的現在剩餘血量
    /// </summary>
    private float playerNowHP;

    /// <summary>
    /// 玩家血條
    /// </summary>
    public Image img_PlayerHp;

    public Text txt_MaxHP;
    public Text txt_NowHP;

    /// <summary>
    /// 玩家隊伍照片清單
    /// </summary>
    public List<Image> list_PlayerTeamImg;

    #endregion

    #region 怪物
    [Header("怪物")]
    public SmallMonster1 smallMonster1;
    public SmallMonster2 smallMonster2;
    public Boss boss;

    /// <summary>
    /// 怪物角色清單
    /// </summary>
    private List<List<IEnemyCharacter>> list_EnemyCharacter;

    private List<List<Image>> list_EnemyCharacterHpBar;
    //private List<List<Image>> list_EnemyCharacterImg;
    private List<float> list_EnemyChMaxHpNowBattle1;
    private List<float> list_EnemyChMaxHpNowBattle2;
    private List<float> list_EnemyChMaxHpNowBattle3;
    private List<float> list_EnemyChNowHpNowBattle1;
    private List<float> list_EnemyChNowHpNowBattle2;
    private List<float> list_EnemyChNowHpNowBattle3;
    private List<List<float>> list_EnemyChMaxHp;
    private List<List<float>> list_EnemyChNowHp;
    private int enemyDeadCount;
    private bool atkPlayer;
    #endregion

    #region battle順序
    [Header("battle順序")]
    private int nowBattle;
    private int totalBattle;
    private int atkOrder;
    private int atkOrderUI;
    public Text txt_nowBattle;
    public Text txt_totalBattle;
    #endregion

    #region 特效
    [Header("特效")]
    /// <summary>
    /// 玩家角色攻擊特效清單
    /// </summary>
    public List<GameObject> list_PlayerChAtkVfx;

    /// <summary>
    /// 特效生成物件後的實體
    /// </summary>
    public List<GameObject> list_PlayerChAtkVfxIns;

    /// <summary>
    /// 玩家角色攻擊特效位置
    /// </summary>
    public List<Vector3> list_PlayerChAtkVfxPos;

    /// <summary>
    /// 敵人角色攻擊特效位置
    /// </summary>
    public List<Vector3> list_EnemyChAtkVfxPos;


    public Vector3 bossAtkVfxPos;

    /// <summary>
    /// 玩家角色的攻擊順序
    /// </summary>
    public List<List<Vector3>> list_PlayerChATKOrder;

    /// <summary>
    /// 玩家角色打Boss的攻擊順序
    /// </summary>
    public List<List<Vector3>> list_PlayerChATKOrderBoss;

    public GameObject obj_HitNumberVfxDarkPre;
    public GameObject obj_HitNumberVfxFirePre;
    public GameObject obj_HitNumberVfxWaterPre;
    public GameObject obj_HitNumberVfxWoodPre;
    public GameObject obj_HitNumberVfxLightPre;
    public GameObject HitVfxfa;


    public GameObject genVfxNumber;
    /// <summary>
    /// 測試特效數字
    /// </summary>
    public GameObject txt_VfxNumber;
    public Vector3 txt_VfxPos;
    #endregion

    #region 轉珠時間
    [Header("轉珠時間")]
    public Image img_TurnBeatsTime;
    public Image img_Time;
    private float totalTime;
    private float nowTime;

    #endregion

    #region 音效
    [Header("音效")]
    public AudioSource audioSource;
    public List<AudioClip> list_TurnBeatBGM;

    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;
    public AudioSource audioSource5;
    public int changeNumber;
    #endregion

    /// <summary>
    /// 設定關卡動畫介面
    /// </summary>
    /// <param name="levelAni"></param>
    public void SetLevelAni(ILevelAni levelAni)
    {
        this.levelAni = levelAni;
    }

    /// <summary>
    /// 我是管理者
    /// </summary>
    /// <param name="levelManager"></param>
    public void IAmManager(ILevelManager levelManager)
    {
        foreach (var ball in ballCtrl.list_AllBall)
        {
            ball.SetLevelManager(levelManager);
        }
        ballCtrl.SetLevelManager(levelManager);
        smallMonster1.SetLevelManager(levelManager);
        smallMonster2.SetLevelManager(levelManager);
        boss.SetLevelManager(levelManager);
        playerBody.SetLevelManager(levelManager);
    }

    public void CantTurnBeatsPanelOpen()
    {
        obj_CantTurnBeatsPanel.SetActive(true);
    }

    public int GetNowBattle()
    {
        return nowBattle;
    }

    /// <summary>
    /// 進入下一個關卡
    /// </summary>
    /// <returns></returns>
    public bool IntoNextBattle()
    {
        return list_EnemyChNowHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1] <= 0; //該battle的最後一隻怪物已經死了
    }

    /// <summary>
    /// 增加Battle
    /// </summary>
    public void BattleAdd()
    {
        nowBattle++;
        txt_nowBattle.text = nowBattle.ToString();
        //進入下一個battle後，order要歸0
        atkOrderUI = 0;
        atkOrder = 0;
        enemyDeadCount = 0;
    }

    /// <summary>
    /// 玩家角色攻擊順序初始化
    /// </summary>
    private void PlayerChAtkOrderInit()
    {
        #region 打小怪
        list_PlayerChATKOrder = new List<List<Vector3>>();
        //第1位玩家角色
        list_PlayerChATKOrder.Add(new List<Vector3>());
        list_PlayerChATKOrder[0].Add(new Vector3(-47.5f, 90, 0)); //第1位玩家角色打第1隻怪物的角度, order = 0
        list_PlayerChATKOrder[0].Add(new Vector3(-21.505f, 90, 0)); //第1位玩家角色打第2隻怪物的角度, order = 1

        //第2位玩家角色
        list_PlayerChATKOrder.Add(new List<Vector3>());
        list_PlayerChATKOrder[1].Add(new Vector3(-85f, 90, 0)); //第2位玩家角色打第1隻怪物的角度, order = 0
        list_PlayerChATKOrder[1].Add(new Vector3(-30, 90, 0)); //第2位玩家角色打第2隻怪物的角度, order = 1

        //第3位玩家角色
        list_PlayerChATKOrder.Add(new List<Vector3>());
        list_PlayerChATKOrder[2].Add(new Vector3(-126, 90, 0)); //第3位玩家角色打第1隻怪物的角度, order = 0
        list_PlayerChATKOrder[2].Add(new Vector3(-47.5f, 90, 0)); //第3位玩家角色打第2隻怪物的角度, order = 1

        //第4位玩家角色
        list_PlayerChATKOrder.Add(new List<Vector3>());
        list_PlayerChATKOrder[3].Add(new Vector3(-147.2f, 90, 0)); //第4位玩家角色打第1隻怪物的角度, order = 0
        list_PlayerChATKOrder[3].Add(new Vector3(-85f, 90, 0)); //第4位玩家角色打第2隻怪物的角度, order = 1

        //第5位玩家角色
        list_PlayerChATKOrder.Add(new List<Vector3>());
        list_PlayerChATKOrder[4].Add(new Vector3(-157.2f, 90, 0)); //第5位玩家角色打第1隻怪物的角度, order = 0
        list_PlayerChATKOrder[4].Add(new Vector3(-126f, 90, 0)); //第5位玩家角色打第2隻怪物的角度, order = 1
        #endregion

        #region 打Boss
        list_PlayerChATKOrderBoss = new List<List<Vector3>>();
        //第1位玩家角色
        list_PlayerChATKOrderBoss.Add(new List<Vector3>());
        list_PlayerChATKOrderBoss[0].Add(new Vector3(-35, 90, 0)); //第1位玩家角色打第1隻怪物(Boss)的角度, order = 0

        //第2位玩家角色
        list_PlayerChATKOrderBoss.Add(new List<Vector3>());
        list_PlayerChATKOrderBoss[1].Add(new Vector3(-50, 90, 0)); //第2位玩家角色打第1隻怪物(Boss)的角度, order = 0

        //第3位玩家角色
        list_PlayerChATKOrderBoss.Add(new List<Vector3>());
        list_PlayerChATKOrderBoss[2].Add(new Vector3(-85, 90, 0)); //第3位玩家角色打第1隻怪物(Boss)的角度, order = 0

        //第4位玩家角色
        list_PlayerChATKOrderBoss.Add(new List<Vector3>());
        list_PlayerChATKOrderBoss[3].Add(new Vector3(-120, 90, 0)); //第4位玩家角色打第1隻怪物(Boss)的角度, order = 0

        //第5位玩家角色
        list_PlayerChATKOrderBoss.Add(new List<Vector3>());
        list_PlayerChATKOrderBoss[4].Add(new Vector3(-140, 90, 0)); //第4位玩家角色打第1隻怪物(Boss)的角度, order = 0
        #endregion
    }

    /// <summary>
    /// 玩家角色特效初始化相關
    /// </summary>
    private void PlayerChAtkVfxInit()
    {
        list_PlayerChAtkVfxPos = new List<Vector3>();
        list_PlayerChAtkVfxPos.Add(new Vector3(-9, 3, 0));
        list_PlayerChAtkVfxPos.Add(new Vector3(-4.5f, 3, 0));
        list_PlayerChAtkVfxPos.Add(new Vector3(0, 3, 0));
        list_PlayerChAtkVfxPos.Add(new Vector3(4.5f, 3, 0));
        list_PlayerChAtkVfxPos.Add(new Vector3(9, 3, 0));
    }

    /// <summary>
    /// 玩家初始化
    /// </summary>
    private void PlayerInit()
    {
        ICharacterFactory characterFactory = TOAFactory.GetCharacterFactory();
        player = characterFactory.CreatePlayerCharacter(list_PlayerTeamMember);
        Debug.Log($"玩家的總血量 : {player.GetPlayerHP()}");
    }

    /// <summary>
    /// 玩家隊伍初始化 此腳本 114
    /// </summary>
    private void PlayerTeamInit()
    {
        list_PlayerTeamMember = new List<IPlayerCharacter>();

        ICharacterFactory characterFactory = TOAFactory.GetCharacterFactory();
        //list_team.Add(characterFactory.CreatePlayerCharacter(PlayerCharacter.Alice, 2));

        for (int i = 0; i < TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData.Count; i++)
        {
            list_PlayerTeamMember.Add(characterFactory.CreatePlayerCharacter(TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].playerCharacter, TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].CharacterLevel));
            list_PlayerTeamImg[i].sprite = TowerOfAdventureGame.Inst.GetCardInfo().cardData[TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemImgIndexData[i]].CharacterAvatar;
        }             
    }

    /// <summary>
    /// 怪物隊伍初始化
    /// </summary>
    private void MonsterTeamInit()
    {
        list_EnemyCharacter = new List<List<IEnemyCharacter>>();
        ICharacterFactory characterFactory = TOAFactory.GetCharacterFactory();

        //創造小怪
        for (int i = 0; i < 2; i++)
        {
            list_EnemyCharacter.Add(new List<IEnemyCharacter>());
            for (int j = 0; j < 2; j++)
            {
                list_EnemyCharacter[i].Add(characterFactory.CreateEnemyCharacter((EnemyCharacter)(i + 1), 1));
                Debug.Log($"怪物清單第{i + 1}組的第{j + 1}之的傷害 : {list_EnemyCharacter[i][j].GetDamage()}");
            }    
        }

        //創造boss
        list_EnemyCharacter.Add(new List<IEnemyCharacter>());
        list_EnemyCharacter[2].Add(characterFactory.CreateEnemyCharacter((EnemyCharacter)3, 1));
        Debug.Log($"怪物清單第{3}組的第{1}之的傷害 : {list_EnemyCharacter[2][0].GetDamage()}");
    }

    /// <summary>
    /// 敵人角色血條初始化
    /// </summary>
    private void EnemyCharacterHpBarInit()
    {
        list_EnemyCharacterHpBar = new List<List<Image>>();

        list_EnemyCharacterHpBar.Add(new List<Image>());
        list_EnemyCharacterHpBar[0].Add(UITool.FindChildChildObjectComponent<Image>("SkeletonSoldier1", "HpBar","Hp"));
        list_EnemyCharacterHpBar[0].Add(UITool.FindChildChildObjectComponent<Image>("SkeletonSoldier2", "HpBar","Hp"));

        list_EnemyCharacterHpBar.Add(new List<Image>());
        list_EnemyCharacterHpBar[1].Add(UITool.FindChildChildObjectComponent<Image>("AngryScorpion1", "HpBar","Hp"));
        list_EnemyCharacterHpBar[1].Add(UITool.FindChildChildObjectComponent<Image>("AngryScorpion2", "HpBar","Hp"));

        list_EnemyCharacterHpBar.Add(new List<Image>());
        list_EnemyCharacterHpBar[2].Add(UITool.FindChildChildObjectComponent<Image>("Tauren", "HpBar","Hp"));        
    }

    /// <summary>
    /// 敵人角色HP初始化
    /// </summary>
    private void EnemyChHpInit()
    {
        list_EnemyChMaxHpNowBattle1 = new List<float>();
        for (int i = 0; i < list_EnemyCharacter[0].Count; i++)
        {
            list_EnemyChMaxHpNowBattle1.Add(list_EnemyCharacter[0][i].GetNowHP());
        }

        list_EnemyChMaxHpNowBattle2 = new List<float>();
        for (int i = 0; i < list_EnemyCharacter[1].Count; i++)
        {
            list_EnemyChMaxHpNowBattle2.Add(list_EnemyCharacter[1][i].GetNowHP());
        }

        list_EnemyChMaxHpNowBattle3 = new List<float>();
        for (int i = 0; i < list_EnemyCharacter[2].Count; i++)
        {
            list_EnemyChMaxHpNowBattle3.Add(list_EnemyCharacter[2][i].GetNowHP());
        }

        list_EnemyChNowHpNowBattle1 = new List<float>();
        for (int i = 0; i < list_EnemyCharacter[0].Count; i++)
        {
            list_EnemyChNowHpNowBattle1.Add(list_EnemyCharacter[0][i].GetNowHP());
        }

        list_EnemyChNowHpNowBattle2 = new List<float>();
        for (int i = 0; i < list_EnemyCharacter[1].Count; i++)
        {
            list_EnemyChNowHpNowBattle2.Add(list_EnemyCharacter[1][i].GetNowHP());
        }

        list_EnemyChNowHpNowBattle3 = new List<float>();
        for (int i = 0; i < list_EnemyCharacter[2].Count; i++)
        {
            list_EnemyChNowHpNowBattle3.Add(list_EnemyCharacter[2][i].GetNowHP());
        }

        list_EnemyChMaxHp = new List<List<float>>();
        list_EnemyChMaxHp.Add(list_EnemyChMaxHpNowBattle1);
        list_EnemyChMaxHp.Add(list_EnemyChMaxHpNowBattle2);
        list_EnemyChMaxHp.Add(list_EnemyChMaxHpNowBattle3);

        list_EnemyChNowHp = new List<List<float>>();
        list_EnemyChNowHp.Add(list_EnemyChNowHpNowBattle1);
        list_EnemyChNowHp.Add(list_EnemyChNowHpNowBattle2);
        list_EnemyChNowHp.Add(list_EnemyChNowHpNowBattle3);
        //list_EnemyChMaxHp[0].Add(new List<int>());  //不能這樣寫，就很麻煩
    }

    /// <summary>
    /// 玩家HP初始化
    /// </summary>
    private void PlayerHPInit()
    {
        playerMaxHP = player.GetPlayerHP();
        playerNowHP = player.GetPlayerHP();
        txt_NowHP.text = player.GetPlayerHP().ToString();
        txt_MaxHP.text = player.GetPlayerHP().ToString();
    }

    /// <summary>
    /// 玩家受到傷害
    /// </summary>
    public void PlayerHurt(string vfxName)
    {
        for (int i = 0; i < list_EnemyCharacter.Count; i++)
        {
            if (vfxName == $"EnemyChAtkVfx{i + 1}")
            {
                Debug.Log("nowBattle : " + nowBattle);
                Debug.Log("怪物的傷害 : " + list_EnemyCharacter[nowBattle - 1][i].GetDamage());
                playerNowHP -= list_EnemyCharacter[nowBattle - 1][i].GetDamage();
                txt_NowHP.text = playerNowHP.ToString();
                Debug.Log("目前玩家的HP : " + playerNowHP);
                if (playerNowHP <= 0)  //血量沒有負的
                {
                    playerNowHP = 0;
                }
                ShowPlayerRemainingHp();
                ballCtrl.turnBeadsStatus = TurnBeadsStatus.WaitTurnBeads;
                obj_CantTurnBeatsPanel.SetActive(false);
            }               
        }

        StartCoroutine(CoCheckPlayerSurvive());
    }



    /// <summary>
    /// 敵人角色1號受到傷害
    /// </summary>
    /// <param name="vfxName"></param>
    public void EnemyCh01Hurt(string vfxName)
    {       
        for (int i = 0; i < list_PlayerTeamMember.Count; i++)
        {
            if (vfxName == $"PlayerChAtkVfx{i + 1}")
            {
                list_EnemyChNowHp[nowBattle - 1][atkOrderUI] -= list_PlayerTeamMember[i].GetDamage();
                ShowEnemyRemainingHp();
                PlayPlayerChAtkSoundFx(i);
                GenerateHitNumber(list_PlayerTeamMember[i].GetDamage(), new Vector3(-4, 14, 0), TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].playerCharacter);
                if (list_EnemyChNowHp[nowBattle - 1][atkOrderUI] <= 0)
                {
                    list_EnemyChNowHp[nowBattle - 1][atkOrderUI] = 0;
                    //level1Ani.EnemyDead(nowBattle, atkOrderUI);
                    if (list_EnemyChNowHp[nowBattle - 1].Count > atkOrderUI + 1)   //這個atkOrderUI不是該battle的最後一隻怪物
                    {
                        Debug.Log($"Battle{nowBattle}的怪物數量 : {list_EnemyChNowHp[nowBattle - 1].Count}");
                        atkOrderUI++; //打死換打下一隻
                        enemyDeadCount++;
                        Debug.Log($"atkOrderUI : {atkOrderUI}, enemyDeadCount : {enemyDeadCount}");
                    }
                }
            }


            
        }
    }

    /// <summary>
    /// 敵人角色2號受到傷害
    /// </summary>
    /// <param name="vfxName"></param>
    public void EnemyCh02Hurt(string vfxName)
    {
        for (int i = 0; i < list_PlayerTeamMember.Count; i++)
        {
            if (vfxName == $"PlayerChAtkVfx{i + 1}")
            {
                PlayPlayerChAtkSoundFx(i);
                if (list_EnemyChNowHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1] <= 0) //最後1隻怪物已經死了
                {
                    //只能打最後一隻怪物
                    list_EnemyChNowHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1] -= list_PlayerTeamMember[i].GetDamage();                   
                    list_EnemyChNowHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1] = 0; //血量沒有負的
                    ShowEnemyRemainingHp();
                    GenerateHitNumber(list_PlayerTeamMember[i].GetDamage(), new Vector3(5, 14, 0), TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].playerCharacter);
                    Debug.Log($"atkOrderUI : {atkOrderUI}, enemyDeadCount : {enemyDeadCount}");
                }
                else
                {
                    list_EnemyChNowHp[nowBattle - 1][atkOrderUI] -= list_PlayerTeamMember[i].GetDamage();
                    ShowEnemyRemainingHp();
                    GenerateHitNumber(list_PlayerTeamMember[i].GetDamage(), new Vector3(5, 14, 0), TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].playerCharacter);
                    if (list_EnemyChNowHp[nowBattle - 1][atkOrderUI] <= 0)
                    {
                        list_EnemyChNowHp[nowBattle - 1][atkOrderUI] = 0;
                        //level1Ani.EnemyDead(nowBattle, atkOrderUI);
                        if (list_EnemyChNowHp[nowBattle - 1].Count > atkOrderUI + 1)   //這個atkOrderUI"不是"該battle的最後一隻怪物
                        {
                            atkOrderUI++; //打死換打下一隻
                            enemyDeadCount++;
                            Debug.Log("假設打死的不是最後一隻怪物");
                            Debug.Log($"atkOrderUI : {atkOrderUI}, enemyDeadCount : {enemyDeadCount}");
                        }
                        else if (list_EnemyChNowHp[nowBattle - 1].Count == atkOrderUI + 1) //這個atkOrderUI"是"該battle的最後一隻怪物
                        {
                            enemyDeadCount++;
                            Debug.Log("假設打死的是最後一隻怪物");
                            Debug.Log($"atkOrderUI : {atkOrderUI}, enemyDeadCount : {enemyDeadCount}");
                        }
                    }
                }               
            }


            //if (vfxName == $"PlayerChAtkVfx5")  //最後一個人打到怪物，檢查有哪些怪物死掉，死掉的要做動畫
            //{
            //    for (int j = 0; j < enemyDeadCount; j++)
            //    {
            //        level1Ani.EnemyDead(nowBattle, j);
            //    }
            //}
        }
    }


    /// <summary>
    /// Boss受到傷害
    /// </summary>
    public void BossHurt(string vfxName)
    {
        for (int i = 0; i < list_PlayerTeamMember.Count; i++)
        {
            if (vfxName == $"PlayerChAtkVfx{i + 1}")
            {
                PlayPlayerChAtkSoundFx(i);
                if (list_EnemyChNowHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1] <= 0) //最後1隻怪物已經死了
                {
                    //只能打最後一隻怪物
                    list_EnemyChNowHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1] -= list_PlayerTeamMember[i].GetDamage();
                    list_EnemyChNowHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1] = 0; //血量沒有負的
                    ShowEnemyRemainingHp();
                    GenerateHitNumber(list_PlayerTeamMember[i].GetDamage(), new Vector3(0.5f, 16.5f, 0), TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].playerCharacter);
                    Debug.Log($"atkOrderUI : {atkOrderUI}, enemyDeadCount : {enemyDeadCount}");
                }
                else
                {
                    list_EnemyChNowHp[nowBattle - 1][atkOrderUI] -= list_PlayerTeamMember[i].GetDamage();
                    ShowEnemyRemainingHp();
                    GenerateHitNumber(list_PlayerTeamMember[i].GetDamage(), new Vector3(0.5f, 16.5f, 0), TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].playerCharacter);
                    //GenerateHitNumber(list_PlayerTeamMember[i].GetDamage(), txt_VfxPos, TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].playerCharacter);
                    if (list_EnemyChNowHp[nowBattle - 1][atkOrderUI] <= 0)
                    {
                        list_EnemyChNowHp[nowBattle - 1][atkOrderUI] = 0;
                        //level1Ani.EnemyDead(nowBattle, atkOrderUI);
                        if (list_EnemyChNowHp[nowBattle - 1].Count == atkOrderUI + 1)   //這個atkOrderUI是該battle的最後一隻怪物
                        {
                            Debug.Log($"Battle{nowBattle}的怪物數量 : {list_EnemyChNowHp[nowBattle - 1].Count}");
                            //atkOrderUI++; //打死換打下一隻
                            enemyDeadCount++;
                            Debug.Log($"atkOrderUI : {atkOrderUI}, enemyDeadCount : {enemyDeadCount}");
                        }
                    }
                }                
            }           
        }
    }

    /// <summary>
    /// 誰死了
    /// </summary>
    public IEnumerator CoWhoDied()
    {
        yield return CoCheckEnemySurvive();
        //yield return CoCheckPlayerSurvive();
    }

    /// <summary>
    /// 檢查敵人存活
    /// </summary>
    public IEnumerator CoCheckEnemySurvive()
    {
        Debug.Log("enemyDeadCount : " + enemyDeadCount);
        for (int j = 0; j < enemyDeadCount; j++)
        {
            //level1Ani.EnemyDead(nowBattle, j);
            levelAni.EnemyDead(nowBattle, j);
        }
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// 檢查玩家存活
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoCheckPlayerSurvive()
    {
        Debug.Log("檢查玩家存活");
        Debug.Log("playerNowHP : " + playerNowHP);
        if (playerNowHP <= 0)
        {
            //Debug.Log("playerNowHP : " + playerNowHP);
            level1Ani.PlayerDead();
        }
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// 顯示敵人剩餘血量
    /// </summary>
    private void ShowEnemyRemainingHp()
    {
        if (list_EnemyChNowHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1] <= 0) //最後一隻怪物已經死了
        {
            if (nowBattle == 3)
            {
                list_EnemyCharacterHpBar[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1].transform.localPosition = new Vector3(-360 + 360 * (list_EnemyChNowHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1] / list_EnemyChMaxHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1]), 0, 0);
            }
            else
            {
                list_EnemyCharacterHpBar[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1].transform.localPosition = new Vector3(-210 + 210 * (list_EnemyChNowHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1] / list_EnemyChMaxHp[nowBattle - 1][list_EnemyChNowHp[nowBattle - 1].Count - 1]), 0, 0);
            }           
        }
        else
        {
            if (nowBattle == 3)
            {
                list_EnemyCharacterHpBar[nowBattle - 1][atkOrderUI].transform.localPosition = new Vector3(-360 + 360 * (list_EnemyChNowHp[nowBattle - 1][atkOrderUI] / list_EnemyChMaxHp[nowBattle - 1][atkOrderUI]), 0, 0);
            }
            else
            {
                list_EnemyCharacterHpBar[nowBattle - 1][atkOrderUI].transform.localPosition = new Vector3(-210 + 210 * (list_EnemyChNowHp[nowBattle - 1][atkOrderUI] / list_EnemyChMaxHp[nowBattle - 1][atkOrderUI]), 0, 0);
            }         
        }      
    }

    /// <summary>
    /// 顯示玩家剩餘血量
    /// </summary>
    private void ShowPlayerRemainingHp()
    {
        Debug.Log("img_PlayerHp.transform.localPosition : " + img_PlayerHp.transform.localPosition);
        img_PlayerHp.transform.localPosition = new Vector3(-1080 + 1080 * (playerNowHP / playerMaxHP), 0, 0);
    }

    /// <summary>
    /// 顯示轉珠時間
    /// </summary>
    public void ShowTurnBeatsTime()
    {
        img_TurnBeatsTime.gameObject.SetActive(true);
    }

    /// <summary>
    /// 關閉轉珠時間
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoCloseTurnBeatsTime()
    {
        img_TurnBeatsTime.gameObject.SetActive(false);
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// 轉珠時間初始化
    /// </summary>
    private void TimeInit()
    {
        totalTime = 10f;
        nowTime = 10f;
    }

    /// <summary>
    /// 減少轉珠時間
    /// </summary>
    public void ReduceTime()
    {
        nowTime -= Time.deltaTime / 4;
        if (nowTime <= 0)
        {
            nowTime = 0;
        }
        img_Time.transform.localPosition = new Vector3(-1080 + 1080 * (nowTime / totalTime), 0, 0);
    }

    /// <summary>
    /// 重製轉珠時間
    /// </summary>
    public IEnumerator CoResetTime()
    {
        nowTime = 10f;
        yield return new WaitForSeconds(0);
    }


    /// <summary>
    /// 轉珠時間到
    /// </summary>
    public bool TurnBeatsTimesUp()
    {
        return nowTime <= 0f;
    }

    /// <summary>
    /// 隊伍初始化
    /// </summary>
    private void TeamInit()
    {
        PlayerTeamInit();
        PlayerInit();
        PlayerChAtkOrderInit();
        MonsterTeamInit();
        PlayerChAtkVfxInit();
        EnemyChAtkVfxInit();
        EnemyChHpInit();
        EnemyCharacterHpBarInit();
        PlayerHPInit();
        TimeInit();
        BtnInit();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {       
        TeamInit();
        nowBattle = 1;
        totalBattle = 3;
        atkOrder = 0;
        atkOrderUI = 0;
        enemyDeadCount = 0;
        txt_LevelExp.text = levelExp.ToString();
        txt_nowBattle.text = nowBattle.ToString();
        txt_totalBattle.text = totalBattle.ToString();
        IAmManager(this);
    }

    /// <summary>
    /// 設定玩家角色傷害 ballctrl 607
    /// </summary>
    public void SetPlayerCharacterDamage()
    {
        for (int i = 0; i < list_PlayerTeamMember.Count; i++)
        {
            //Debug.Log($"玩家隊伍成員清單 : {list_PlayerTeamMember.Count}");
            list_PlayerTeamMember[i].SetDamage(ballCtrl.GetTeamMemberTotalDMGList()[i]);
            //Debug.Log($"玩家角色第{i + 1}位對怪物造成的傷害 : {list_PlayerTeamMember[i].GetDamage()}");
        }
    }


    /// <summary>
    /// 攻擊怪物 ballctrl 233
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoAttackMonster()
    {
        for (int i = 0; i < list_PlayerTeamMember.Count; i++)
        {
            if (list_EnemyCharacter[nowBattle - 1][list_EnemyCharacter[nowBattle - 1].Count - 1].GetNowHP() <= 0) //該battle的最後1隻怪物已經沒血了
            {
                //先生成攻擊特效並改名子
                GameObject playerChAtkVfx = list_PlayerTeamMember[i].InstAttackVFX(list_PlayerChAtkVfxPos[i], list_PlayerChATKOrder[i][atkOrder], list_PlayerChATKOrderBoss[i][0], nowBattle);
                playerChAtkVfx.transform.name = $"PlayerChAtkVfx{i + 1}";
                //只能繼續打最後1隻怪物
                list_PlayerTeamMember[i].Attack(list_EnemyCharacter[nowBattle - 1][list_EnemyCharacter[nowBattle - 1].Count - 1]);
            }
            else
            {
                if (list_EnemyCharacter[nowBattle - 1][atkOrder].GetNowHP() <= 0) //order從0開始，檢查該order的怪物死了沒
                {
                    //該order的怪物死了，就打下一個order的怪物
                    atkOrder++;
                    //先生成攻擊特效並改名子
                    GameObject playerChAtkVfx = list_PlayerTeamMember[i].InstAttackVFX(list_PlayerChAtkVfxPos[i], list_PlayerChATKOrder[i][atkOrder], list_PlayerChATKOrderBoss[i][0], nowBattle);
                    playerChAtkVfx.transform.name = $"PlayerChAtkVfx{i + 1}";
                    //然後再扣怪物的血量
                    list_PlayerTeamMember[i].Attack(list_EnemyCharacter[nowBattle - 1][atkOrder]);
                }
                else
                {
                    //該order的怪物沒死，就繼續打該order的怪物

                    //先生成攻擊特效並改名子
                    GameObject playerChAtkVfx = list_PlayerTeamMember[i].InstAttackVFX(list_PlayerChAtkVfxPos[i], list_PlayerChATKOrder[i][atkOrder], list_PlayerChATKOrderBoss[i][0], nowBattle);
                    playerChAtkVfx.transform.name = $"PlayerChAtkVfx{i + 1}";
                    //然後再扣怪物的血量
                    list_PlayerTeamMember[i].Attack(list_EnemyCharacter[nowBattle - 1][atkOrder]);
                }
            }
            CheckMonsterHP();
            yield return new WaitForSeconds(0.3f);
        }       
        yield return new WaitForSeconds(1);
    }

    /// <summary>
    /// 攻擊玩家
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoAttackPlayer()
    {
        yield return CoSurviveEnemyAtkPlayer();              
    }

    /// <summary>
    /// 存活的怪物攻擊玩家
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoSurviveEnemyAtkPlayer()
    {
        for (int i = 0; i < list_EnemyCharacter[nowBattle - 1].Count; i++)  //檢查battle1的怪物還有沒有活著的
        {
            if (list_EnemyCharacter[nowBattle - 1][i].GetNowHP() > 0)
            {
                if (nowBattle == 3)
                {
                    GameObject bossAtkVfx = list_EnemyCharacter[nowBattle - 1][i].InstAttackVFX(bossAtkVfxPos, new Vector3(145, 0, 0), new Vector3(132, 0, 0), nowBattle);
                    bossAtkVfx.transform.name = $"EnemyChAtkVfx{i + 1}";
                    list_EnemyCharacter[nowBattle - 1][i].Attack(player);
                    yield return new WaitForSeconds(0.3f);
                }
                else
                {
                    //還活著的怪物攻擊玩家

                    //先生成攻擊特效並改名子
                    GameObject enemyChAtkVfx = list_EnemyCharacter[nowBattle - 1][i].InstAttackVFX(list_EnemyChAtkVfxPos[i], new Vector3(145, 0, 0), new Vector3(145, 0, 0), nowBattle);
                    enemyChAtkVfx.transform.name = $"EnemyChAtkVfx{i + 1}";
                    //然後再扣玩家的血量
                    list_EnemyCharacter[nowBattle - 1][i].Attack(player);
                    yield return new WaitForSeconds(0.3f);
                    //atkPlayer = true;
                }

            }
        }
    }

    /// <summary>
    /// 敵人角色攻擊特效初始化
    /// </summary>
    private void EnemyChAtkVfxInit()
    {
        list_EnemyChAtkVfxPos = new List<Vector3>();
        list_EnemyChAtkVfxPos.Add(new Vector3(-4, 7, 0));
        list_EnemyChAtkVfxPos.Add(new Vector3(5, 7, 0));
        bossAtkVfxPos = new Vector3(0.6f, 10.75f, 0);
    }

    /// <summary>
    /// 確認怪物血量
    /// </summary>
    public void CheckMonsterHP()
    {
        //Debug.Log($"怪物清單第{1}組的第{1}之的血量 : {list_EnemyCharacter[0][0].GetNowHP()}");
        //Debug.Log($"怪物清單第{1}組的第{2}之的血量 : {list_EnemyCharacter[0][1].GetNowHP()}");
    }    

    /// <summary>
    /// 攻擊特效
    /// </summary>
    public void AttackVFX()
    {
        //角色在創造出來的時候就要有攻擊特效prefab了
        
        #region 打boss
        /*
        打boss : 

        1號玩家角色
        特效開始位置 >> Vector3(-9, 3, 0)
        生成角度 >> Quaternion.Euler(new Vector3(-35, 90, 0)

        2號玩家角色
        特效開始位置 >> Vector3(-4.5f, 3, 0)
        生成角度 >> Quaternion.Euler(new Vector3(-50, 90, 0)

        3號玩家角色
        特效開始位置 >> Vector3(0, 3, 0)
        生成角度 >> Quaternion.Euler(new Vector3(-85, 90, 0)

        4號玩家角色
        特效開始位置 >> Vector3(4.5f, 3, 0)
        生成角度 >> Quaternion.Euler(new Vector3(-120, 90, 0)

        5號玩家角色
        特效開始位置 >> Vector3(9, 3, 0)
        生成角度 >> Quaternion.Euler(new Vector3(-140, 90, 0)
        */

        //if (Input.GetKeyDown(KeyCode.A)) 
        //{
        //    list_PlayerChAtkVfxIns[0] = Instantiate(list_PlayerChAtkVfx[0], new Vector3(-9, 3, 0), Quaternion.Euler(new Vector3(-33, 90, 0)));
        //    list_PlayerChAtkVfxIns[0].transform.name = "PlayerChAtkVfx01";
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    list_PlayerChAtkVfxIns[1] = Instantiate(list_PlayerChAtkVfx[0], new Vector3(-4.5f, 3, 0), Quaternion.Euler(new Vector3(-51, 90, 0)));
        //    list_PlayerChAtkVfxIns[1].transform.name = "PlayerChAtkVfx02";
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    list_PlayerChAtkVfxIns[2] = Instantiate(list_PlayerChAtkVfx[0], new Vector3(0, 3, 0), Quaternion.Euler(new Vector3(-85, 90, 0)));
        //    list_PlayerChAtkVfxIns[2].transform.name = "PlayerChAtkVfx03";
        //}

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    list_PlayerChAtkVfxIns[3] = Instantiate(list_PlayerChAtkVfx[0], new Vector3(4.5f, 3, 0), Quaternion.Euler(new Vector3(-122.5f, 90, 0)));
        //    list_PlayerChAtkVfxIns[3].transform.name = "PlayerChAtkVfx04";
        //}

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    list_PlayerChAtkVfxIns[4] = Instantiate(list_PlayerChAtkVfx[0], new Vector3(9, 3, 0), Quaternion.Euler(new Vector3(-143.5f, 90, 0)));
        //    list_PlayerChAtkVfxIns[4].transform.name = "PlayerChAtkVfx05";

        //}
        #endregion

        #region 打小怪
        /*
        玩家角色1號 : 
        特效開始位置 >> Vector3(-9, 3, 0) 
        打1號怪物的角度 >> Quaternion.Euler(new Vector3(-47.5f, 90, 0)
        打2號怪物的角度 >> Quaternion.Euler(new Vector3(-21.505f, 90, 0)

        玩家角色2號 : 
        特效開始位置 >> Vector3(-4.5f, 3, 0)
        打1號怪物的角度 >> Quaternion.Euler(new Vector3(-85f, 90, 0)
        打2號怪物的角度 >> Quaternion.Euler(new Vector3(-30, 90, 0)

        玩家角色3號 : 
        特效開始位置 >> Vector3(0, 3, 0)
        打1號怪物的角度 >> Quaternion.Euler(new Vector3(-126, 90, 0)
        打2號怪物的角度 >> Quaternion.Euler(new Vector3(-47.5f, 90, 0)

        玩家角色4號 : 
        特效開始位置 >> Vector3(4.5f, 3, 0)
        打1號怪物的角度 >> Quaternion.Euler(new Vector3(-147.2f, 90, 0)
        打2號怪物的角度 >> Quaternion.Euler(new Vector3(-85f, 90, 0)

        玩家角色5號 : 
        特效開始位置 >> Vector3(9, 3, 0)
        打1號怪物的角度 >> Quaternion.Euler(new Vector3(-157.2f, 90, 0)
        打2號怪物的角度 >> Quaternion.Euler(new Vector3(-126f, 90, 0)
        */

        if (Input.GetKeyDown(KeyCode.A))
        {
            list_PlayerChAtkVfxIns[0] = Instantiate(list_PlayerChAtkVfx[0], new Vector3(-9, 3, 0), Quaternion.Euler(new Vector3(-21.505f, 90, 0)));
            list_PlayerChAtkVfxIns[0].transform.name = "PlayerChAtkVfx01";
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            list_PlayerChAtkVfxIns[1] = Instantiate(list_PlayerChAtkVfx[0], new Vector3(-4.5f, 3, 0), Quaternion.Euler(new Vector3(-30, 90, 0)));
            list_PlayerChAtkVfxIns[1].transform.name = "PlayerChAtkVfx02";
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            list_PlayerChAtkVfxIns[2] = Instantiate(list_PlayerChAtkVfx[0], new Vector3(0, 3, 0), Quaternion.Euler(new Vector3(-47.5f, 90, 0)));
            list_PlayerChAtkVfxIns[2].transform.name = "PlayerChAtkVfx03";
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            list_PlayerChAtkVfxIns[3] = Instantiate(list_PlayerChAtkVfx[0], new Vector3(4.5f, 3, 0), Quaternion.Euler(new Vector3(-85f, 90, 0)));
            list_PlayerChAtkVfxIns[3].transform.name = "PlayerChAtkVfx04";
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            list_PlayerChAtkVfxIns[4] = Instantiate(list_PlayerChAtkVfx[0], new Vector3(9, 3, 0), Quaternion.Euler(new Vector3(-126f, 90, 0)));
            list_PlayerChAtkVfxIns[4].transform.name = "PlayerChAtkVfx05";
        }
        #endregion
    }

    /// <summary>
    /// 產生攻擊數字
    /// </summary>
    /// <param name="damage">傷害</param>
    /// <param name="vfxPos">產生位置</param>
    public void GenerateHitNumber(int damage, Vector3 vfxPos, PlayerCharacter playerCharacter)
    {        
        switch (playerCharacter)
        {
            case PlayerCharacter.Alice:
                GameObject obj_GenHitNumberVfxDark = Instantiate(obj_HitNumberVfxDarkPre, vfxPos, Quaternion.identity, HitVfxfa.transform);
                obj_GenHitNumberVfxDark.GetComponentInChildren<Text>().text = damage.ToString();
                break;
            case PlayerCharacter.Rogritte:
                GameObject obj_GenHitNumberVfxFire = Instantiate(obj_HitNumberVfxFirePre, vfxPos, Quaternion.identity, HitVfxfa.transform);
                obj_GenHitNumberVfxFire.GetComponentInChildren<Text>().text = damage.ToString();
                break;
            case PlayerCharacter.Keli:
                GameObject obj_GenHitNumberVfxWood = Instantiate(obj_HitNumberVfxWoodPre, vfxPos, Quaternion.identity, HitVfxfa.transform);
                obj_GenHitNumberVfxWood.GetComponentInChildren<Text>().text = damage.ToString();
                break;
            case PlayerCharacter.LonelySnow:
                GameObject obj_GenHitNumberVfxWater = Instantiate(obj_HitNumberVfxWaterPre, vfxPos, Quaternion.identity, HitVfxfa.transform);
                obj_GenHitNumberVfxWater.GetComponentInChildren<Text>().text = damage.ToString();
                break;
            case PlayerCharacter.Yuna:
                GameObject obj_GenHitNumberVfxLight = Instantiate(obj_HitNumberVfxLightPre, vfxPos, Quaternion.identity, HitVfxfa.transform);
                obj_GenHitNumberVfxLight.GetComponentInChildren<Text>().text = damage.ToString();
                break;
        }                      
    }

    /// <summary>
    /// 可以轉珠
    /// </summary>
    public void CanTurnBeats()
    {
        obj_CantTurnBeatsPanel.SetActive(false);
        ballCtrl.turnBeadsStatus = TurnBeadsStatus.WaitTurnBeads;
    }

    /// <summary>
    /// 按鈕初始化
    /// </summary>
    private void BtnInit()
    {
        btn_VictoryReturnMainMenu.onClick.AddListener(delegate () {
            //playerData = new PlayerData();
            LoadingPlayerDataArchive();
            UpdateSaveData();
            DeletePlayerDataArchive();
            PlayerDataArchive();
            GameLoop.inst.PlayStartMainMenuMusic();
            returnMainMenu = true;
        });

        btn_FailReturnMainMenu.onClick.AddListener(delegate () {
            returnMainMenu = true;
        });
    }


    public bool ReturnMainMenu()
    {
        return returnMainMenu;
    }


    public void SetParam()
    {
        returnMainMenu = false;
    }


    /// <summary>
    /// 玩家資料存檔
    /// </summary>
    private void PlayerDataArchive()
    {
        //儲存背包資料(最新的背包資料)
        j_PlayerData = JsonUtility.ToJson(playerData); //把玩家資料轉成Json資料，放到字串裡面
        PlayerPrefs.SetString("玩家資料", j_PlayerData); //設定要儲存的資料名稱(key) , 資料內容(value)
        PlayerPrefs.Save(); //存檔
        Debug.Log("玩家資料以儲存");
    }

    /// <summary>
    /// 刪除玩家資料存檔(舊資料)
    /// </summary>
    private void DeletePlayerDataArchive()
    {
        if (PlayerPrefs.HasKey("玩家資料"))
        {
            //把舊資料先刪除
            PlayerPrefs.DeleteKey("玩家資料");
            Debug.Log("玩家資料以清除");
        }
    }

    /// <summary>
    /// 讀取玩家資料存檔
    /// </summary>
    private void LoadingPlayerDataArchive()
    {
        if (PlayerPrefs.HasKey("玩家資料"))
        {
            j_PlayerData = PlayerPrefs.GetString("玩家資料"); //取得儲存的背包資料
            playerData = JsonUtility.FromJson<PlayerData>(j_PlayerData); //把背包資料(Json)轉成PlayerData腳本        
        }
        else
        {
            playerData = new PlayerData();
        }       
    }

    /// <summary>
    /// 更新要儲存的資料
    /// </summary>
    public void UpdateSaveData()
    {
        playerData.nowEXP += levelExp;

        //判斷是否升等
        if (playerData.nowEXP >= playerData.totalEXP)
        {
            playerData.Lv++;
            playerData.nowEXP = 0;
        }
    }

    /// <summary>
    /// 播放轉珠音效
    /// </summary>
    public void PlayTurnBeatsAudio()
    {
        if (changeNumber == 0)
        {
            if (audioSource.clip != null)
            {
                audioSource.clip = null;
            }
            audioSource.clip = list_TurnBeatBGM[0];
            audioSource.Play();
            //change = false;
            changeNumber = 1;
        }
        else if (changeNumber == 1)
        {
            if (audioSource2.clip != null)
            {
                audioSource2.clip = null;
            }
            audioSource2.clip = list_TurnBeatBGM[0];
            audioSource2.Play();
            //change = true;
            changeNumber = 2;
        }
        else if (changeNumber == 2)
        {
            if (audioSource3.clip != null)
            {
                audioSource3.clip = null;
            }
            audioSource3.clip = list_TurnBeatBGM[0];
            audioSource3.Play();
            //change = true;
            changeNumber = 3;
        }
        else if (changeNumber == 3)
        {
            if (audioSource4.clip != null)
            {
                audioSource4.clip = null;
            }
            audioSource4.clip = list_TurnBeatBGM[0];
            audioSource4.Play();
            //change = true;
            changeNumber = 4;
        }
        else
        {
            if (audioSource5.clip != null)
            {
                audioSource5.clip = null;
            }
            audioSource5.clip = list_TurnBeatBGM[0];
            audioSource5.Play();
            //change = true;
            changeNumber = 0;
        }
    }

    /// <summary>
    /// 播放轉珠2音效
    /// </summary>
    public void PlayTurnBeats2Audio()
    {
        if (audioSource2.clip != null)
        {
            audioSource2.clip = null;
        }
        audioSource2.clip = list_TurnBeatBGM[0];
        audioSource2.Play();
    }

    /// <summary>
    /// 播放Combo音效
    /// </summary>
    public void PlayComboAudio()
    {
        if (audioSource.clip != null)
        {
            audioSource.clip = null;
        }
        audioSource.clip = list_TurnBeatBGM[1];
        audioSource.Play();
    }

    /// <summary>
    /// 播放玩家角色攻擊音效
    /// </summary>
    public void PlayPlayerChAtkSoundFx(int index)
    {
        if (index == 0)
        {
            AudioClip atkSoundFx = list_PlayerTeamMember[index].GetAtkSoundFx();
            audioSource.clip = atkSoundFx;
            audioSource.Play();
        }
        else if (index == 1)
        {
            AudioClip atkSoundFx = list_PlayerTeamMember[index].GetAtkSoundFx();
            audioSource2.clip = atkSoundFx;
            audioSource2.Play();
        }
        else if (index == 2)
        {
            AudioClip atkSoundFx = list_PlayerTeamMember[index].GetAtkSoundFx();
            audioSource3.clip = atkSoundFx;
            audioSource3.Play();
        }
        else if (index == 3)
        {
            AudioClip atkSoundFx = list_PlayerTeamMember[index].GetAtkSoundFx();
            audioSource4.clip = atkSoundFx;
            audioSource4.Play();
        }
        else
        {
            AudioClip atkSoundFx = list_PlayerTeamMember[index].GetAtkSoundFx();
            audioSource5.clip = atkSoundFx;
            audioSource5.Play();
        }
    }

    /// <summary>
    /// 比Start更早之前執行(請看生命週期)
    /// </summary>
    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //AttackVFX();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //genVfxNumber =  Instantiate(txt_VfxNumber, txt_VfxPos, Quaternion.identity, HitVfxfa.transform);
        }
    }
}
