using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerData : MonoBehaviour
{
    public int Lv;
    public Text txt_Lv;
    public float nowEXP;
    public float totalEXP;
    public float percentNumber;
    public Image img_EXP;
    public Image img_EXPBar;
    public Text percentInfo;
    public PlayerData playerData;
    private string j_PlayerData;
    public string playerName;
    public Text txt_playerName;
    /// <summary>
    /// 讀取玩家資料存檔
    /// </summary>
    private void LoadingPlayerDataArchive()
    {
        if (PlayerPrefs.HasKey("玩家資料"))
        {
            j_PlayerData = PlayerPrefs.GetString("玩家資料"); //取得儲存的背包資料
            playerData = JsonUtility.FromJson<PlayerData>(j_PlayerData); //把背包資料(Json)轉成PlayerData腳本
                                                                         
            #region 灌值
            Lv = playerData.Lv;
            txt_Lv.text = playerData.Lv.ToString();
            nowEXP = playerData.nowEXP;
            totalEXP = playerData.totalEXP;
            img_EXPBar.transform.localPosition = new Vector3(-275 + 275 * (nowEXP / totalEXP), 0, 0);
            percentNumber = nowEXP / totalEXP;
            percentInfo.text = (percentNumber * 100).ToString();
            playerName = playerData.playerName;
            txt_playerName.text = playerName;
            #endregion
        }
        else
        {
            txt_playerName.text = "神抄之塔";
            Lv = 1;
            nowEXP = 0;
            totalEXP = 500;
            img_EXPBar.transform.localPosition = new Vector3(-275 + 275 * (nowEXP / totalEXP), 0, 0);
            percentNumber = nowEXP / totalEXP;
            percentInfo.text = (percentNumber * 100).ToString();
        }
    }
    
    public void SetPlayerName(string newName)
    {
        txt_playerName.text = newName;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadingPlayerDataArchive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
