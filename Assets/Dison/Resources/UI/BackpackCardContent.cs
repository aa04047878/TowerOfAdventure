using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackCardContent : IUserInterface
{    
    /// <summary>
    /// 生成物件後的卡片UI資料清單
    /// </summary>
    private List<CardContent> listCardContent = null;
    private Object cardInfo = null;
    private CardInfo _cardInfo = null;
    private GameObject obj_CardContent = null;
    private GameObject obj_CardContentS = null;
    public GameObject obj_CardContentParent;
    private GameObject obj_PreInstant;
    private GameObject obj_PreInstantCardInfo;
    private GameObject obj_PreInstantCardContant;


    private List<GameObject> listObj_PreInstant;

    public BackpackCardContent(TowerOfAdventureGame TOAGame) : base(TOAGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        listCardContent = new List<CardContent>();
        cardInfo = (CardInfo)Resources.Load("ScriptableObject/CardInfo", typeof(CardInfo)); //OK
        _cardInfo = (CardInfo)cardInfo;  //OK
        obj_CardContent = (GameObject)Resources.Load("Prefab/ItemCardContant");
        obj_CardContentS = (GameObject)Resources.Load("Prefab/ItemCardContantS");
        obj_CardContentParent = GameObject.Find("BackpackCardContent");
        listObj_PreInstant = new List<GameObject>();
    }

    /// <summary>
    /// 增加背包內容
    /// </summary>
    /// <param name="i"></param>
    public void AddBackpackContent(int i ,ENUM_Behavior behavior)
    {             
        if (behavior == ENUM_Behavior.DrawCard)
        {
            ////灌入抽卡結果
            listCardContent[listCardContent.Count - 1].characterName.text = _cardInfo.cardData[i].CharacterName;
            listCardContent[listCardContent.Count - 1].Attr.sprite = _cardInfo.cardData[i].Attr;
            listCardContent[listCardContent.Count - 1].space.text = _cardInfo.cardData[i].Space.ToString();
            listCardContent[listCardContent.Count - 1].character.sprite = _cardInfo.cardData[i].Character;
            listCardContent[listCardContent.Count - 1].characterLevel.text = _cardInfo.cardData[i].CharacterLevel.ToString();
            listCardContent[listCardContent.Count - 1].HP.text = _cardInfo.cardData[i].HP.ToString();
            listCardContent[listCardContent.Count - 1].recover.text = _cardInfo.cardData[i].Recover.ToString();
            listCardContent[listCardContent.Count - 1].ATK.text = _cardInfo.cardData[i].ATK.ToString();
            listCardContent[listCardContent.Count - 1].race.text = _cardInfo.cardData[i].Race;
            listCardContent[listCardContent.Count - 1].activeCD.text = _cardInfo.cardData[i].ActiveCD.ToString();
            listCardContent[listCardContent.Count - 1].activeLV.text = _cardInfo.cardData[i].ActiveLV.ToString();
            listCardContent[listCardContent.Count - 1].active.text = _cardInfo.cardData[i].Active;
            listCardContent[listCardContent.Count - 1].leader.text = _cardInfo.cardData[i].Leader;
        }
        
        if(behavior == ENUM_Behavior.Loading)
        {
            LoadingBackpackContent(i);
        }
    }

    /// <summary>
    /// 取得卡片清單編號多少的物體
    /// </summary>
    /// <param name="index">編號</param>
    /// <returns></returns>
    public GameObject GetListObj_PreInstantIndex(int index)
    {
        return listObj_PreInstant[index];
    }

    /// <summary>
    /// 取得卡片清單
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetListObj_PreInstant()
    {
        return listObj_PreInstant;
    }

    /// <summary>
    /// 生成卡片物件
    /// </summary>
    public void InstantiateCard(int openBackpackContentButton, int cardDataIndex)
    {
        Debug.Log("openBackpackContentButton : " + openBackpackContentButton);
        if (cardDataIndex == 2 || cardDataIndex == 3)
        {
            obj_PreInstant = Object.Instantiate(obj_CardContentS, new Vector3(0, 1, 90), Quaternion.identity, obj_CardContentParent.transform);
        }
        else
        {
            obj_PreInstant = Object.Instantiate(obj_CardContent, new Vector3(0, 1, 90), Quaternion.identity, obj_CardContentParent.transform);
        }
        
        listObj_PreInstant.Add(obj_PreInstant);
        //m_TOAGame.GetBackpackData().listObj_PreInstant.Add(obj_PreInstant);
        obj_PreInstant.name = $"Item{openBackpackContentButton}CardContant";
        Debug.Log("obj_PreInstant.transform.position : " + obj_PreInstant.transform.position);
        obj_PreInstantCardContant = obj_PreInstant.transform.GetChild(1).gameObject;
        obj_PreInstantCardInfo = obj_PreInstantCardContant.transform.GetChild(1).gameObject;
        obj_PreInstantCardContant.name = $"ShowItem{openBackpackContentButton}CardContant";
        obj_PreInstantCardInfo.name = $"ShowItem{openBackpackContentButton}CardInfo";

        listCardContent.Add(new CardContent());
        listCardContent[listCardContent.Count - 1].characterName = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "CharacterName");
        listCardContent[listCardContent.Count - 1].Attr = UITool.FindChildObjectComponent<Image>($"ShowItem{openBackpackContentButton}CardInfo", "Attr");
        listCardContent[listCardContent.Count - 1].space = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "Space");
        listCardContent[listCardContent.Count - 1].character = UITool.FindChildObjectComponent<Image>($"ShowItem{openBackpackContentButton}CardContant", "Character");
        listCardContent[listCardContent.Count - 1].characterLevel = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "CharacterLevel");
        listCardContent[listCardContent.Count - 1].HP = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "HP");
        listCardContent[listCardContent.Count - 1].recover = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "Recover");
        listCardContent[listCardContent.Count - 1].ATK = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "ATK");
        listCardContent[listCardContent.Count - 1].race = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "Race");
        listCardContent[listCardContent.Count - 1].activeCD = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "ActiveCD");
        listCardContent[listCardContent.Count - 1].activeLV = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "ActiveLV");
        listCardContent[listCardContent.Count - 1].active = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "Active");
        listCardContent[listCardContent.Count - 1].leader = UITool.FindChildObjectComponent<Text>($"ShowItem{openBackpackContentButton}CardInfo", "Leader");
    }

    /// <summary>
    /// 讀取生成卡片物件
    /// </summary>
    /// <param name="openBackpackContentButton"></param>
    public void LoadingInstantiateCard(int openBackpackContentButton, int cardDataIndex)
    {
        InstantiateCard(openBackpackContentButton, cardDataIndex);
    }

    /// <summary>
    /// 讀取背包內容
    /// </summary>
    /// <param name="i"></param>
    public void LoadingBackpackContent(int i)
    {
        Debug.Log("m_TOAGame.GetBackpackData().list_BackpackData.Count : " + m_TOAGame.GetBackpackData().list_BackpackData.Count); //會讀不到腳本內容(以下都一樣)
        listCardContent[listCardContent.Count - 1].characterName.text = m_TOAGame.GetBackpackData().list_BackpackData[i].CharacterName;
        //listCardContent[listCardContent.Count - 1].Attr.sprite = m_TOAGame.GetBackpackData().list_BackpackData[i].Attr;
        listCardContent[listCardContent.Count - 1].space.text = m_TOAGame.GetBackpackData().list_BackpackData[i].Space.ToString();
        //listCardContent[listCardContent.Count - 1].character.sprite = m_TOAGame.GetBackpackData().list_BackpackData[i].Character;
        listCardContent[listCardContent.Count - 1].characterLevel.text = m_TOAGame.GetBackpackData().list_BackpackData[i].CharacterLevel.ToString();
        listCardContent[listCardContent.Count - 1].HP.text = m_TOAGame.GetBackpackData().list_BackpackData[i].HP.ToString();
        listCardContent[listCardContent.Count - 1].recover.text = m_TOAGame.GetBackpackData().list_BackpackData[i].Recover.ToString();
        listCardContent[listCardContent.Count - 1].ATK.text = m_TOAGame.GetBackpackData().list_BackpackData[i].ATK.ToString();
        listCardContent[listCardContent.Count - 1].race.text = m_TOAGame.GetBackpackData().list_BackpackData[i].Race;
        listCardContent[listCardContent.Count - 1].activeCD.text = m_TOAGame.GetBackpackData().list_BackpackData[i].ActiveCD.ToString();
        listCardContent[listCardContent.Count - 1].activeLV.text = m_TOAGame.GetBackpackData().list_BackpackData[i].ActiveLV.ToString();
        listCardContent[listCardContent.Count - 1].active.text = m_TOAGame.GetBackpackData().list_BackpackData[i].Active;
        listCardContent[listCardContent.Count - 1].leader.text = m_TOAGame.GetBackpackData().list_BackpackData[i].Leader;
    }


    /// <summary>
    /// 取得卡片內容清單
    /// </summary>
    /// <returns></returns>
    public List<CardContent> GetCardContentList()
    {
        return listCardContent;
    }
}
