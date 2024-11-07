using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    /*
    問題發現 : 使用Grid Layout Group 會讓子物件的位置都一模一樣，但是把Grid Layout Group移除，子物件的位置又都不一樣了，莫名其妙???

    之後要完成的事情 : 
    1. 使用smomogame的寫法先寫出來
    2. 在改寫成多為陣列或你想要的樣子
    */
    /// <summary>
    /// 元素類型清單
    /// </summary>
    [Header("元素類型清單")]  
    public List<Sprite> list_ElementType;

    /// <summary>
    /// 所有網格上的元素(UI)
    /// </summary>
    [Header("所有網格上的元素(UI)")]    
    public List<Image> list_AllElement;

    /// <summary>
    /// 所有網格上的元素(資料)
    /// </summary>
    public List<ElementType> list_ElementTypes;

    /// <summary>
    /// 所有球的位置
    /// </summary>
    [Header("所有球的位置")]
    public List<Vector3> list_AllballPos;

    /// <summary>
    /// 所有的球
    /// </summary>
    [Header("所有的球")]
    public List<Ball> list_AllBall;

    /// <summary>
    /// 所有的球(按照ID順去排)
    /// </summary>
    [Header("所有的球(按照ID順去排)")]
    public Ball[] AllBallIDData;    

    #region 待消除清單
    /// <summary>
    /// 待消除清單(多少Combo)
    /// </summary>
    public List<List<Ball>> list_ReadyToEliminate;

    #endregion


    /// <summary>
    /// 上方球的位置清單
    /// </summary>
    [Header("上方球的清單")]
    public List<UpBall> list_UpBall;

    /// <summary>
    /// 上方球的位置清單
    /// </summary>
    [Header("上方球的位置清單")]
    public List<Vector3> list_UpBallPos;

    /// <summary>
    /// 所有在場景上的球
    /// </summary>
    [Header("所有在場景上的球")]
    public List<GameObject> list_AllObj;

    /// <summary>
    /// 被拖曳的球
    /// </summary>
    public Ball isDragedBall;

    public List<RectTransform> list_rect;

    public bool reDelete;
    public TurnBeadsStatus turnBeadsStatus;
    public bool allballFallCheck;
    ILevelManager levelManager;

    #region 傷害參數
    /// <summary>
    /// 單一Combo傷害清單
    /// </summary>
    private List<int> list_SingleComboDamage;

    /// <summary>
    /// 隊伍單一combo傷害清單
    /// </summary>
    private List<List<int>> list_TeamSingleComboDamage;

    /// <summary>
    /// 隊伍加總傷害清單
    /// </summary>
    private List<int> list_TeamSumComboDamage;

    /// <summary>
    /// 每一次隊伍加總傷害清單(拷貝)
    /// </summary>
    private List<List<int>> list_TeamSumComboDamageCopy;

    /// <summary>
    /// 無法在消除後的每個成員的最終傷害
    /// </summary>
    private List<int> list_TeamSumComboDamageData;

    #endregion

    public void SetLevelManager(ILevelManager levelManager)
    {
        this.levelManager = levelManager;
    }

    public void TestPos()
    {

        for (int i = 0; i < list_AllObj.Count; i++)
        {
            Debug.Log($"list_obj[{i}].transform.localPosition : " + list_AllObj[i].transform.localPosition);
        }

        for (int i = 0; i < list_AllObj.Count; i++)
        {
            list_rect.Add(list_AllObj[i].transform.GetComponent<RectTransform>());
            Debug.Log($"list_rect[{i}].anchoredPosition3D" + list_rect[i].anchoredPosition3D);
        }
    }


    #region 進入關卡後要馬上做的事情 && 初始化相關
    /// <summary>
    /// 元素類型初始化
    /// </summary>
    public void ElementTypesInit()
    {
        list_ElementTypes = new List<ElementType>();
        list_ElementTypes.Add(ElementType.Water);
        list_ElementTypes.Add(ElementType.Fire);
        list_ElementTypes.Add(ElementType.Wood);
        list_ElementTypes.Add(ElementType.Light);
        list_ElementTypes.Add(ElementType.Dark);
    }

    /// <summary>
    /// 清單初始化
    /// </summary>
    public void ListInit()
    {
        list_ReadyToEliminate = new List<List<Ball>>();
        AllBallIDData = new Ball[30];
        for (int i = list_AllBall.Count; i > 0; i--)
        {
            list_AllBall[i - 1].ListInit();
        }

        list_SingleComboDamage = new List<int>();
        list_TeamSingleComboDamage = new List<List<int>>();
        list_TeamSumComboDamage = new List<int>();
        list_TeamSumComboDamageData = new List<int>();
        list_TeamSumComboDamageCopy = new List<List<int>>();

        for (int i = 0; i < TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData.Count; i++)
        {
            list_TeamSumComboDamageData.Add(0);
        }
    }

    /// <summary>
    /// 產生隨機球
    /// </summary>
    private void SpawnRandomBall()
    {
        ElementTypesInit();
        for (int i = 0; i < list_AllElement.Count; i++)
        {
            int r = Random.Range(0, 5);
            //int r = Random.Range(0, 2);
            list_AllElement[i].sprite = list_ElementType[r];
            list_AllBall[i].elementType = list_ElementTypes[r];
        }
    }


    /// <summary>
    /// 儲存所有球的位置
    /// </summary>
    public void SaveAllBallPos()
    {
        list_AllballPos = new List<Vector3>();
        for (int i = 0; i < list_AllElement.Count; i++)
        {
            list_AllballPos.Add(list_AllObj[i].transform.position);
            //list_AllballPos.Add(list_AllObj[i].transform.localPosition);
        }
    }


    public void SaveAllBall()
    {
        list_AllBall = new List<Ball>();
        for (int i = 0; i < 30; i++)
        {
            list_AllBall.Add(GameObject.Find("Elements_NoGroup").transform.GetChild(i).transform.GetComponent<Ball>());
        }
    }

    /// <summary>
    /// 上方球的位置
    /// </summary>
    private void UpBallPos()
    {
        for (int i = 0; i < list_UpBall.Count; i++)
        {
            //Debug.Log($"list_UpBallPos[{i}].位置 : {list_UpBall[i].transform.position}");
            list_UpBallPos.Add(list_UpBall[i].transform.position);
        }

    }

    #endregion

    #region 消除邏輯相關
    /// <summary>
    /// 全版消除
    /// </summary>
    public IEnumerator CoFullVersionDelete()
    {
        yield return levelManager.CoCloseTurnBeatsTime();
        yield return CoFullVersionDeleteStart();
        yield return CoCountTotalDamage();
        //要攻擊怪物
        yield return levelManager.CoAttackMonster();
        yield return levelManager.CoAttackPlayer();
        //切換battle
        yield return levelManager.CoWhoDied();
        yield return ClearTotalDamageData();
        yield return CoResetTotalDamageData();
        yield return levelManager.CoResetTime();
        //turnBeadsStatus = TurnBeadsStatus.WaitTurnBeads;
    }

    /// <summary>
    /// 全版消除開始
    /// </summary>
    public IEnumerator CoFullVersionDeleteStart()
    {
        reDelete = true; //滑鼠放開要先true，不然消除不了
        Debug.Log("全版消除開始");
        while (reDelete)
        {
            yield return CoAllBallCheckLink();
            yield return CoCountDamage(); //看看有沒有問題
            yield return CoBallHide();
            //Debug.Log("球隱藏");
            yield return CoAllBallCheckFall(); //本身這樣寫就有2種功能 >> 1 這件事情做完才能做以下的事情 2 執行一次CoAllBallCheckFall()
            yield return CoAllBallFallFinish();
            //yield return new WaitForSeconds(0.75f);
            //Debug.Log("所有的球確定落下已做完");
            yield return CoReDeleteCheck();
            yield return CoDeleteData();  //清除資料
            //Debug.Log("資料已被清除");
            yield return new WaitForSeconds(0);
        }
    }

    /// <summary>
    /// 所有的球確認連線
    /// </summary>
    public IEnumerator CoAllBallCheckLink()
    {
        for (int i = list_AllBall.Count; i > 0; i--)
        {
            list_AllBall[i - 1].RaycastAllDirections();
        }

        for (int i = list_AllBall.Count; i > 0; i--)
        {
            //id = 30 的球要優先判斷，而不是用清單裡面的最後一份資料優先判斷
            for (int j = list_AllBall.Count; j > 0; j--)
            {
                if (list_AllBall[i - 1].id == j)
                {
                    AllBallIDData[j - 1] = list_AllBall[i - 1];
                }
            }
        }

        for (int i = AllBallIDData.Length; i > 0; i--)
        {
            AllBallIDData[i - 1].CheckLink();
            if (AllBallIDData[i - 1].list_ReadyToEliminate.Count > 0)
            {
                list_ReadyToEliminate.Add(AllBallIDData[i - 1].list_ReadyToEliminate[0]);
            }
        }

        yield return null;
    }

    /// <summary>
    /// 球隱藏
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoBallHide()
    {
        for (int i = 0; i < list_ReadyToEliminate.Count; i++)
        {
            foreach (var ball in list_ReadyToEliminate[i])
            {
                ball.Fade();
            }
            levelManager.PlayComboAudio();
            yield return new WaitForSeconds(0.46f);
        }
    }

    /// <summary>
    /// 所有的球確定落下
    /// </summary>
    public IEnumerator CoAllBallCheckFall()
    {
        //yield return CoBallHide(); //等球隱藏玩以後在座以下事情
        //Debug.Log("帶消除清單的球已隱藏完畢");
        yield return new WaitForSeconds(0.15f);
        UpBallAppear();  //改成動畫就沒問題
        UpBallChangeColor(); //改變顏色      
        //以下的方式都不好，只能一顆球檢查完就馬上變true，不能憶起做，但要小心true太快搶拍子
        yield return CoDownBallFall();
        //DownBallAllowMove();
        yield return CoUpBallFall();
        //UpBallAllowMove();

        allballFallCheck = true;
    }

    /// <summary>
    /// 所有的球掉落完成
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoAllBallFallFinish()
    {
        //要寫在協成裡面
        while (allballFallCheck)
        {
            //檢查所有的球是否移動完成
            int moveturecount = 0;
            for (int i = 0; i < list_AllBall.Count; i++)
            {
                if (list_AllBall[i].move)
                {
                    moveturecount++;
                }
            }

            if (moveturecount == 0)
            {
                allballFallCheck = false;
            }

            yield return new WaitForSeconds(0);
        }
    }

    /// <summary>
    /// 重新消除檢查
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoReDeleteCheck()
    {
        int canReDeleteCount = 0;
        for (int i = 0; i < list_AllBall.Count; i++)
        {
            list_AllBall[i].ReDeleteCheck();
            if (list_AllBall[i].canReDelete)
            {
                canReDeleteCount++;
            }
        }

        if (canReDeleteCount > 0)
        {
            reDelete = true;
        }
        else
        {
            reDelete = false;
        }

        yield return null;
    }



    /// <summary>
    /// 下面的球落下
    /// </summary>
    public IEnumerator CoDownBallFall()
    {
        //下面的球落下
        for (int i = 30; i > 0; i--)
        {
            for (int j = list_AllBall.Count; j > 0; j--)
            {
                if (list_AllBall[j - 1].id == i)
                {
                    //list_AllBall[j - 1].CheckFall();
                    StartCoroutine(list_AllBall[j - 1].CoCheckFall());
                }
            }
        }
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// 下面的球允許移動
    /// </summary>
    /// <returns></returns>
    public void DownBallAllowMove()
    {
        //下面的球落下
        for (int i = 30; i > 0; i--)
        {
            for (int j = list_AllBall.Count; j > 0; j--)
            {
                if (list_AllBall[j - 1].id == i)
                {
                    list_AllBall[j - 1].move = true;
                    //StartCoroutine(list_AllBall[j - 1].CoCheckFall());
                }
            }
        }

    }

    /// <summary>
    /// 上面的球落下
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoUpBallFall()
    {
        //上面的球落下
        for (int i = -30; i < 0; i++)
        {
            for (int j = 30; j > 0; j--)
            {
                if (list_AllBall[j - 1].id == i)
                {
                    //list_AllBall[j - 1].CheckFall();
                    StartCoroutine(list_AllBall[j - 1].CoCheckFall());
                }
            }
        }
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// 上面的球允許移動
    /// </summary>
    /// <returns></returns>
    public void UpBallAllowMove()
    {
        //上面的球落下
        for (int i = -30; i < 0; i++)
        {
            for (int j = 30; j > 0; j--)
            {
                if (list_AllBall[j - 1].id == i)
                {
                    list_AllBall[j - 1].move = true;
                    //StartCoroutine(list_AllBall[j - 1].CoCheckFall());
                }
            }
        }

    }

    /// <summary>
    /// 上面的球出現
    /// </summary>
    public void UpBallAppear()
    {
        //Debug.Log("上面的球出現");
        for (int i = 0; i < list_ReadyToEliminate.Count; i++)
        {
            foreach (var ball in list_ReadyToEliminate[i])
            {
                ball.Appear();
            }
        }
    }

    /// <summary>
    /// 上方的球改變顏色
    /// </summary>
    private void UpBallChangeColor()
    {
        //Debug.Log("上面的球改變顏色");
        for (int i = 0; i < list_ReadyToEliminate.Count; i++)
        {
            foreach (var ball in list_ReadyToEliminate[i])
            {
                int r = Random.Range(0, 5);
                ball.elementType = list_ElementTypes[r];
                ball.transform.GetComponent<Image>().sprite = list_ElementType[r];
            }
        }
    }

    #endregion

    #region 清除Combo資料

    /// <summary>
    /// 清除資料
    /// </summary>
    public IEnumerator CoDeleteData()
    {
        for (int i = 0; i < list_AllBall.Count; i++)
        {
            list_AllBall[i].DeleteDataReset();
        }

        yield return CoListRemake();
        yield return null;
    }

    /// <summary>
    /// 清單重製
    /// </summary>
    public IEnumerator CoListRemake()
    {
        list_ReadyToEliminate.Clear();
        yield return CoCountTotalDamage();
        list_TeamSingleComboDamage.Clear();
        list_TeamSumComboDamage.Clear();
        yield return new WaitForSeconds(0);
    }

    #endregion


    #region 計算玩家角色傷害
    /// <summary>
    /// 計算每個全版消除的傷害
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoCountDamage()
    {        
        for (int i = 0; i < TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData.Count; i++)
        {
            list_TeamSingleComboDamage.Add(new List<int>());
            int damageTemp = 0;

            for (int j = 0; j < list_ReadyToEliminate.Count; j++)
            {
                if (TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].elementType == list_ReadyToEliminate[j][0].elementType)
                {                   
                    int damage = 0;
                    damage = TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].ATK * list_ReadyToEliminate[j].Count * 2;
                    list_TeamSingleComboDamage[i].Add(damage);
                }
                else
                {                   
                    int damage = 0;
                    damage = TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData[i].ATK * list_ReadyToEliminate[j].Count * 1;
                    list_TeamSingleComboDamage[i].Add(damage);
                }
            }

            foreach (int SingleComboDamage in list_TeamSingleComboDamage[i])
            {
                //list_TeamSumComboDamage.Add()
                damageTemp += SingleComboDamage;
            }
            list_TeamSumComboDamage.Add(damageTemp);
                   
        }

        for (int i = 0; i < list_TeamSumComboDamage.Count; i++)
        {
            Debug.Log($"list_TeamSumComboDamage[{i}] : {list_TeamSumComboDamage[i]}");
        }

        list_TeamSumComboDamageCopy.Add(new List<int>());
        list_TeamSumComboDamageCopy[list_TeamSumComboDamageCopy.Count - 1] = list_TeamSumComboDamage.GetRange(0, list_TeamSumComboDamage.Count);
        yield return new WaitForSeconds(0);
        //for (int i = 0; i < list_TeamSumComboDamage.Count; i++)
        //{
        //    Debug.Log($"隊伍第{i + 1}位傷害 : {list_TeamSumComboDamage[i]}");
        //}

        //GetRange是拷貝值(就是我要的東西)
        //list_SaveTeamSumComboDamage.Add()
    }

    /// <summary>
    /// 計算總傷害
    /// </summary>
    private IEnumerator CoCountTotalDamage()
    {
        Debug.Log($"list_TeamSumComboDamageData.Count : {list_TeamSumComboDamageData.Count}");
        Debug.Log($"list_TeamSumComboDamageCopy.Count : {list_TeamSumComboDamageCopy.Count}");
        for (int i = 0; i < list_TeamSumComboDamageData.Count; i++)
        {
            for (int j = 0; j < list_TeamSumComboDamageCopy.Count; j++)
            {
                list_TeamSumComboDamageData[i] += list_TeamSumComboDamageCopy[j][i];               
            }
            Debug.Log($"隊伍第{i + 1}位傷害 : {list_TeamSumComboDamageData[i]}");
        }
        levelManager.SetPlayerCharacterDamage();
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// 清除總傷害資料
    /// </summary>
    public IEnumerator ClearTotalDamageData()
    {
        list_TeamSumComboDamageCopy.Clear();
        list_TeamSumComboDamageData.Clear();
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// 重製總傷害資料
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoResetTotalDamageData()
    {
        for (int i = 0; i < TowerOfAdventureGame.Inst.GetTeamData().list_SelectEditorTeamItemCardData.Count; i++)
        {
            list_TeamSumComboDamageData.Add(0);
        }
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// 取得隊伍成員總傷害清單
    /// </summary>
    /// <returns></returns>
    public List<int> GetTeamMemberTotalDMGList()
    {
        return list_TeamSumComboDamageData;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        UpBallPos();
        SaveAllBall();
        SpawnRandomBall();
        SaveAllBallPos();
        ListInit();
        //Debug.Log($"list_AllballPos[{0}] : " + list_AllballPos[0]);
        //Debug.Log("obj.transform.position : " + obj.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    Debug.Log("我有持續按滑鼠左鍵");
        //}
    }
}
