using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoUI : IUserInterface
{
    //隊伍資料
    private TeamData teamData;
    private string j_TeamData;
    private bool loading;

    private Button mBtn_Return = null;
    private Animator ani_Team = null;
   
    private List<Image> list_TeamImg;
    private GameObject obj_EditorTeam;

    #region 編輯隊伍
    private Object cardInfo = null;
    private CardInfo _cardInfo = null;
    private Button mBtn_EditorTeamReturn;

    /// <summary>
    /// 編輯隊伍項目的Img清單
    /// </summary>
    private List<Image> list_EditorTeamImgItem;

    private int index ;
    private float timer;
    private GameObject showBackpackCardContent;
    public GameObject obj_CardContentParent;
    /// <summary>
    /// 編輯隊伍項目打開
    /// </summary>
    private bool editorTeamItemOpen;
    private bool allowCloss;
    /// <summary>
    /// 紅隊成員清單
    /// </summary>
    private List<int> list_RedTeamMembers;
    /// <summary>
    /// 紅隊成員
    /// </summary>
    private int redMember;
    /// <summary>
    /// 選擇編輯隊伍項目照片清單
    /// </summary>
    private List<GameObject> list_EditorTeamSelectImg;

    /// <summary>
    /// 下方檢視選擇隊伍清單
    /// </summary>
    private List<Image> list_SelectEditorTeamItem;

    /// <summary>
    /// 檢視選擇未選擇照片
    /// </summary>
    private Sprite viewSelectionNoSelect;

    /// <summary>
    /// 選擇第幾位
    /// </summary>
    private int selectIndex;
    /// <summary>
    /// 選擇順序清單
    /// </summary>
    private List<int> list_SelectOrder;
    /// <summary>
    /// 檢視第幾個
    /// </summary>
    private int viewWhichNumber;
    /// <summary>
    /// 檢視按壓時間
    /// </summary>
    private float viewPressTimer;
    /// <summary>
    /// 檢視隊伍項目打開
    /// </summary>
    private bool viewTeamItemOpen;
    /// <summary>
    /// 允許檢視隊伍項目關閉
    /// </summary>
    private bool allowViewTeamItemClose;
    /// <summary>
    /// 紅隊人數
    /// </summary>
    private int redTeamPeople;
    /// <summary>
    /// 檢視確定按鈕
    /// </summary>
    private Button mbtn_ViewSure;

    private int teamWhichNumber;
    /// <summary>
    /// 隊伍按壓時間
    /// </summary>
    private float teamPressTimer;
    /// <summary>
    /// 紅隊項目打開
    /// </summary>
    private bool redTeamItemOpen;
    private bool allowRedTeamItemClose;
    #endregion

    public TeamInfoUI(TowerOfAdventureGame TOAGame):base(TOAGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        teamData = new TeamData();
        obj_EditorTeam = UITool.FindHiddenChildObject("Canvas", "EditorTeam");
        ani_Team = UITool.FindGameComponent<Animator>("Image_Team");        
        cardInfo = (CardInfo)Resources.Load("ScriptableObject/CardInfo", typeof(CardInfo)); //OK
        _cardInfo = (CardInfo)cardInfo;  //OK
        showBackpackCardContent = GameObject.Find("ShowBackpackCardContent");
        obj_CardContentParent = GameObject.Find("BackpackCardContent");
        list_RedTeamMembers = new List<int>();
        list_SelectOrder = new List<int>();
        viewSelectionNoSelect = Resources.Load<Sprite>("Sprite/Mini_background1");
        
        index = 0;
        timer = 0;
        redTeamPeople = 0;
        teamWhichNumber = 0;
        teamPressTimer = 0;
        ButtonInit();
        TeamInit();
        EditorTeamItemInit();


    }

    public override void Update()
    {
        if (!loading)
        {
            LoadingTeamArchive();
            loading = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteTeamArchive();
        }

        TeamDetect();
        EditorTeamItemOpen();
        ViewSelectionItemOpen();
        TeamItemOpen();
        SelectTeamMembers();
        EditorTeamItemClose();               
        ViewSelectionItemClose();
        TeamItemClose();
        TimerReset();
    }

    /// <summary>
    /// 隊伍資訊打開
    /// </summary>
    public void TeamInfoOpen()
    {
        bool Teamb = ani_Team.GetBool("teamopen");
        bool Backpackb = m_TOAGame.GetBackpackBool();
        if (Teamb == true && Backpackb == true)
        {
            //ani_Team.SetBool("teamopen", false); //程式碼由上到下執行，只執行一次，最終結果是teamopen >> true ，所以此行就白寫了
            m_TOAGame.BackpackInfoClose();
            ani_Team.SetBool("teamopen", true);
        }
        else if (Backpackb)
        {
            ani_Team.SetBool("teamopen", true);
            m_TOAGame.BackpackInfoClose();
            //ani_Team.SetBool("teamopen", true);
        }
        else
        {
            ani_Team.SetBool("teamopen", true);
        }     
    }

    /// <summary>
    /// 隊伍資訊關閉
    /// </summary>
    public void TeamInfoClose()
    {
        ani_Team.SetBool("teamopen", false);
    }

    /// <summary>
    /// 取得布林
    /// </summary>
    /// <returns></returns>
    public bool GetBool()
    {
        return ani_Team.GetBool("teamopen");
    }

    /// <summary>
    /// 隊伍初始化
    /// </summary>
    private void TeamInit()
    {
        list_TeamImg = new List<Image>();
        int temp = 0;
        for(int i = 1; i <= 5; i++)
        {
            temp++;
            list_TeamImg.Add(UITool.FindGameComponent<Image>($"Button_Member{temp}"));            
        }
    }

    /// <summary>
    /// 編輯隊伍項目初始化
    /// </summary>
    private void EditorTeamItemInit()
    {
        list_EditorTeamImgItem = new List<Image>();
        for (int i = 0; i < 10; i++)
        {
            //list_EditorTeamImgItem.Add(UITool.FindGameComponent<Image>($"EditorTeamItem{i}"));
            list_EditorTeamImgItem.Add(obj_EditorTeam.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).GetComponent<Image>());
        }

        list_EditorTeamSelectImg = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            //list_EditorTeamSelectImg.Add(GameObject.Find($"SelectEditorTeamItem{i}"));
            list_EditorTeamSelectImg.Add(list_EditorTeamImgItem[i].transform.GetChild(1).gameObject);
            list_EditorTeamSelectImg[i].SetActive(false);
        }

        list_SelectEditorTeamItem = new List<Image>();
        for (int i = 0; i < 5; i++)
        {
            //list_SelectEditorTeamItem.Add(UITool.FindGameComponent<Image>($"ViewSelectionItem{i}"));
            list_SelectEditorTeamItem.Add(obj_EditorTeam.transform.GetChild(1).transform.GetChild(0).transform.GetChild(i).GetComponent<Image>());
        }
        obj_EditorTeam.SetActive(false); //一開始進到主畫面就把編輯隊伍物件關閉
    }

    /// <summary>
    /// 編輯隊伍項目更新
    /// </summary>
    public void EditorTeamItemUpdate(int openBackpackContentButton, int CardDataIndex, ENUM_Behavior behavior)
    {
        if (behavior == ENUM_Behavior.DrawCard)
        {
            for (int i = 1; i <= 10; i++)
            {
                if (openBackpackContentButton == i)
                {
                    list_EditorTeamImgItem[openBackpackContentButton - 1].sprite = _cardInfo.cardData[CardDataIndex].CharacterAvatar;

                    //新增資料 (應該不用，直接跟背包拿就好，不需要再開個欄位放背包的資料)
                    //teamData.list_EditorTeamCardDataItem.Add(new CardData());
                    //teamData.list_EditorTeamCardDataItem[openBackpackContentButton - 1] = _cardInfo.cardData[CardDataIndex];
                }
            }
        }

        if (behavior == ENUM_Behavior.Loading)
        {
            for (int i = 1; i <= 10; i++)
            {
                if (openBackpackContentButton == i)
                {
                    //list_EditorTeamImgItem[openBackpackContentButton - 1].sprite = m_TOAGame.GetBackpackData().list_BackpackData[CardDataIndex].CharacterAvatar;
                    list_EditorTeamImgItem[openBackpackContentButton - 1].sprite = _cardInfo.cardData[m_TOAGame.GetBackpackData().list_cardDataIndex[openBackpackContentButton - 1]].CharacterAvatar;
                }
            }
        }
    }

    /// <summary>
    /// 編輯隊伍項目打開
    /// </summary>
    private void EditorTeamItemOpen()
    {
        RaycastHit hit; //被射線打到的東西
        if (Input.GetMouseButtonDown(0))
        {
            if (editorTeamItemOpen)
            {
                allowCloss = true;
            }
            else
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置
                if (Physics.Raycast(ray2, out hit, Mathf.Infinity))
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        if (hit.transform.tag == "EditorTeam" && hit.transform.name == $"EditorTeamItem{i}")
                        {
                            index = i;
                        }
                    }
                }
            }           
        }

        if (Input.GetMouseButton(0))
        {            
            timer += Time.deltaTime;
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置
            if (Physics.Raycast(ray2, out hit, Mathf.Infinity))
            {
                if (timer >= 0.5f && index != 0)
                {
                    m_TOAGame.GetListObj_PreInstantIndex(index - 1).transform.SetParent(showBackpackCardContent.transform);
                    editorTeamItemOpen = true;
                }
            }
        }       
    }

    /// <summary>
    /// 編輯隊伍項目關閉
    /// </summary>
    private void EditorTeamItemClose()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (editorTeamItemOpen && allowCloss)
            {
                showBackpackCardContent.transform.GetChild(0).gameObject.transform.SetParent(obj_CardContentParent.transform);
                editorTeamItemOpen = false;
                allowCloss = false;
            }
        }
    }

    /// <summary>
    /// 檢視選擇項目打開
    /// </summary>
    private void ViewSelectionItemOpen()
    {
        RaycastHit hit; //被射線打到的東西
        if (Input.GetMouseButtonDown(0))
        {
            if (viewTeamItemOpen)
            {
                allowViewTeamItemClose = true;
            }
            else
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置
                if (Physics.Raycast(ray2, out hit, Mathf.Infinity))
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (hit.transform.tag == "ViewSelection" && hit.transform.name == $"ViewSelectionItem{i}")
                        {
                            viewWhichNumber = i;
                        }
                    }
                }
            }          
        }

        if (Input.GetMouseButton(0))
        {
            viewPressTimer += Time.deltaTime;
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置
            if (Physics.Raycast(ray2, out hit, Mathf.Infinity))
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (hit.transform.tag == "ViewSelection" && hit.transform.name == $"ViewSelectionItem{i}")
                    {
                        if (viewPressTimer >= 0.5f)
                        {
                            m_TOAGame.GetListObj_PreInstantIndex(list_RedTeamMembers[viewWhichNumber - 1] - 1).transform.SetParent(showBackpackCardContent.transform);
                            viewTeamItemOpen = true;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 檢視選擇項目關閉
    /// </summary>
    private void ViewSelectionItemClose()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (viewTeamItemOpen && allowViewTeamItemClose)
            {
                showBackpackCardContent.transform.GetChild(0).gameObject.transform.SetParent(obj_CardContentParent.transform);
                viewTeamItemOpen = false;
                allowViewTeamItemClose = false;
            }
        }
    }

    /// <summary>
    /// 計時器重製
    /// </summary>
    private void TimerReset()
    {
        //Debug.Log($"timer : {timer} , index : {index}");
        if (Input.GetMouseButtonUp(0))
        {
            timer = 0;
            index = 0;
            viewWhichNumber = 0;
            viewPressTimer = 0;
            teamPressTimer = 0;
        }
    }

    /// <summary>
    /// 按鈕初始化
    /// </summary>
    private void ButtonInit()
    {      
        mBtn_Return = UITool.FindChildObjectComponent<Button>("Image_TeamUpInterface", "Button_Return");
        mBtn_Return.onClick.AddListener(delegate () {
            ani_Team.SetBool("teamopen", false);
        });

        mBtn_EditorTeamReturn = obj_EditorTeam.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        mBtn_EditorTeamReturn.onClick.AddListener(delegate () {
            obj_EditorTeam.SetActive(false);
        });

        //mbtn_ViewSure = UITool.FindChildObjectComponent<Button>("EditorTeamDownUI", "Button_Sure");
        mbtn_ViewSure = obj_EditorTeam.transform.GetChild(1).transform.GetChild(2).GetComponent<Button>();
        mbtn_ViewSure.onClick.AddListener(delegate () { 
            for (int i = 0; i < list_RedTeamMembers.Count; i++)
            {
                list_TeamImg[i].sprite = list_SelectEditorTeamItem[i].sprite;


                //儲存資料 
                teamData.list_TeamImgData.Add(list_SelectEditorTeamItem[i].sprite);
                DeleteTeamArchive();
                TeamArchive();
            }
            m_TOAGame.UpdateBattleReadyTeamUI();
            m_TOAGame.UpdateBattleReadyTeamUIFireLevel();
            m_TOAGame.UpdateBattleReadyTeamUIWoodLevel();
            m_TOAGame.UpdateBattleReadyTeamUILightLevel();
            m_TOAGame.UpdateBattleReadyTeamUIDarkLevel();
            obj_EditorTeam.SetActive(false);
        });
    }

    /// <summary>
    /// 隊伍偵測
    /// </summary>
    private void TeamDetect()
    {
        RaycastHit hit; //被射線打到的東西
        if (Input.GetMouseButtonUp(0))
        {
            if (!redTeamItemOpen)
            {
                //time = 0;
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置
                if (Physics.Raycast(ray2, out hit, Mathf.Infinity))
                {
                    if (hit.transform.tag == "Team")
                    {
                        Debug.Log(string.Format("Touch - Name:{0}, Position:{1}, Point:{2}", hit.transform.name, hit.transform.position, hit.point));
                        obj_EditorTeam.SetActive(true);
                    }
                }
            }            
        }
    }

    /// <summary>
    /// 隊伍項目打開
    /// </summary>
    private void TeamItemOpen()
    {
        RaycastHit hit; //被射線打到的東西
        if (Input.GetMouseButtonDown(0))
        {
            if (redTeamItemOpen)
            {
                allowRedTeamItemClose = true;
            }
            else
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置
                if (Physics.Raycast(ray2, out hit, Mathf.Infinity))
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (hit.transform.tag == "Team" && hit.transform.name == $"Button_Member{i}")
                        {
                            teamWhichNumber = i;
                        }
                    }
                }
            }           
        }

        if (Input.GetMouseButton(0))
        {
            teamPressTimer += Time.deltaTime;
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置
            if (Physics.Raycast(ray2, out hit, Mathf.Infinity))
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (hit.transform.tag == "Team" && hit.transform.name == $"Button_Member{i}")
                    {
                        if (teamPressTimer >= 0.5f)
                        {
                            if (teamWhichNumber > list_RedTeamMembers.Count || list_RedTeamMembers[teamWhichNumber - 1] == 0)
                            {
                                //若隊伍的某個成員是空的，就不讓你把那個隊伍項目打開
                                return;
                            }
                            m_TOAGame.GetListObj_PreInstantIndex(list_RedTeamMembers[teamWhichNumber - 1] - 1).transform.SetParent(showBackpackCardContent.transform);
                            redTeamItemOpen = true;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 隊伍項目關閉
    /// </summary>
    private void TeamItemClose()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (redTeamItemOpen && allowRedTeamItemClose)
            {
                showBackpackCardContent.transform.GetChild(0).gameObject.transform.SetParent(obj_CardContentParent.transform);
                redTeamItemOpen = false;
                allowRedTeamItemClose = false;
            }
        }
    }

    /// <summary>
    /// 選擇隊伍成員
    /// </summary>
    private void SelectTeamMembers()
    {
        RaycastHit hit; //被射線打到的東西
        if (Input.GetMouseButtonUp(0))
        {
            if (!editorTeamItemOpen && !viewTeamItemOpen) 
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置
                if (Physics.Raycast(ray2, out hit, Mathf.Infinity))
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        if (hit.transform.tag == "EditorTeam" && hit.transform.name == $"EditorTeamItem{i}")
                        {
                            redMember = i;
                            //list_SelectEditorTeamItem.Add(list_EditorTeamSelectImg[redMember - 1]);
                            if (list_EditorTeamSelectImg[redMember - 1].activeInHierarchy)
                            {
                                //selectIndex--;
                                //list_SelectOrder.Remove()

                                for (int j = 0; j < list_RedTeamMembers.Count; j++)
                                {
                                    if (list_RedTeamMembers[j] == redMember)
                                    {
                                        list_RedTeamMembers.RemoveAt(j);
                                        list_RedTeamMembers.Insert(j, 0); //在原本移除的內容塞一個0，代表此內容原本有選擇，但後來被移除
                                        list_SelectEditorTeamItem[j].sprite = viewSelectionNoSelect;

                                        //儲存資料
                                        teamData.list_RedTeamMembersData.RemoveAt(j);
                                        teamData.list_RedTeamMembersData.Insert(j, 0);
                                        teamData.list_SelectEditorTeamItemCardData[j] = new CardData();
                                        teamData.list_SelectEditorTeamItemImgIndexData[j] = -1;
                                        teamData.list_SelectionOrder.RemoveAt(j);
                                        teamData.list_SelectionOrder.Insert(j, 0);
                                        teamData.list_SelectImgActivity[j] = false;
                                        DeleteTeamArchive();
                                        TeamArchive();
                                    }
                                }
                               
                                Text text = list_EditorTeamSelectImg[redMember - 1].transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
                                text.text = $"{0}";
                                list_EditorTeamSelectImg[redMember - 1].SetActive(false);                                                              
                            }
                            else
                            {                              
                                if (list_RedTeamMembers.Count >= 5) //檢查紅隊是否已經5人
                                {
                                    for (int l = 0; l < list_RedTeamMembers.Count; l++)
                                    {
                                        if (list_RedTeamMembers[l] != 0)
                                        {
                                            redTeamPeople++;
                                        }
                                    }
                                    if (redTeamPeople == 5)
                                    {
                                        redTeamPeople = 0;
                                        return;
                                    }
                                    else
                                    {
                                        redTeamPeople = 0;
                                    }
                                }

                                for (int k = 0; k < list_RedTeamMembers.Count; k++)
                                {
                                    if (list_RedTeamMembers[k] == 0)
                                    {
                                        list_RedTeamMembers.RemoveAt(k);
                                        list_RedTeamMembers.Insert(k, redMember);
                                        Text txt = list_EditorTeamSelectImg[redMember - 1].transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
                                        txt.text = $"{k + 1}";
                                        list_EditorTeamSelectImg[redMember - 1].SetActive(true);
                                        list_SelectEditorTeamItem[k].sprite = list_EditorTeamImgItem[redMember - 1].sprite; //下方檢視選擇也會出現所點選的角色(照片)

                                        //儲存資料
                                        teamData.list_RedTeamMembersData.RemoveAt(k);
                                        teamData.list_RedTeamMembersData.Insert(k, redMember);
                                        teamData.list_SelectionOrder.RemoveAt(k);
                                        teamData.list_SelectionOrder.Insert(k, k + 1);
                                        teamData.list_SelectImgActivity[k] = true;
                                        teamData.list_SelectEditorTeamItemCardData[k] = m_TOAGame.GetBackpackData().list_BackpackData[redMember - 1];
                                        teamData.list_SelectEditorTeamItemImgIndexData[k] = m_TOAGame.GetBackpackData().list_cardDataIndex[redMember - 1];
                                        DeleteTeamArchive();
                                        TeamArchive();
                                        return;
                                    }
                                }
                                list_RedTeamMembers.Add(redMember);
                                Text text = list_EditorTeamSelectImg[redMember - 1].transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
                                text.text = $"{list_RedTeamMembers.Count}";
                                list_EditorTeamSelectImg[redMember - 1].SetActive(true);
                                list_SelectEditorTeamItem[list_RedTeamMembers.Count - 1].sprite = list_EditorTeamImgItem[redMember - 1].sprite; //下方檢視選擇也會出現所點選的角色(照片)

                                //儲存資料
                                teamData.list_RedTeamMembersData.Add(redMember); //儲存紅隊成員資料
                                teamData.list_SelectionOrder.Add(list_RedTeamMembers.Count); //儲存選擇第幾位清單
                                teamData.list_SelectImgActivity.Add(true); //儲存選擇照片的活動狀態 //可能需要一次存10個的可能性，要再思考，因為寫到上面就卡住了
                                teamData.list_SelectEditorTeamItemCardData.Add(m_TOAGame.GetBackpackData().list_BackpackData[redMember - 1]); //儲存下方檢視選擇隊伍的卡片資料
                                teamData.list_SelectEditorTeamItemImgIndexData.Add(m_TOAGame.GetBackpackData().list_cardDataIndex[redMember - 1]);
                                DeleteTeamArchive();
                                TeamArchive();
                            }
                        }
                    }
                }
            }          
        }
    }

    /// <summary>
    /// 取得紅隊成員代表抽到第幾張卡
    /// </summary>
    /// <param name="Members">紅隊成員</param>
    /// <returns></returns>
    public int GetRedTeamMembers(int Members)
    {
        return list_RedTeamMembers[Members - 1];
    }

    public GameObject GetShowBackpackCardContent()
    {
        return showBackpackCardContent;
    }

    /// <summary>
    /// 取得編輯隊伍
    /// </summary>
    /// <returns></returns>
    public GameObject GetEditorTeam()
    {
        return obj_EditorTeam;
    }

    /// <summary>
    /// 取得紅隊成員清單
    /// </summary>
    /// <returns></returns>
    public List<int> GetRedTeamMembersList()
    {
        return list_RedTeamMembers;
    }

    /// <summary>
    /// 取得卡片內容父物件
    /// </summary>
    /// <returns></returns>
    public GameObject GetCardContentParent()
    {
        return obj_CardContentParent;
    }

    /// <summary>
    /// 取得選擇編輯隊伍成員清單(照片)
    /// </summary>
    /// <returns></returns>
    public Sprite GetSelectEditorTeamItemList(int i)
    {
        return list_SelectEditorTeamItem[i].sprite;
    }


    /// <summary>
    /// 隊伍存檔
    /// </summary>
    private void TeamArchive()
    {
        j_TeamData = JsonUtility.ToJson(teamData);
        PlayerPrefs.SetString("隊伍資料", j_TeamData);
        PlayerPrefs.Save(); //存檔
        Debug.Log("隊伍資料以儲存");
    }

    /// <summary>
    /// 刪除隊伍存檔
    /// </summary>
    private void DeleteTeamArchive()
    {
        if (PlayerPrefs.HasKey("隊伍資料"))
        {
            //把舊資料先刪除
            PlayerPrefs.DeleteKey("隊伍資料");
            Debug.Log("隊伍資料以清除");
        }
    }

    /// <summary>
    /// 讀取隊伍存檔
    /// </summary>
    private void LoadingTeamArchive()
    {
        if (PlayerPrefs.HasKey("隊伍資料"))
        {
            Debug.Log("讀取隊伍檔案");
            j_TeamData = PlayerPrefs.GetString("隊伍資料");
            teamData = JsonUtility.FromJson<TeamData>(j_TeamData);

            //灌值
            Debug.Log("teamData.list_RedTeamMembersData.Count : " + teamData.list_RedTeamMembersData.Count);
            for (int i = 0; i < teamData.list_RedTeamMembersData.Count; i++)
            {
                list_RedTeamMembers.Add(teamData.list_RedTeamMembersData[i]);
                Debug.Log($"list_RedTeamMembers[{i}] : {list_RedTeamMembers[i]}");
                if (list_RedTeamMembers[i] <= 0)
                {
                    continue;
                }
                Text text = list_EditorTeamSelectImg[list_RedTeamMembers[i] - 1].transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
                text.text = $"{teamData.list_SelectionOrder[i]}";
                list_EditorTeamSelectImg[list_RedTeamMembers[i] - 1].SetActive(teamData.list_SelectImgActivity[i]);

                #region 圖片更新的部分
                if (teamData.list_SelectEditorTeamItemImgIndexData[i] < 0)
                {
                    continue;
                }
                list_SelectEditorTeamItem[i].sprite = _cardInfo.cardData[teamData.list_SelectEditorTeamItemImgIndexData[i]].CharacterAvatar;
                //list_SelectEditorTeamItem[i].sprite = teamData.list_SelectEditorTeamItemCardData[i].CharacterAvatar;
                list_TeamImg[i].sprite = _cardInfo.cardData[teamData.list_SelectEditorTeamItemImgIndexData[i]].CharacterAvatar;
                //list_TeamImg[i].sprite = teamData.list_SelectEditorTeamItemCardData[i].CharacterAvatar;

                //戰鬥準備隊伍也一起更新
                m_TOAGame.GetBattleReadyTeam()[i].sprite = _cardInfo.cardData[teamData.list_SelectEditorTeamItemImgIndexData[i]].CharacterAvatar;
                m_TOAGame.GetBattleReadyTeamFireLevel()[i].sprite = _cardInfo.cardData[teamData.list_SelectEditorTeamItemImgIndexData[i]].CharacterAvatar;
                m_TOAGame.GetBattleReadyTeamWoodLevel()[i].sprite = _cardInfo.cardData[teamData.list_SelectEditorTeamItemImgIndexData[i]].CharacterAvatar;
                m_TOAGame.GetBattleReadyTeamLightLevel()[i].sprite = _cardInfo.cardData[teamData.list_SelectEditorTeamItemImgIndexData[i]].CharacterAvatar;
                m_TOAGame.GetBattleReadyTeamDarkLevel()[i].sprite = _cardInfo.cardData[teamData.list_SelectEditorTeamItemImgIndexData[i]].CharacterAvatar;
                //m_TOAGame.GetBattleReadyTeam()[i].sprite = teamData.list_SelectEditorTeamItemCardData[i].CharacterAvatar;
                #endregion

            }
        }
    }

    /// <summary>
    /// 取得隊伍資料
    /// </summary>
    /// <returns></returns>
    public TeamData GetTeamData()
    {
        if (teamData == null)
        {
            j_TeamData = PlayerPrefs.GetString("隊伍資料");
            teamData = JsonUtility.FromJson<TeamData>(j_TeamData);
        }
        return teamData;
    }
}
