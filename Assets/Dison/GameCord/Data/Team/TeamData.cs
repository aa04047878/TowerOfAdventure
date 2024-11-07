using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TeamData 
{
    public List<Sprite> list_TeamImgData;
    /// <summary>
    /// 紅隊成員清單資料
    /// </summary>
    public List<int> list_RedTeamMembersData;
    /// <summary>
    /// 下方檢視選擇隊伍清單的卡片資料
    /// </summary>
    public List<CardData> list_SelectEditorTeamItemCardData;

    /// <summary>
    /// 下方檢視選擇隊伍清單的照片資料(index)
    /// </summary>
    public List<int> list_SelectEditorTeamItemImgIndexData;

    /// <summary>
    /// 選擇順序清單
    /// </summary>
    public List<int> list_SelectionOrder;

    /// <summary>
    /// 選擇照片活動清單
    /// </summary>
    public List<bool> list_SelectImgActivity;

    public TeamData()
    {
        list_TeamImgData = new List<Sprite>();
        list_RedTeamMembersData = new List<int>();
        list_SelectEditorTeamItemCardData = new List<CardData>();
        list_SelectionOrder = new List<int>();
        list_SelectImgActivity = new List<bool>();
        list_SelectEditorTeamItemImgIndexData = new List<int>();
    }
}
