using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInfoUI : IUserInterface
{
    #region 動畫
    private Animator ani_Shop = null;
    /// <summary>
    /// 抽卡畫面動畫
    /// </summary>
    private Animator ani_DrawCardOncePicture = null;
    private Animator ani_DrawCard = null;
    private GameObject obj_DrawCard;

    #endregion

    #region 返回鍵
    /// <summary>
    /// 返回主畫面
    /// </summary>
    private Button mBtn_Return = null;
    /// <summary>
    /// 商店裡封印卡介面返回鍵
    /// </summary>
    private Button mBtn_CardReturn = null;
    /// <summary>
    /// 商店裡魔法石封印介面返回鍵
    /// </summary>
    private Button mBtn_StoneDrawCardReturn = null;
    #endregion

    #region 魔法石商店相關
    /// <summary>
    /// 魔法石商店
    /// </summary>
    private Button mBtn_StoneShop = null;
    #endregion

    #region 封印卡相關
    /// <summary>
    /// 封印卡按鈕
    /// </summary>
    private Button mBtn_Card = null;
    /// <summary>
    /// 魔法石封印按鈕
    /// </summary>
    private Button mBtn_StoneDrawCard = null;

    /// <summary>
    /// 一次魔法石封印按鈕
    /// </summary>
    private Button mBtn_StoneDrawCardOnce = null;
    /// <summary>
    /// 確認抽卡一次訊息
    /// </summary>
    private GameObject confirmDrawCardOnceMessage = null;
    /// <summary>
    /// 取消抽卡一次按鈕
    /// </summary>
    private Button mBtn_CancelDrawCardOnce = null;
    /// <summary>
    /// 確認抽卡一次按鈕
    /// </summary>
    private Button mBtn_SureDrawCardOnce = null;

    /// <summary>
    /// 十次魔法石封印按鈕
    /// </summary>
    private Button mBtn_StoneDrawTenTimes = null;
    /// <summary>
    /// 抽卡畫面
    /// </summary>
    private GameObject drawCardOncePicture = null;
    #endregion

    #region 回復體力相關
    /// <summary>
    /// 回復體力按鈕
    /// </summary>
    private Button mBtn_RecoverEnergy = null;
    #endregion

    #region 背包空間相關
    /// <summary>
    /// 增加背包空間
    /// </summary>
    private Button mBtn_AddBackpack = null;
    #endregion

    #region 朋友相關
    /// <summary>
    /// 增加朋友上限
    /// </summary>
    private Button mBtn_AddFriends = null;
    #endregion

    #region 攝影機
    private Camera drawCardCamera;
    #endregion

    public ShopInfoUI(TowerOfAdventureGame TOAGame) : base(TOAGame)
    {
        Initialize();
    }

    public override void Initialize()
    {

        mBtn_Return = UITool.FindChildObjectComponent<Button>("Image_ShopUPInterface", "Button_Return");
        ani_Shop = UITool.FindGameComponent<Animator>("ShopInfoUI");
        mBtn_CardReturn = UITool.FindChildObjectComponent<Button>("Image_CardUPInterface", "Button_Return");
        mBtn_StoneDrawCardReturn = UITool.FindChildObjectComponent<Button>("Image_StoneDrawCardUPInterface", "Button_Return");
        //mBtn_StoneShop = UITool.FindGameComponent<Button>("Button_StoneShop");
        mBtn_Card = UITool.FindGameComponent<Button>("Button_Card");
        mBtn_StoneDrawCard = UITool.FindGameComponent<Button>("Button_StoneDrawCard");
        mBtn_StoneDrawCardOnce = UITool.FindGameComponent<Button>("Button_StoneDrawCardOnce");
        confirmDrawCardOnceMessage = GameObject.Find("Panel_ConfirmDrawCardOnceMessage");
        mBtn_CancelDrawCardOnce = UITool.FindGameComponent<Button>("Button_CancelDrawCardOnce");
        mBtn_SureDrawCardOnce = UITool.FindGameComponent<Button>("Button_SureDrawCardOnce");
        //drawCardOncePicture = UITool.FindChildObjectComponent<RawImage>("Canvas", "RawImage_DrawCardOncePicture");
        obj_DrawCard = UITool.FindHiddenChildObject("Canvas", "DrawCard");
        ani_DrawCard = obj_DrawCard.GetComponent<Animator>();
        drawCardOncePicture = obj_DrawCard.transform.GetChild(0).gameObject;
        ani_DrawCardOncePicture = drawCardOncePicture.GetComponent<Animator>();
        drawCardCamera = UITool.FindGameComponent<Camera>("Camera_DrawCard");
        //mBtn_RecoverEnergy = UITool.FindGameComponent<Button>("Button_RecoverEnergy");
        //mBtn_AddBackpack = UITool.FindGameComponent<Button>("Button_AddBackpack");
        //mBtn_AddFriends = UITool.FindGameComponent<Button>("Button_AddFriends");

        confirmDrawCardOnceMessage.SetActive(false);

        mBtn_Return.onClick.AddListener(delegate () {
            ReturnHome();
        });
        mBtn_Card.onClick.AddListener(delegate () {
            ani_Shop.SetTrigger("cardopen");
        });

        mBtn_CardReturn.onClick.AddListener(delegate () {
            ani_Shop.SetTrigger("cardclose");
        });
        mBtn_StoneDrawCard.onClick.AddListener(delegate () {
            ani_Shop.SetBool("stonedrawcardopen", true);
        });
        mBtn_StoneDrawCardReturn.onClick.AddListener(delegate () {
            ani_Shop.SetBool("stonedrawcardopen", false);
        });

        mBtn_StoneDrawCardOnce.onClick.AddListener(delegate () {
            confirmDrawCardOnceMessage.SetActive(true);
        });

        mBtn_CancelDrawCardOnce.onClick.AddListener(delegate () {
            confirmDrawCardOnceMessage.SetActive(false);
        });

        mBtn_SureDrawCardOnce.onClick.AddListener(delegate () {
            //drawCardOncePicture.gameObject.SetActive(true);
            //ani_DrawCardOncePicture.SetBool("drawcardpictureopen", true);

            obj_DrawCard.SetActive(true);
            ani_DrawCard.SetTrigger("drawcardpictureopen");
            //drawCardCamera.depth = 1;
            m_TOAGame.DrawCardOnceResult();
        });

        
    }

    /// <summary>
    /// 商店資訊打開
    /// </summary>
    public void ShopInfoOpen()
    {
        ani_Shop.SetBool("shopopen", true);
    }

    /// <summary>
    /// 返回主畫面
    /// </summary>
    private void ReturnHome()
    {
        bool Teamb = m_TOAGame.GetTeamBool();
        bool Backpackb = m_TOAGame.GetBackpackBool();
        bool Shop = ani_Shop.GetBool("shopopen");

        if (Teamb && Backpackb && Shop)
        {
            m_TOAGame.TeamInfoClose();
            m_TOAGame.BackpackInfoClose();
            ani_Shop.SetBool("shopopen", false);
        }
        else if (Teamb && Shop)
        {
            m_TOAGame.TeamInfoClose();
            ani_Shop.SetBool("shopopen", false);
        }
        else if (Backpackb && Shop)
        {
            m_TOAGame.BackpackInfoClose();
            ani_Shop.SetBool("shopopen", false);
        }
        else
        {
            ani_Shop.SetBool("shopopen", false);
        }

    }
}
