using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingInfoUI : IUserInterface
{
    private GameObject obj_SettingInfoUI;
    private Button btn_Return;

    #region 變更玩家名子
    private Button btn_ChangePlayerName;
    private GameObject obj_ChangePlayerName;
    private Button btn_ReturnChangeName;
    private Text newName;
    private Button Btn_ChangePlayerNameSure;
    private Button Btn_ChangePlayerNameCancel;
    private Button btn_ChangeFinishSure;
    private GameObject changeFinishPanel;
    #endregion

    #region 玩家資料
    public PlayerData playerData;
    private string j_PlayerData;
    private ShowPlayerData showPlayerData;
    #endregion

    #region 離開遊戲
    private Button btn_ExitGame;
    private GameObject obj_ExitGame;
    private Button btn_ExitGameSure;
    private Button btn_ExitGameCancel;
    private Button btn_ExitGameReturn;
    #endregion

    public SettingInfoUI(TowerOfAdventureGame TOAGame) : base(TOAGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        obj_SettingInfoUI = UITool.FindHiddenChildObject("Canvas", "SettingInfoUI");
        btn_Return = obj_SettingInfoUI.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        btn_ChangePlayerName = obj_SettingInfoUI.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        obj_ChangePlayerName = obj_SettingInfoUI.transform.GetChild(0).transform.GetChild(1).gameObject;
        btn_ReturnChangeName = obj_SettingInfoUI.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        newName = obj_SettingInfoUI.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        Btn_ChangePlayerNameSure = obj_SettingInfoUI.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<Button>();
        Btn_ChangePlayerNameCancel = obj_SettingInfoUI.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).GetComponent<Button>();
        changeFinishPanel = obj_SettingInfoUI.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(4).gameObject;
        btn_ChangeFinishSure = changeFinishPanel.transform.GetChild(1).GetComponent<Button>();
        showPlayerData = UITool.FindChildObjectComponent<ShowPlayerData>("Canvas", "UserInfoUI");
        obj_ExitGame = obj_SettingInfoUI.transform.GetChild(0).transform.GetChild(2).gameObject;
        btn_ExitGame = obj_SettingInfoUI.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Button>();
        btn_ExitGameSure = obj_ExitGame.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>();
        btn_ExitGameCancel = obj_ExitGame.transform.GetChild(0).transform.GetChild(2).GetComponent<Button>();
        btn_ExitGameReturn = obj_ExitGame.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();

        btn_Return.onClick.AddListener(delegate () {
            obj_SettingInfoUI.SetActive(false);
        });

        btn_ChangePlayerName.onClick.AddListener(delegate () {
            obj_ChangePlayerName.SetActive(true);
        });

        btn_ReturnChangeName.onClick.AddListener(delegate () {
            obj_ChangePlayerName.SetActive(false);
        });

        Btn_ChangePlayerNameSure.onClick.AddListener(delegate () {
            changeFinishPanel.SetActive(true);
            LoadingPlayerDataArchive();
            UpdatePlayerData();
            DeletePlayerDataArchive();
            PlayerDataArchive();

        });

        Btn_ChangePlayerNameCancel.onClick.AddListener(delegate () {
            obj_ChangePlayerName.SetActive(false);
        });

        btn_ChangeFinishSure.onClick.AddListener(delegate () {
            changeFinishPanel.SetActive(false);
        });

        btn_ExitGame.onClick.AddListener(delegate ()
        {
            obj_ExitGame.SetActive(true);
        });

        btn_ExitGameSure.onClick.AddListener(delegate () {
            Application.Quit(); //關閉遊戲
        });

        btn_ExitGameCancel.onClick.AddListener(delegate () {
            obj_ExitGame.SetActive(false);
        });

        btn_ExitGameReturn.onClick.AddListener(delegate () {
            obj_ExitGame.SetActive(false);
        });

        obj_ChangePlayerName.SetActive(false);
        obj_SettingInfoUI.SetActive(false);
        obj_ExitGame.SetActive(false);
    }

    /// <summary>
    /// 設定資訊打開
    /// </summary>
    public void SettingInfoOpen()
    {
        obj_SettingInfoUI.SetActive(true);
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
    /// 更新玩家資料
    /// </summary>
    private void UpdatePlayerData()
    {
        playerData.playerName = newName.text;
        showPlayerData.SetPlayerName(newName.text);
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
}
