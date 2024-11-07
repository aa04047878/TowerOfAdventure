using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerOfAdventureGame 
{
    //單利模式 : 取得遊戲唯一物件，且便於呼叫。
    private static TowerOfAdventureGame _inst;
    public static TowerOfAdventureGame Inst
    {
        get
        {
            if(_inst == null)
            {
                _inst = new TowerOfAdventureGame();
            }
            return _inst;
        }
    }

    private TowerOfAdventureGame()
    {
        //把建構式設為私有成員，確保不會被別的腳本new，以免出現第二個腳本
        //呼叫時要透過Inst >>  TowerOfAdventureGame.Inst.
    }

    //遊戲主要類別的實作，將子系統定義成類別成員
    private UserInfoUI m_UserInfoUI = null;
    private TeamInfoUI m_TeamInfoUI = null;
    private BackpackInfoUI m_BackpackInfoUI = null;
    private ShopInfoUI m_ShopInfoUI = null;
    private SettingInfoUI m_SettingInfoUI = null;
    private WaterLevelInfoUI m_WaterLevelInfoUI = null;
    private FireLevelInfoUI m_FireLevelInfoUI = null;
    private WoodLevelInfoUI m_WoodLevelInfoUI = null;
    private LightLevelInfoUI m_LightLevelInfoUI = null;
    private DarkLevelInfoUI m_DarkLevelInfoUI = null;
    private DrawCardSystem m_DrawCardSystem = null;

    private Object o_BackpackCardContent;
    private BackpackCardContent m_BackpackCardContent = null;
    private GameEventSystem m_GameEventSystem = null;

    public EnterLevel m_EnterLevel;


    /// <summary>
    /// 初始化
    /// </summary>
    public void Initinal() //外觀模式
    {

        m_UserInfoUI = new UserInfoUI(this);
        m_GameEventSystem = new GameEventSystem(this);
        m_TeamInfoUI = new TeamInfoUI(this);
        m_BackpackCardContent = new BackpackCardContent(this);

        m_ShopInfoUI = new ShopInfoUI(this);
        m_SettingInfoUI = new SettingInfoUI(this);
        m_DrawCardSystem = new DrawCardSystem(this);
        m_BackpackInfoUI = new BackpackInfoUI(this);

        m_WaterLevelInfoUI = new WaterLevelInfoUI(this);
        m_FireLevelInfoUI = new FireLevelInfoUI(this);
        m_WoodLevelInfoUI = new WoodLevelInfoUI(this);
        m_LightLevelInfoUI = new LightLevelInfoUI(this);
        m_DarkLevelInfoUI = new DarkLevelInfoUI(this);
        m_EnterLevel = UITool.FindGameComponent<EnterLevel>("Canvas");
        ResigerGameEvent();
    }

    public void Update()
    {
        m_BackpackInfoUI.Update();
        m_TeamInfoUI.Update();
        m_WaterLevelInfoUI.Update();
        m_FireLevelInfoUI.Update();
        m_WoodLevelInfoUI.Update();
        m_LightLevelInfoUI.Update();
        m_DarkLevelInfoUI.Update();
    }

    /// <summary>
    /// 隊伍資訊打開
    /// </summary>
    public void TeamInfoOpen()
    {
        m_TeamInfoUI.TeamInfoOpen();
    }

    /// <summary>
    /// 隊伍資訊關閉
    /// </summary>
    public void TeamInfoClose()
    {
        m_TeamInfoUI.TeamInfoClose();
    }


    public CardInfo GetCardInfo()
    {
        return m_BackpackInfoUI.GetCardInfo();
    }

    /// <summary>
    /// 取得隊伍資訊紅隊成員代表抽到第幾張卡
    /// </summary>
    /// <param name="Members"></param>
    /// <returns></returns>
    public int GetTeamInfoRedTeamMembers(int Members)
    {
        return m_TeamInfoUI.GetRedTeamMembers(Members);
    }

    /// <summary>
    /// 取得隊伍資訊的編輯隊伍
    /// </summary>
    /// <returns></returns>
    public GameObject GetTeamInfoEditorTeam()
    {
        return m_TeamInfoUI.GetEditorTeam();
    }

    /// <summary>
    /// 取得紅隊成員清單
    /// </summary>
    /// <returns></returns>
    public List<int> GetRedTeamMembersList()
    {
        return m_TeamInfoUI.GetRedTeamMembersList();
    }

    /// <summary>
    /// 取得隊伍資料
    /// </summary>
    /// <returns></returns>
    public TeamData GetTeamData()
    {
        return m_TeamInfoUI.GetTeamData();
    }


    /// <summary>
    /// 取得選擇編輯隊伍成員清單(照片)
    /// </summary>
    /// <returns></returns>
    public Sprite GetSelectEditorTeamItemList(int i)
    {
        return m_TeamInfoUI.GetSelectEditorTeamItemList(i);
    }

    /// <summary>
    /// 更新戰鬥準備隊伍UI
    /// </summary>
    public void UpdateBattleReadyTeamUI()
    {
        m_WaterLevelInfoUI.UpdateBattleReadyTeamUI();
    }

    /// <summary>
    /// 更新戰鬥準備隊伍UI火關
    /// </summary>
    public void UpdateBattleReadyTeamUIFireLevel()
    {
        m_FireLevelInfoUI.UpdateBattleReadyTeamUI();
    }

    /// <summary>
    /// 更新戰鬥準備隊伍UI木關
    /// </summary>
    public void UpdateBattleReadyTeamUIWoodLevel()
    {
        m_WoodLevelInfoUI.UpdateBattleReadyTeamUI();
    }

    /// <summary>
    /// 更新戰鬥準備隊伍UI光關
    /// </summary>
    public void UpdateBattleReadyTeamUILightLevel()
    {
        m_LightLevelInfoUI.UpdateBattleReadyTeamUI();
    }

    /// <summary>
    /// 更新戰鬥準備隊伍UI暗關
    /// </summary>
    public void UpdateBattleReadyTeamUIDarkLevel()
    {
        m_DarkLevelInfoUI.UpdateBattleReadyTeamUI();
    }

    /// <summary>
    /// 取得戰鬥準備隊伍
    /// </summary>
    /// <returns></returns>
    public List<Image> GetBattleReadyTeam()
    {
        return m_WaterLevelInfoUI.GetBattleReadyTeam();
    }

    /// <summary>
    /// 取得戰鬥準備隊伍火關
    /// </summary>
    /// <returns></returns>
    public List<Image> GetBattleReadyTeamFireLevel()
    {
        return m_FireLevelInfoUI.GetBattleReadyTeam();
    }

    /// <summary>
    /// 取得戰鬥準備隊伍木關
    /// </summary>
    /// <returns></returns>
    public List<Image> GetBattleReadyTeamWoodLevel()
    {
        return m_WoodLevelInfoUI.GetBattleReadyTeam();
    }

    /// <summary>
    /// 取得戰鬥準備隊伍光關
    /// </summary>
    /// <returns></returns>
    public List<Image> GetBattleReadyTeamLightLevel()
    {
        return m_LightLevelInfoUI.GetBattleReadyTeam();
    }

    /// <summary>
    /// 取得戰鬥準備隊伍暗關
    /// </summary>
    /// <returns></returns>
    public List<Image> GetBattleReadyTeamDarkLevel()
    {
        return m_DarkLevelInfoUI.GetBattleReadyTeam();
    }

    /// <summary>
    /// 背包資訊打開
    /// </summary>
    public void BackpackInfoOpen()
    {
        m_BackpackInfoUI.BackpackInfoOpen();
    }

    /// <summary>
    /// 背包資訊關閉
    /// </summary>
    public void BackpackInfoClose()
    {
        m_BackpackInfoUI.BackpackInfoClose();
    }


    public GameObject GetTeamInfoShowBackpackCardContent()
    {
        return m_TeamInfoUI.GetShowBackpackCardContent();
    }

    /// <summary>
    /// 商店資訊打開
    /// </summary>
    public void ShopInfoOpen()
    {
        m_ShopInfoUI.ShopInfoOpen();
    }

    /// <summary>
    /// 設定資訊打開
    /// </summary>
    public void SettingInfoOpen()
    {
        m_SettingInfoUI.SettingInfoOpen();
    }

    /// <summary>
    /// 水關卡資訊打開
    /// </summary>
    public void WaterLevelInfoOpen()
    {
        m_WaterLevelInfoUI.WaterLevelInfoOpen();
    }

    /// <summary>
    /// 火關卡資訊打開
    /// </summary>
    public void FireLevelInfoOpen()
    {
        m_FireLevelInfoUI.FireLevelInfoOpen();
    }

    /// <summary>
    /// 木關卡資訊打開
    /// </summary>
    public void WoodLevelInfoOpen()
    {
        m_WoodLevelInfoUI.WoodLevelInfoOpen();
    }

    /// <summary>
    /// 光關卡資訊打開
    /// </summary>
    public void LightLevelInfoOpen()
    {
        m_LightLevelInfoUI.LightLevelInfoOpen();
    }

    /// <summary>
    /// 暗關卡資訊打開
    /// </summary>
    public void DarkLevelInfoOpen()
    {
        m_DarkLevelInfoUI.DarkLevelInfoOpen();
    }

    /// <summary>
    /// 進入關卡動畫
    /// </summary>
    public void EnterLevelAni()
    {
        //火關要分開寫(明天再做)
        m_UserInfoUI.EnterLevelBGOpen();
        m_EnterLevel.EnterLevelAni();
    }

    /// <summary>
    /// 進入火關01動畫
    /// </summary>
    public void EnterFireLevel01Ani()
    {
        m_UserInfoUI.EnterLevelBGOpen();
        m_EnterLevel.EnterFireLevel01Ani();
    }

    /// <summary>
    /// 進入木關01動畫
    /// </summary>
    public void EnterWoodLevel01Ani()
    {
        m_UserInfoUI.EnterLevelBGOpen();
        m_EnterLevel.EnterWoodLevel01Ani();
    }

    /// <summary>
    /// 進入光關01動畫
    /// </summary>
    public void EnterLightLevel01Ani()
    {
        m_UserInfoUI.EnterLevelBGOpen();
        m_EnterLevel.EnterLightLevel01Ani();
    }

    /// <summary>
    /// 進入暗關01動畫
    /// </summary>
    public void EnterDarkLevel01Ani()
    {
        m_UserInfoUI.EnterLevelBGOpen();
        m_EnterLevel.EnterDarkLevel01Ani();
    }

    /// <summary>
    /// 進入水關第1關
    /// </summary>
    /// <returns></returns>
    public bool GetEnterWaterLevel1()
    {
        return m_EnterLevel.GetEnterWaterLevel01();
    }

    /// <summary>
    /// 進入火關第1關
    /// </summary>
    /// <returns></returns>
    public bool GetEnterFireLevel1()
    {
        return m_EnterLevel.GetEnterFireLevel01();
    }

    /// <summary>
    /// 進入木關第1關
    /// </summary>
    /// <returns></returns>
    public bool GetEnterWoodLevel1()
    {
        return m_EnterLevel.GetEnterWoodLevel01();
    }

    /// <summary>
    /// 進入光關第1關
    /// </summary>
    /// <returns></returns>
    public bool GetEnterLightLevel1()
    {
        return m_EnterLevel.GetEnterLightLevel01();
    }

    /// <summary>
    /// 進入暗關第1關
    /// </summary>
    /// <returns></returns>
    public bool GetEnterDarkLevel1()
    {
        return m_EnterLevel.GetEnterDarkLevel01();
    }

    /// <summary>
    /// 設定參數
    /// </summary>
    public void SetParam()
    {
        m_WaterLevelInfoUI.SetParam();
    }

    /// <summary>
    /// 設定參數火關卡
    /// </summary>
    public void SetParamFireLevel()
    {
        m_FireLevelInfoUI.SetParam();
    }

    /// <summary>
    /// 取得背包布林
    /// </summary>
    /// <returns></returns>
    public bool GetBackpackBool()
    {
        return m_BackpackInfoUI.GetBool();
    }

    /// <summary>
    /// 取得隊伍布林
    /// </summary>
    /// <returns></returns>
    public bool GetTeamBool()
    {
        return m_TeamInfoUI.GetBool();
    }

    

    /// <summary>
    /// 抽卡一次結果
    /// </summary>
    public void DrawCardOnceResult()
    {
        m_DrawCardSystem.DrawCardOnceResult();
    }

    /// <summary>
    /// 抽卡畫面關閉
    /// </summary>
    public void DrawCardPictureClose()
    {
        m_DrawCardSystem.DrawCardPictureClose();
    }


    /// <summary>
    /// 增加背包項目
    /// </summary>
    /// <param name="i"></param>
    public void AddBackpackItem(int i)
    {
        //m_BackpackInfoUI.AddBackpackItem(i);
    }

    /// <summary>
    /// 增加背包內容
    /// </summary>
    /// <param name="i"></param>
    public void AddBackpackContent(int i, ENUM_Behavior behavior)
    {
        m_BackpackCardContent.AddBackpackContent(i, behavior);
    }

    /// <summary>
    /// 取得背包資料
    /// </summary>
    /// <returns></returns>
    public BackpackData GetBackpackData()
    {
        return m_BackpackInfoUI.GetBackpackData();
    }

    /// <summary>
    /// 取得背包資料(Json)
    /// </summary>
    /// <returns></returns>
    public string GetBackpackDataJson()
    {
        return m_BackpackInfoUI.GetBackpackDataJson();
    }

    /// <summary>
    /// 取得卡片內容父物件
    /// </summary>
    /// <returns></returns>
    public GameObject GetCardContentParent()
    {
        return m_TeamInfoUI.GetCardContentParent();
    }

    /// <summary>
    /// 取得卡片清單編號多少的物體
    /// </summary>
    /// <param name="index">編號</param>
    /// <returns></returns>
    public GameObject GetListObj_PreInstantIndex(int index)
    {
        return m_BackpackCardContent.GetListObj_PreInstantIndex(index);
    }

    /// <summary>
    /// 取得卡片清單
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetListObj_PreInstant()
    {
        return m_BackpackCardContent.GetListObj_PreInstant();
    }


    /// <summary>
    /// 讀取生成卡片物件
    /// </summary>
    /// <param name="openBackpackContentButton"></param>
    public void LoadingInstantiateCard(int openBackpackContentButton, int cardDataIndex)
    {
        m_BackpackCardContent.LoadingInstantiateCard(openBackpackContentButton, cardDataIndex);
    }

    /// <summary>
    /// 讀取背包內容
    /// </summary>
    /// <param name="i"></param>
    public void LoadingBackpackContent(int i)
    {
        m_BackpackCardContent.LoadingBackpackContent(i);
    }

    /// <summary>
    /// 取得卡片內容清單
    /// </summary>
    /// <returns></returns>
    public List<CardContent> GetCardContentList()
    {
        return m_BackpackCardContent.GetCardContentList();
    }

    /// <summary>
    /// 叫遊戲事件系統去通知主題更新
    /// </summary>
    /// <param name="emGameEvnet"></param>
    public void NotifySubject(ENUM_GameEvent emGameEvnet, int cardDataIndex, ENUM_Behavior behavior)
    {
        m_GameEventSystem.NotifySubject(emGameEvnet, cardDataIndex, behavior);
    }

    /// <summary>
	/// 註冊遊戲事件系統
	/// </summary>
	private void ResigerGameEvent()
    {
        m_GameEventSystem.RegisterObserver(ENUM_GameEvent.AddBackpackContent, new AddBackpackContentObserverBCC(m_BackpackCardContent));
        m_GameEventSystem.RegisterObserver(ENUM_GameEvent.AddBackpackContent, new AddBackpackContentObserverUI(m_BackpackInfoUI));       
        m_GameEventSystem.RegisterObserver(ENUM_GameEvent.AddBackpackContent, new AddBackpackContentObserverTeamUI(m_TeamInfoUI));       

    }
}
