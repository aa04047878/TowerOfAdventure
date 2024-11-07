using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserInfoUI : IUserInterface
{
    //玩家資料
    private PlayerData playerData;

    //下方UI
    private GameObject g_Name = null;    
    private GameObject g_Lv = null;  
    private GameObject gBtn_Team = null;
    private GameObject gBtn_Backpack = null;
    private GameObject gBtn_Shop = null;
    private GameObject gBtn_Setting = null;
    private GameObject gImage_Team = null;

    private Text m_Name = null;
    private Text m_Lv = null;
    private Button mBtn_Team = null;
    private Button mBtn_Backpack = null;
    private Button mBtn_Shop = null;
    private Button mBtn_Setting = null;
    private Image mImage_Team = null;

    //上方UI
    private Button mBtn_WaterLevel;
    private Button mBtn_FireLevel;
    private Button mBtn_WoodLevel;
    private Button mBtn_LightLevel;
    private Button mBtn_DarkLevel;

    //進入關卡BG
    private GameObject g_EnterLevelBG;


    public UserInfoUI(TowerOfAdventureGame TOAGame):base(TOAGame)
    {
        Initialize();
    }

    //初始化
    public override void Initialize()
    {               
        g_Lv = GameObject.Find("TextLevel");
        g_Name = GameObject.Find("Text_Name");
        gBtn_Team = GameObject.Find("Button_Team");
        gBtn_Backpack = GameObject.Find("Button_Backpack");
        gBtn_Shop = GameObject.Find("Button_Shop");
        gBtn_Setting = GameObject.Find("Button_Setting");
        //gImage_Team = UITool.FindHiddenChildObject("Canvas", "Image_Team");

        m_Lv = g_Lv.GetComponent<Text>();
        m_Name = g_Name.GetComponent<Text>();
        mBtn_Team = gBtn_Team.GetComponent<Button>();
        mBtn_Backpack = gBtn_Backpack.GetComponent<Button>();
        mBtn_Shop = gBtn_Shop.GetComponent<Button>();
        mBtn_Setting = gBtn_Setting.GetComponent<Button>();
        mBtn_WaterLevel = UITool.FindChildObjectComponent<Button>("Level", "Button_waterLevel");
        mBtn_FireLevel = UITool.FindChildObjectComponent<Button>("Level", "Button_fireLevel");
        mBtn_WoodLevel = UITool.FindChildObjectComponent<Button>("Level", "Button_treeLevel");
        mBtn_LightLevel = UITool.FindChildObjectComponent<Button>("Level", "Button_lightLevel");
        mBtn_DarkLevel = UITool.FindChildObjectComponent<Button>("Level", "Button_darkLevel");
        //ani_ImageTeam = gImage_Team.GetComponent<Animator>();
        //ani_ImageBackpack = UITool.FindGameComponent<Animator>("Image_Backpack");

        mBtn_Team.onClick.AddListener(delegate () {
            m_TOAGame.TeamInfoOpen();
        });

        mBtn_Backpack.onClick.AddListener(delegate () {
            m_TOAGame.BackpackInfoOpen();
        });

        mBtn_Shop.onClick.AddListener(delegate () {
            m_TOAGame.ShopInfoOpen();
        });

        mBtn_Setting.onClick.AddListener(delegate () {
            m_TOAGame.SettingInfoOpen();
        });

        mBtn_WaterLevel.onClick.AddListener(delegate () {
            m_TOAGame.WaterLevelInfoOpen();
        });

        mBtn_FireLevel.onClick.AddListener(delegate () {
            m_TOAGame.FireLevelInfoOpen();
        });

        mBtn_WoodLevel.onClick.AddListener(delegate () {
            m_TOAGame.WoodLevelInfoOpen();
        });

        mBtn_LightLevel.onClick.AddListener(delegate () {
            m_TOAGame.LightLevelInfoOpen();
        });

        mBtn_DarkLevel.onClick.AddListener(delegate () {
            m_TOAGame.DarkLevelInfoOpen();
        });

        g_EnterLevelBG = UITool.FindHiddenChildObject("Canvas", "EnterLevelBG");
        g_EnterLevelBG.SetActive(false);
    }

    public void EnterLevelBGOpen()
    {
        g_EnterLevelBG.SetActive(true);
    }
}
