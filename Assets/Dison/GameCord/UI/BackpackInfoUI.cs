using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackInfoUI : IUserInterface
{
    private Button mBtn_Return = null;
    private Animator ani_Backpack = null;

    /// <summary>
    /// 背包內容的項目UI
    /// </summary>
    private List<Image> listBackpackitem;
    private Object cardInfo = null;
    private CardInfo _cardInfo = null;
    private List<Button> list_ItemsBtn;
    private bool detectItem01ContentClose = false;
    private GameObject showBackpackCardContent;
    public GameObject obj_CardContentParent;

    //背包資料
    private BackpackData backpackData;
    private string j_BackpackData;
    private GameObject testObject;
    private GameObject obj_CardContent;
    private bool loading;

    public BackpackInfoUI(TowerOfAdventureGame TOAGame):base(TOAGame)
    {
        Initialize();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        backpackData = new BackpackData();
        mBtn_Return = UITool.FindChildObjectComponent<Button>("Image_BackpackUPInterface", "Button_Return");
        ani_Backpack = UITool.FindGameComponent<Animator>("BackpackInfoUI");
        BackpackItemInit();
        BackpackButtonInit();
        cardInfo = (CardInfo)Resources.Load("ScriptableObject/CardInfo", typeof(CardInfo)); //OK
        _cardInfo = (CardInfo)cardInfo;  //OK
        showBackpackCardContent = GameObject.Find("ShowBackpackCardContent");
        obj_CardContentParent = GameObject.Find("BackpackCardContent");

        mBtn_Return.onClick.AddListener(delegate () {
            bool Teamb = m_TOAGame.GetTeamBool();
            bool Backpackb = ani_Backpack.GetBool("backpackopen");
            if (Teamb && Backpackb)
            {
                m_TOAGame.TeamInfoClose();
                ani_Backpack.SetBool("backpackopen", false);
            }
            else
            {
                ani_Backpack.SetBool("backpackopen", false);
            }
            
        });

        //讀取檔案
        obj_CardContent = (GameObject)Resources.Load("Prefab/ItemCardContant");

        //Debug.Log("自動載入存檔");
        //LoadingBackpackArchive();
    }

    /// <summary>
    /// 更新(unity的Update)
    /// </summary>
    public override void Update()
    {
        DetectItemContentClose();
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteBackpackArchive();
        }

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Debug.Log("手動載入存檔");
        //    LoadingBackpackArchive();
        //}

        if (!loading)
        {
            Debug.Log("手動載入存檔");
            LoadingBackpackArchive();
            loading = true;
        }
    }

    /// <summary>
    /// 取得背包資料
    /// </summary>
    public BackpackData GetBackpackData()
    {       
        if (backpackData == null)
        {
            j_BackpackData = PlayerPrefs.GetString("背包資料"); //取得儲存的背包資料
            backpackData = JsonUtility.FromJson<BackpackData>(j_BackpackData); //把背包資料(Json)轉成BackpackData腳本
        }
        return backpackData;
    }

    /// <summary>
    /// 取得背包資料(Json)
    /// </summary>
    /// <returns></returns>
    public string GetBackpackDataJson()
    {
        return j_BackpackData;
    }

    /// <summary>
    /// 背包資訊打開
    /// </summary>
    public void BackpackInfoOpen()
    {
        ani_Backpack.SetBool("backpackopen", true);
    }

    /// <summary>
    /// 背包資訊關閉
    /// </summary>
    public void BackpackInfoClose()
    {
        ani_Backpack.SetBool("backpackopen", false);
    }

    /// <summary>
    /// 取得布林
    /// </summary>
    /// <returns></returns>
    public bool GetBool()
    {
        return ani_Backpack.GetBool("backpackopen");
    }

    /// <summary>
    /// 增加背包項目
    /// </summary>
    /// <param name="i"></param>
    public void AddBackpackItem(int openBackpackContentButton, int CardDataIndex, ENUM_Behavior behavior)
    {        
        for (int i = 1; i <= 10; i++)
        {
            if (openBackpackContentButton == i)
            {              
                if (behavior == ENUM_Behavior.DrawCard)
                {
                    listBackpackitem[openBackpackContentButton - 1].sprite = _cardInfo.cardData[CardDataIndex].CharacterAvatar;
                    //儲存背包資料(過程)
                    backpackData.list_BackpackData.Add(new CardData());
                    backpackData.list_cardDataIndex.Add(CardDataIndex);                    
                    backpackData.list_BackpackData[i - 1].CharacterName = _cardInfo.cardData[CardDataIndex].CharacterName;
                    //backpackData.list_BackpackData[i - 1].Attr = _cardInfo.cardData[CardDataIndex].Attr;
                    backpackData.list_BackpackData[i - 1].Space = _cardInfo.cardData[CardDataIndex].Space;
                    //backpackData.list_BackpackData[i - 1].Character = _cardInfo.cardData[CardDataIndex].Character;
                    backpackData.list_BackpackData[i - 1].CharacterLevel = _cardInfo.cardData[CardDataIndex].CharacterLevel;
                    backpackData.list_BackpackData[i - 1].HP = _cardInfo.cardData[CardDataIndex].HP;
                    backpackData.list_BackpackData[i - 1].Recover = _cardInfo.cardData[CardDataIndex].Recover;
                    backpackData.list_BackpackData[i - 1].ATK = _cardInfo.cardData[CardDataIndex].ATK;
                    backpackData.list_BackpackData[i - 1].Race = _cardInfo.cardData[CardDataIndex].Race;
                    backpackData.list_BackpackData[i - 1].ActiveCD = _cardInfo.cardData[CardDataIndex].ActiveCD;
                    backpackData.list_BackpackData[i - 1].ActiveLV = _cardInfo.cardData[CardDataIndex].ActiveLV;
                    backpackData.list_BackpackData[i - 1].Active = _cardInfo.cardData[CardDataIndex].Active;
                    backpackData.list_BackpackData[i - 1].Leader = _cardInfo.cardData[CardDataIndex].Leader;
                    //backpackData.list_BackpackData[i - 1].CharacterAvatar = _cardInfo.cardData[CardDataIndex].CharacterAvatar;
                    backpackData.list_BackpackData[i - 1].playerCharacter = _cardInfo.cardData[CardDataIndex].playerCharacter;
                    backpackData.list_BackpackData[i - 1].elementType = _cardInfo.cardData[CardDataIndex].elementType;
                    Debug.Log("backpackData.list_BackpackData.Count : " + backpackData.list_BackpackData.Count);
                    DeleteBackpackArchive();
                    BackpackArchive();
                }

                if (behavior == ENUM_Behavior.Loading)
                {
                    listBackpackitem[openBackpackContentButton - 1].sprite = _cardInfo.cardData[backpackData.list_cardDataIndex[i - 1]].CharacterAvatar;
                    LoadingInstantiateCardContent(CardDataIndex, i);

                }
            }
        }
    }

    /// <summary>
    /// 增加背包內容(按鈕部分)
    /// </summary>
    public void AddBackpackContentButton(int openBackpackContentButton)
    {             
        for (int i = 1; i <= 10; i++)
        {
            if(openBackpackContentButton == i)
            {
                list_ItemsBtn[openBackpackContentButton - 1].onClick.AddListener(delegate () {
                    m_TOAGame.GetListObj_PreInstantIndex(openBackpackContentButton - 1).transform.SetParent(showBackpackCardContent.transform);
                    detectItem01ContentClose = true;
                });

                //加入儲存資料 (儲存的部分由AddBackpackItem執行就好)
                backpackData.list_openBackpackContentButton.Add(openBackpackContentButton);
            }
        }
    }

    /// <summary>
    /// 偵測項目內容關閉
    /// </summary>
    public void DetectItemContentClose()
    {
        if(detectItem01ContentClose)
        {
            if(Input.GetMouseButtonDown(0))
            {
                showBackpackCardContent.transform.GetChild(0).gameObject.transform.SetParent(obj_CardContentParent.transform);
                detectItem01ContentClose = false;
            }
        }
    }

    /// <summary>
    /// 背包項目初始化
    /// </summary>
    private void BackpackItemInit()
    {
        listBackpackitem = new List<Image>();      
        for (int i = 1; i <= 10; i++)
        {
            listBackpackitem.Add(UITool.FindGameComponent<Image>($"Item{i}"));
        }
    }

    /// <summary>
    /// 背包按鈕初始化
    /// </summary>
    private void BackpackButtonInit()
    {
        list_ItemsBtn = new List<Button>();
        for (int i = 1; i <= 10; i++)
        {
            list_ItemsBtn.Add(UITool.FindGameComponent<Button>($"Item{i}"));
        }
    }

    /// <summary>
    /// 背包存檔
    /// </summary>
    private void BackpackArchive()
    {
        //儲存背包資料(最新的背包資料)
        j_BackpackData = JsonUtility.ToJson(backpackData); //把背包資料轉成Json資料，放到字串裡面
        PlayerPrefs.SetString("背包資料", j_BackpackData); //設定要儲存的資料名稱(key) , 資料內容(value)
        PlayerPrefs.Save(); //存檔
        Debug.Log("背包資料以儲存");
    }

    /// <summary>
    /// 讀取背包存檔
    /// </summary>
    private void LoadingBackpackArchive()
    {      
        if (PlayerPrefs.HasKey("背包資料"))
        {
            j_BackpackData = PlayerPrefs.GetString("背包資料"); //取得儲存的背包資料
            backpackData = JsonUtility.FromJson<BackpackData>(j_BackpackData); //把背包資料(Json)轉成BackpackData腳本
            Debug.Log("backpackData.list_BackpackData.Count : " + backpackData.list_BackpackData.Count);

            #region 舊的寫法
            //for (int i = 0; i < backpackData.list_BackpackData.Count; i++)
            //{
            //    //把儲存的背包資料灌入背包裡面
            //    listBackpackitem[i].sprite = backpackData.list_BackpackData[i].CharacterAvatar;
            //}

            //for (int i = 0; i < backpackData.list_openBackpackContentButton.Count; i++)
            //{
            //    //m_TOAGame.GetListObj_PreInstant().Add(backpackData.listObj_PreInstant[i]);
            //    //testObject = Object.Instantiate(backpackData.listObj_PreInstant[i], new Vector3(0, 1, 90), Quaternion.identity, obj_CardContentParent.transform); //直接跳錯
            //    //testObject = Object.Instantiate(obj_CardContent, new Vector3(0, 1, 90), Quaternion.identity, obj_CardContentParent.transform); //有東西，但是都沒資料
            //    //backpackData.listObj_PreInstant[i].transform.SetParent(obj_CardContentParent.transform);
            //    //m_TOAGame.GetListObj_PreInstantIndex(i).transform.SetParent(obj_CardContentParent.transform);
            //    m_TOAGame.LoadingInstantiateCard(i);
            //    LoadingInstantiateCardContent(i);
            //    //m_TOAGame.LoadingBackpackContent(i); //只能自己做，不能在別的腳本做，別的腳本讀取不到backpackData的存在
            //}
            //////Debug.Log("m_TOAGame.GetListObj_PreInstant().Count : " + m_TOAGame.GetListObj_PreInstant().Count);

            //for (int i = 0; i < backpackData.list_openBackpackContentButton.Count; i++)
            //{
            //    int temp = i;
            //    list_ItemsBtn[temp].onClick.AddListener(delegate ()
            //    {
            //        m_TOAGame.GetListObj_PreInstantIndex(temp).transform.SetParent(showBackpackCardContent.transform);
            //        detectItem01ContentClose = true;
            //    });
            //}

            #endregion

            #region 新的寫法
            for (int i = 0; i < backpackData.list_BackpackData.Count; i++)
            {
                //不能再start的時候執行，只能在update的時候執行
                m_TOAGame.NotifySubject(ENUM_GameEvent.AddBackpackContent, i, ENUM_Behavior.Loading);
            }

            //m_TOAGame.NotifySubject(ENUM_GameEvent.AddBackpackContent, 0, ENUM_Behavior.Loading);
            #endregion
        }
    }

    /// <summary>
    /// 刪除背包存檔
    /// </summary>
    private void DeleteBackpackArchive()
    {
        if (PlayerPrefs.HasKey("背包資料"))
        {
            //把舊資料先刪除
            PlayerPrefs.DeleteKey("背包資料");
            Debug.Log("背包資料以清除");
        }
    }


    /// <summary>
    /// 讀取生成卡片內容
    /// </summary>
    private void LoadingInstantiateCardContent(int CardDataIndex, int i)
    {
        m_TOAGame.GetCardContentList()[CardDataIndex].characterName.text = backpackData.list_BackpackData[CardDataIndex].CharacterName;
        //m_TOAGame.GetCardContentList()[i].Attr.sprite = backpackData.list_BackpackData[i].Attr;
        m_TOAGame.GetCardContentList()[CardDataIndex].Attr.sprite = _cardInfo.cardData[backpackData.list_cardDataIndex[i - 1]].Attr;
        m_TOAGame.GetCardContentList()[CardDataIndex].space.text = backpackData.list_BackpackData[CardDataIndex].Space.ToString();
        //m_TOAGame.GetCardContentList()[i].character.sprite = backpackData.list_BackpackData[i].Character;
        m_TOAGame.GetCardContentList()[CardDataIndex].character.sprite = _cardInfo.cardData[backpackData.list_cardDataIndex[i - 1]].Character;
        m_TOAGame.GetCardContentList()[CardDataIndex].characterLevel.text = backpackData.list_BackpackData[CardDataIndex].CharacterLevel.ToString();
        m_TOAGame.GetCardContentList()[CardDataIndex].HP.text = backpackData.list_BackpackData[CardDataIndex].HP.ToString();
        m_TOAGame.GetCardContentList()[CardDataIndex].recover.text = backpackData.list_BackpackData[CardDataIndex].Recover.ToString();
        m_TOAGame.GetCardContentList()[CardDataIndex].ATK.text = backpackData.list_BackpackData[CardDataIndex].ATK.ToString();
        m_TOAGame.GetCardContentList()[CardDataIndex].race.text = backpackData.list_BackpackData[CardDataIndex].Race;
        m_TOAGame.GetCardContentList()[CardDataIndex].activeCD.text = backpackData.list_BackpackData[CardDataIndex].ActiveCD.ToString();
        m_TOAGame.GetCardContentList()[CardDataIndex].activeLV.text = backpackData.list_BackpackData[CardDataIndex].ActiveLV.ToString();
        m_TOAGame.GetCardContentList()[CardDataIndex].active.text = backpackData.list_BackpackData[CardDataIndex].Active;
        m_TOAGame.GetCardContentList()[CardDataIndex].leader.text = backpackData.list_BackpackData[CardDataIndex].Leader;
    }

    public CardInfo GetCardInfo()
    {
        return _cardInfo;
    }
}
