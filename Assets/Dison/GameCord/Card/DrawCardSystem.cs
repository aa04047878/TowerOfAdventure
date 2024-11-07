using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCardSystem : IGameSystem
{
    private Image showCard = null;
    private List<Card> cardPool = null;
    private Object cardInfo = null;
    private CardInfo _cardInfo = null;
    private Text characterName = null;
    /// <summary>
    /// 屬性
    /// </summary>
    private Image Attr = null;
    private Text space = null;
    private Image character = null;
    private Text characterLevel = null;
    private Text HP = null;
    private Text recover = null;
    private Text ATK = null;
    private Text race = null;
    private Text activeCD = null;
    private Text activeLV = null;
    private Text active = null;
    private Text leader = null;


    //private GameObject obj_showCardS;
    //private Text characterNameS = null;
    //private Image AttrS = null;
    //private Text spaceS = null;
    //private Image characterS = null;
    //private Text characterLevelS = null;
    //private Text HPS = null;
    //private Text recoverS = null;
    //private Text ATKS = null;
    //private Text raceS = null;
    //private Text activeCDS = null;
    //private Text activeLVS = null;
    //private Text activeS = null;
    //private Text leaderS = null;

    private GameObject obj_DrawCard;
    private GameObject obj_ShowCardContent;
    private GameObject obj_ShowCardInfo;

    private float ChWidth;
    private float ChHeight;

    public DrawCardSystem(TowerOfAdventureGame TOAGame) : base(TOAGame)
    {
        Initialize();
    }
    public override void Initialize()
    {
        //showCard = GameObject.Find("ShowCard");
        //showCard.SetActive(false); //無法對它進行物件開關
        //showCard.transform.position = new Vector3(0, 0, 0); //但可以移動位置
        //showCard = UITool.FindChildObjectComponent<Image>("Canvas_DrawCard", "ShowCard");
        //showCard.transform.gameObject.SetActive(false);
        //showCard.transform.position = new Vector3(0, 0, 0); //但可以移動位置
        AddCardPool();
        
        cardInfo = (CardInfo)Resources.Load("ScriptableObject/CardInfo", typeof(CardInfo)); 
        _cardInfo = (CardInfo)cardInfo;

        #region 原本抽卡顯示的地方(取值的地方要更改)
        //characterName = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "CharacterName");  
        //Attr = UITool.FindChildObjectComponent<Image>("ShowCardInfo", "Attr");
        //space = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "Space");
        //character = UITool.FindChildObjectComponent<Image>("ShowCardContent", "Character");  
        //characterLevel = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "CharacterLevel");
        //HP = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "HP");
        //recover = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "Recover");
        //ATK = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "ATK");
        //race = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "Race");
        //activeCD = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "ActiveCD");
        //activeLV = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "ActiveLV");
        //active = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "Active");
        //leader = UITool.FindChildObjectComponent<Text>("ShowCardInfo", "Leader");
        #endregion

        #region keli LonelySnow的抽卡顯示部分 (用不到了)
        //keli LonelySnow的抽卡顯示部分
        //obj_showCardS = GameObject.Find("ShowCardS");
        //characterNameS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "CharacterName");
        //AttrS = UITool.FindChildObjectComponent<Image>("ShowCardInfoS", "Attr");
        //spaceS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "Space");
        //characterS = UITool.FindChildObjectComponent<Image>("ShowCardContentS", "Character");
        //characterLevelS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "CharacterLevel");
        //HPS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "HP");
        //recoverS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "Recover");
        //ATKS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "ATK");
        //raceS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "Race");
        //activeCDS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "ActiveCD");
        //activeLVS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "ActiveLV");
        //activeS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "Active");
        //leaderS = UITool.FindChildObjectComponent<Text>("ShowCardInfoS", "Leader");
        #endregion

        #region 現在顯示的地方
        obj_DrawCard = UITool.FindHiddenChildObject("Canvas", "DrawCard");
        obj_ShowCardContent = obj_DrawCard.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).gameObject;
        obj_ShowCardInfo = obj_ShowCardContent.transform.GetChild(1).gameObject;

        characterName = obj_ShowCardInfo.transform.Find("CharacterName").GetComponent<Text>();
        Attr = obj_ShowCardInfo.transform.Find("Attr").GetComponent<Image>();
        space = obj_ShowCardInfo.transform.Find("Space").GetComponent<Text>();
        character = obj_ShowCardContent.transform.Find("Character").GetComponent<Image>();
        characterLevel = obj_ShowCardInfo.transform.Find("CharacterLevel").GetComponent<Text>();
        HP = obj_ShowCardInfo.transform.Find("HP").GetComponent<Text>();
        recover = obj_ShowCardInfo.transform.Find("Recover").GetComponent<Text>();
        ATK = obj_ShowCardInfo.transform.Find("ATK").GetComponent<Text>();
        race = obj_ShowCardInfo.transform.Find("Race").GetComponent<Text>();
        activeCD = obj_ShowCardInfo.transform.Find("ActiveCD").GetComponent<Text>();
        activeLV = obj_ShowCardInfo.transform.Find("ActiveLV").GetComponent<Text>();
        active = obj_ShowCardInfo.transform.Find("Active").GetComponent<Text>();
        leader = obj_ShowCardInfo.transform.Find("Leader").GetComponent<Text>();
        #endregion
        ChWidth = character.GetComponent<RectTransform>().rect.width;
        ChHeight = character.GetComponent<RectTransform>().rect.height;
    }
    public override void Update()
    {

    }

    /// <summary>
    /// 單抽
    /// </summary>
    /// <returns></returns>
    public int SingleDraw()
    {
        return Random.Range(0, cardPool.Count);
    }

    /// <summary>
    /// 單抽結果
    /// </summary>
    /// <returns></returns>
    public Card SingleDrawResult(int i)
    {
        //int singleDrawResult = SingleDraw();
        //Debug.Log("singleDrawResult : " + singleDrawResult);
        return cardPool[i];
    }

    /// <summary>
    /// 抽卡一次結果
    /// </summary>
    public void DrawCardOnceResult()
    {
        Debug.Log("_cardInfo.cardData.Count : " + _cardInfo.cardData.Count);
        int Result = SingleDraw();
        for (int i = 0; i < _cardInfo.cardData.Count; i++)
        {
            if (SingleDrawResult(Result) == cardPool[i])
            {
                Debug.Log("i : " + i);
                //生成物件應該要寫在這裡，這裡才有資料
                if (i == 2 || i == 3)
                {
                    //ChWidth = 600;
                    //ChHeight = 800;
                    character.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 800);
                }
                else
                {
                    //obj_showCardS.SetActive(false);
                    //ChWidth = 734.6154f;
                    //ChHeight = 616.153f;
                    character.GetComponent<RectTransform>().sizeDelta = new Vector2(734.6154f, 616.153f);
                }

                //obj_showCardS.SetActive(false);
                characterName.text = _cardInfo.cardData[i].CharacterName;
                Attr.sprite = _cardInfo.cardData[i].Attr;
                space.text = _cardInfo.cardData[i].Space.ToString();
                character.sprite = _cardInfo.cardData[i].Character;
                characterLevel.text = _cardInfo.cardData[i].CharacterLevel.ToString();
                HP.text = _cardInfo.cardData[i].HP.ToString();
                recover.text = _cardInfo.cardData[i].Recover.ToString();
                ATK.text = _cardInfo.cardData[i].ATK.ToString();
                race.text = _cardInfo.cardData[i].Race;
                activeCD.text = _cardInfo.cardData[i].ActiveCD.ToString();
                activeLV.text = _cardInfo.cardData[i].ActiveLV.ToString();
                active.text = _cardInfo.cardData[i].Active;
                leader.text = _cardInfo.cardData[i].Leader;

                Debug.Log("抽1次");
                m_TOAGame.NotifySubject(ENUM_GameEvent.AddBackpackContent, i, ENUM_Behavior.DrawCard);
                //m_TOAGame.AddBackpackItem(i);
                //m_TOAGame.AddBackpackContent(i);
            }
        }
    }

    /// <summary>
    /// 新增卡池
    /// </summary>
    private void AddCardPool()
    {
        cardPool = new List<Card>();
        cardPool.Add(Card.Alice);
        cardPool.Add(Card.Rogritte);
        cardPool.Add(Card.Keli);
        cardPool.Add(Card.LonelySnow);
        cardPool.Add(Card.Yuna);
    }

    /// <summary>
    /// 抽卡畫面關閉
    /// </summary>
    public void DrawCardPictureClose()
    {
        obj_DrawCard.SetActive(false);
    }
}
