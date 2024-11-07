using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevelInfoUI : IUserInterface
{
    private GameObject m_LevelInfoUI;
    private GameObject m_WaterLevelInfo;
    
    private Button mBtn_return;
    /// <summary>
    /// 水關第1關
    /// </summary>
    private Button mBtn_Level1;
    /// <summary>
    /// 戰鬥準備隊伍
    /// </summary>
    private GameObject m_BattleReadyTeamUI;
    /// <summary>
    /// 戰鬥準備返回鍵
    /// </summary>
    private Button mBtn_BattleReadyReturn;

    /// <summary>
    /// 戰鬥準備確定鍵
    /// </summary>
    private Button mBtn_BattleReadySure;

    private bool enterWaterLevel1;
    private bool enterlevel;
    public WaterLevelInfoUI(TowerOfAdventureGame TOAGame) : base(TOAGame)
    {
        Initialize();
    }

    #region 戰鬥準備隊伍相關
    private List<Image> list_BattleReadyTeam;
    private bool redTeamItemOpen;
    private bool allowRedTeamItemClose;
    private int teamWhichNumber;
    private float teamPressTimer;
    private bool battleReadyTeamUIOpen;
    private bool allowEditorTeamOpen;

    //編輯隊伍相關參數
    private bool editorTeamItemOpen;
    private bool allowCloss;
    private int index;
    private float timer;

    //檢視選擇相關參數
    private bool viewTeamItemOpen;
    private bool allowViewTeamItemClose;
    private int viewWhichNumber;
    private float viewPressTimer;
    #endregion

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        m_LevelInfoUI = UITool.FindHiddenChildObject("Canvas", "LevelInfoUI");
        m_WaterLevelInfo = m_LevelInfoUI.transform.GetChild(0).gameObject;
        BtnInit();
        WaterLevelInfoClose();
        BattleReadyTeamInit();
    }


    public override void Update()
    {
        TeamDetect(); //不能寫在BattleReadyTeamItemClose的後面，不然編輯隊伍項目會開(如果滑鼠放開的時候剛好在項目的位置)
        BattleReadyTeamItemOpen();
        BattleReadyTeamItemClose();

        TimerReset();
    }

    /// <summary>
    /// 按鈕初始化
    /// </summary>
    private void BtnInit()
    {

        mBtn_return = m_WaterLevelInfo.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        mBtn_return.onClick.AddListener(delegate ()
        {
            WaterLevelInfoClose();
        });
        //mBtn_Level1 = UITool.FindGameComponent<Button>("WaterLevel1");
        mBtn_Level1 = m_WaterLevelInfo.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        mBtn_Level1.onClick.AddListener(delegate () {
            BattleReadyTeamUIOpen();
            battleReadyTeamUIOpen = true;
        });
        m_BattleReadyTeamUI = m_WaterLevelInfo.transform.GetChild(1).gameObject;
        mBtn_BattleReadyReturn = m_BattleReadyTeamUI.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        mBtn_BattleReadyReturn.onClick.AddListener(delegate () {
            BattleReadyTeamUIClose();
            battleReadyTeamUIOpen = false;
            allowEditorTeamOpen = false;
        });
        mBtn_BattleReadySure = m_BattleReadyTeamUI.transform.GetChild(0).transform.GetChild(3).GetComponent<Button>();
        mBtn_BattleReadySure.onClick.AddListener(delegate () {

            m_TOAGame.EnterLevelAni();
            //enterWaterLevel1 = true;
        });
    }


    /// <summary>
    /// 進入關卡
    /// </summary>
    /// <returns></returns>
    public bool EnterWater()
    {
        return enterlevel;
    }

    /// <summary>
    /// 進入水關第1關
    /// </summary>
    public bool EnterWaterLevel1()
    {        
        return enterWaterLevel1;
    }

    /// <summary>
    /// 設定參數
    /// </summary>
    public void SetParam()
    {
        enterWaterLevel1 = false;
    }

    /// <summary>
    /// 水關卡資訊打開
    /// </summary>
    public void WaterLevelInfoOpen()
    {
        m_LevelInfoUI.SetActive(true);
        m_WaterLevelInfo.SetActive(true);
    }

    /// <summary>
    /// 水關卡資訊關閉
    /// </summary>
    public void WaterLevelInfoClose()
    {
        m_BattleReadyTeamUI.SetActive(false);
        m_WaterLevelInfo.SetActive(false);
        m_LevelInfoUI.SetActive(false);

    }

    /// <summary>
    /// 戰鬥準備隊伍UI開
    /// </summary>
    private void BattleReadyTeamUIOpen()
    {
        m_BattleReadyTeamUI.SetActive(true);
    }

    /// <summary>
    /// 戰鬥準備隊伍UI關
    /// </summary>
    private void BattleReadyTeamUIClose()
    {
        m_BattleReadyTeamUI.SetActive(false);
    }

    /// <summary>
    /// 戰鬥準備隊伍初始化
    /// </summary>
    private void BattleReadyTeamInit()
    {
        list_BattleReadyTeam = new List<Image>();
        int temp = 0;
        for (int i = 0; i < 5; i++)
        {
            temp++;
            //list_BattleReadyTeam.Add(UITool.FindGameComponent<Image>($"BattleReadyTeamMember{temp}"));
            list_BattleReadyTeam.Add(m_BattleReadyTeamUI.transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).GetComponent<Image>());
        }
        BattleReadyTeamUIClose();
    }



    /// <summary>
    /// 隊伍偵測
    /// </summary>
    private void TeamDetect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (battleReadyTeamUIOpen)
                allowEditorTeamOpen = true;
        }
        RaycastHit hit; //被射線打到的東西
        if (Input.GetMouseButtonUp(0))
        {
            if (!redTeamItemOpen && allowEditorTeamOpen)
            {
                //time = 0;
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置
                if (Physics.Raycast(ray2, out hit, Mathf.Infinity))
                {
                    if (hit.transform.tag == "BattleReadyTeam")
                    {
                        Debug.Log(string.Format("Touch - Name:{0}, Position:{1}, Point:{2}", hit.transform.name, hit.transform.position, hit.point));
                        m_TOAGame.GetTeamInfoEditorTeam().SetActive(true);
                        Debug.Log("編輯隊伍打開");
                    }
                }
            }
        }
    }

    /// <summary>
    /// 戰鬥準備隊伍項目打開
    /// </summary>
    private void BattleReadyTeamItemOpen()
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
                        if (hit.transform.tag == "BattleReadyTeam" && hit.transform.name == $"BattleReadyTeamMember{i}")
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
                    if (hit.transform.tag == "BattleReadyTeam" && hit.transform.name == $"BattleReadyTeamMember{i}")
                    {
                        if (teamPressTimer >= 0.5f)
                        {
                            if (teamWhichNumber > m_TOAGame.GetRedTeamMembersList().Count || m_TOAGame.GetRedTeamMembersList()[teamWhichNumber - 1] == 0)
                            {
                                return;
                            }
                            m_TOAGame.GetListObj_PreInstantIndex(m_TOAGame.GetTeamInfoRedTeamMembers(teamWhichNumber) - 1).transform.SetParent(m_TOAGame.GetTeamInfoShowBackpackCardContent().transform);
                            redTeamItemOpen = true;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 戰鬥準備隊伍項目關閉
    /// </summary>
    private void BattleReadyTeamItemClose()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (redTeamItemOpen && allowRedTeamItemClose)
            {
                m_TOAGame.GetTeamInfoShowBackpackCardContent().transform.GetChild(0).gameObject.transform.SetParent(m_TOAGame.GetCardContentParent().transform);
                redTeamItemOpen = false;
                allowRedTeamItemClose = false;
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
                    m_TOAGame.GetListObj_PreInstantIndex(index - 1).transform.SetParent(m_TOAGame.GetTeamInfoShowBackpackCardContent().transform);
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
                m_TOAGame.GetTeamInfoShowBackpackCardContent().transform.GetChild(0).gameObject.transform.SetParent(m_TOAGame.GetCardContentParent().transform);
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
                            m_TOAGame.GetListObj_PreInstantIndex(m_TOAGame.GetTeamInfoRedTeamMembers(teamWhichNumber) - 1).transform.SetParent(m_TOAGame.GetTeamInfoShowBackpackCardContent().transform);
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
                m_TOAGame.GetTeamInfoShowBackpackCardContent().transform.GetChild(0).gameObject.transform.SetParent(m_TOAGame.GetCardContentParent().transform);
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
        if (Input.GetMouseButtonUp(0))
        {
            teamPressTimer = 0;
        }
    }

    /// <summary>
    /// 更新戰鬥準備隊伍UI
    /// </summary>
    public void UpdateBattleReadyTeamUI()
    {
        for (int i = 0; i < m_TOAGame.GetRedTeamMembersList().Count; i++)
        {
            list_BattleReadyTeam[i].sprite = m_TOAGame.GetSelectEditorTeamItemList(i);
        }
        
    }

    /// <summary>
    /// 取得戰鬥準備隊伍
    /// </summary>
    /// <returns></returns>
    public List<Image> GetBattleReadyTeam()
    {
        return list_BattleReadyTeam;
    }

}
