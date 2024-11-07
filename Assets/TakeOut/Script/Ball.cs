using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour
{
    public BallControl ballCtrl;
    public Vector3 newPos;
    public GameObject obj; //射線打到的東西
    ILevelManager levelManager;

    /// <summary>
    /// 球的id
    /// </summary>
    public int id;

    /// <summary>
    /// 元素類型
    /// </summary>
    public ElementType elementType;

    /// <summary>
    /// 球的動畫
    /// </summary>
    public Animator ani;

    /// <summary>
    /// 射線打到的球(左邊)
    /// </summary>
    RaycastHit2D[] newHitsLeft; //public看不到，就不公開

    /// <summary>
    /// 射線打到的球(右邊)
    /// </summary>
    RaycastHit2D[] newHitsRight;

    /// <summary>
    /// 射線打到的球(上邊)
    /// </summary>
    RaycastHit2D[] newHitsUp;

    /// <summary>
    /// 射線打到的球(下邊)
    /// </summary>
    RaycastHit2D[] newHitsDown;

    public bool canReDelete;

    private bool mouseUp;

    #region 測試GetAxis相關參數
    //public float speed;
    //public float mouseSpeed;
    //public bool savePos;
    //public float mouseXVariety;
    //public float mouseYVariety;
    #endregion

    #region 球的移動相關參數
    /// <summary>
    /// 球的邊界(Bounds就是collider的邊框)
    /// </summary>
    public Bounds bounds;

    /// <summary>
    /// 球移動到格子的bool
    /// </summary>
    public bool move;

    /// <summary>
    /// 被消除
    /// </summary>
    private bool isEliminated;

    /// <summary>
    /// 被拖曳
    /// </summary>
    public bool isDraged;
    /// <summary>
    /// 球有移動過
    /// </summary>
    public bool ballHaveMove;

    /// <summary>
    /// 開始滑鼠位置判斷
    /// </summary>
    private bool startMousePosJudge;
    /// <summary>
    /// 使用滑鼠位置判斷
    /// </summary>
    private bool useMousePosJudge;

    #endregion

    #region 大方向左邊判斷參數
    /// <summary>
    /// 第一次可被削除的球資料(水平方向)
    /// </summary>
    public List<Ball> list_1stTimeCanDeleteHorBall;

    /// <summary>
    /// 第二次可被削除的球資料(水平方向)
    /// </summary>
    public List<Ball> list_2ndTimeCanDeleteHorBall;

    /// <summary>
    /// 第三次可被削除的球資料(水平方向)
    /// </summary>
    public List<Ball> list_3rdTimeCanDeleteHorBall;

    /// <summary>
    /// 第一次可被削除的球資料(垂直方向)
    /// </summary>
    public List<Ball> list_1stTimeCanDeleteVarBall;

    /// <summary>
    /// 第二次可被削除的球資料(垂直方向)
    /// </summary>
    public List<Ball> list_2ndTimeCanDeleteVarBall;

    /// <summary>
    /// 第三次可被削除的球資料(垂直方向)
    /// </summary>
    public List<Ball> list_3rdTimeCanDeleteVarBall;

    /// <summary>
    /// 第一次垂直數量資料清單
    /// </summary>
    public List<int> list_1ndVarCountData;

    /// <summary>
    /// 第二次水平數量資料清單
    /// </summary>
    public List<int> list_2ndHorCountData;

    /// <summary>
    /// 第三次水平數量資料清單
    /// </summary>
    public List<int> list_3rdHorCountData;

    /// <summary>
    /// 第二次垂直數量資料清單
    /// </summary>
    public List<int> list_2ndVarCountData;

    /// <summary>
    /// 第三次垂直數量資料清單
    /// </summary>
    public List<int> list_3rdVarCountData;

    /// <summary>
    /// (第一次判斷)任一顆球的垂直數量有沒有3顆以上
    /// </summary>
    public bool varCountCanLink1st;

    /// <summary>
    /// (第二次判斷)任一顆球的水平數量有沒有3顆以上
    /// </summary>
    public bool horCountCanLink2nd;

    /// <summary>
    /// (第二次判斷)任一顆球的垂直數量有沒有3顆以上
    /// </summary>
    public bool varCountCanLink2nd;

    /// <summary>
    /// (第三次判斷)任一顆球的水平數量有沒有3顆以上
    /// </summary>
    public bool horCountCanLink3rd;

    /// <summary>
    /// (第三次判斷)任一顆球的垂直數量有沒有3顆以上
    /// </summary>
    public bool varCountCanLink3rd;

    #endregion

    #region 大方向上邊判斷參數
    /// <summary>
    /// 第一次可被削除的球資料垂直方向(大方向上邊判斷)
    /// </summary>
    public List<Ball> list_1stTimeCanDeleteVarBallBigUp;

    /// <summary>
    /// 第一次可被削除的球資料水平方向(大方向上邊判斷)
    /// </summary>
    public List<Ball> list_1stTimeCanDeleteHorBallBigUp;

    /// <summary>
    /// 第二次可被削除的球資料垂直方向(大方向上邊判斷)
    /// </summary>
    public List<Ball> list_2ndTimeCanDeleteVarBallBigUp;

    /// <summary>
    /// 第二次可被削除的球資料水平方向(大方向上邊判斷)
    /// </summary>
    public List<Ball> list_2ndTimeCanDeleteHorBallBigUp;

    /// <summary>
    /// 第三次可被削除的球資料垂直方向(大方向上邊判斷)
    /// </summary>
    public List<Ball> list_3rdTimeCanDeleteVarBallBigUp;

    /// <summary>
    /// 第三次可被削除的球資料水平方向(大方向上邊判斷)
    /// </summary>
    public List<Ball> list_3rdTimeCanDeleteHorBallBigUp;

    /// <summary>
    /// 第一次水平數量資料清單(大方向上方判斷)
    /// </summary>
    public List<int> list_1stHorCountDataBigUp;

    /// <summary>
    /// 第二次垂直數量資料清單(大方向上方判斷)
    /// </summary>
    public List<int> list_2ndVarCountDataBigUp;

    /// <summary>
    /// 第二次水平數量資料清單(大方向上方判斷)
    /// </summary>
    public List<int> list_2ndHorCountDataBigUp;

    /// <summary>
    /// 第三次垂直數量資料清單(大方向上方判斷)
    /// </summary>
    public List<int> list_3rdVarCountDataBigUp;

    /// <summary>
    /// 第三次水平數量資料清單(大方向上方判斷)
    /// </summary>
    public List<int> list_3rdHorCountDataBigUp;

    /// <summary>
    /// (第一次判斷)任一顆球的水平數量有沒有3顆以上(大方向上邊判斷)
    /// </summary>
    public bool horCountCanLink1stBigUp;

    /// <summary>
    /// (第二次判斷)任一顆球的垂直數量有沒有3顆以上(大方向上邊判斷)
    /// </summary>
    public bool varCountCanLink2ndBigUp;

    /// <summary>
    /// (第二次判斷)任一顆球的水平數量有沒有3顆以上(大方向上邊判斷)
    /// </summary>
    public bool horCountCanLink2ndBigUp;

    /// <summary>
    /// (第三次判斷)任一顆球的垂直數量有沒有3顆以上(大方向上邊判斷)
    /// </summary>
    public bool varCountCanLink3rdBigUp;

    /// <summary>
    /// (第三次判斷)任一顆球的水平數量有沒有3顆以上(大方向上邊判斷)
    /// </summary>
    public bool horCountCanLink3rdBigUp;

    #endregion

    #region 帶消除資料(1Combo)
    /// <summary>
    /// 1Combo的資料
    /// </summary>
    public List<Ball> list_TestBall;

    /// <summary>
    /// 待消除清單(1Combo)
    /// </summary>
    public List<List<Ball>> list_ReadyToEliminate;
    #endregion



    //--------------------分隔線---------------

    #region 直接把老師的寫法拿來套用，但發現不太行
    void MoveBall()
    {
        if (Input.GetMouseButton(0))
        {
            //newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
            transform.position = new Vector3(newPos.x, newPos.y, 90);
            Debug.Log("newPos : " + newPos);
        }
    }

    void Raycast_Ray2()  //老師的這種寫法只能適用3D的collider，2D完全沒反應
    {
        //Vector3 origin = transform.position;
        //Vector3 direction = transform.TransformDirection(Vector3.forward);
        //Ray ray1 = new Ray(origin, direction);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) //輸入滑鼠左鍵(按滑鼠左鍵)(只偵測一次)
        {
            Debug.Log("1");
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置(把滑鼠在螢幕上的位置轉換成射線)
            //prePos = Camera.main.ScreenToWorldPoint(new Vector3(121.12f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
            //Debug.Log("prePos : " + prePos);
            if (Physics.Raycast(ray2, out hit, Mathf.Infinity)) //若射線有打到東西(滑鼠游標有碰到東西)
            {
                Debug.Log("Down");

                Debug.Log(string.Format("Touch - Name:{0}, Position:{1}", hit.transform.name, hit.transform.position));
                if (obj == null && hit.transform.tag == "Ball")
                {
                    //startMousePoint = interactiveCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
                    //prePos = Camera.main.ScreenToWorldPoint(new Vector3(5751.8f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
                    //Debug.Log("prePos : " + prePos);
                    obj = hit.transform.gameObject; //把obj變成我滑鼠(射線)點到的東西
                    //objPrePos = obj.transform.position; //把obj的原始位置記下來
                }

            }
        }

        if (Input.GetMouseButton(0))
        {
            //newPos = Camera.main.ScreenToWorldPoint(new Vector3(121.12f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
            ////obj.transform.position = new Vector3(prePos.x, prePos.y, obj.transform.position.z); //把物體的位置變成滑鼠的位置
            //obj.transform.position += newPos - prePos;
            //prePos = Camera.main.ScreenToWorldPoint(new Vector3(121.12f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來

            if (obj != null)
            {
                newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
                obj.transform.position = newPos;

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            obj = null;
        }
    }

    void Raycast_Ray2D()  //偵測2D的collider需要用Physics2D.Raycast
    {
        //以下內容因每個腳本所掛的物件都會符合條件，所以30個腳本會同時間一起執行(非常可怕!!!)
        if (Input.GetMouseButtonDown(0)) //輸入滑鼠左鍵(按滑鼠左鍵)(只偵測一次)
        {
            Debug.Log("transform.name : " + transform.name);  //拿來檢查是否點到格子的collider
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置(把滑鼠在螢幕上的位置轉換成射線)
            RaycastHit2D hit = Physics2D.Raycast(ray2.origin, ray2.direction, Mathf.Infinity, 32);  //UI在layerMask階層裡是第5階層，所以填layerMask的數值就是2的5次方，也就是32。
            if (hit.collider) //若射線有打到東西(滑鼠游標有碰到東西)
            {
                //Debug.Log("hit.name : " + hit.transform.name);  //拿來檢查是否點到格子的collider
                if (obj == null && hit.transform.tag == "Ball")
                {
                    //startMousePoint = interactiveCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
                    //prePos = Camera.main.ScreenToWorldPoint(new Vector3(5751.8f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
                    //Debug.Log("prePos : " + prePos);
                    obj = hit.transform.gameObject; //把obj變成我滑鼠(射線)點到的東西
                    //objPrePos = obj.transform.position; //把obj的原始位置記下來
                    Debug.Log("obj.name : " + obj.name);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 90));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
            if (obj != null)
            {
                #region 設定球的移動範圍
                bounds = obj.transform.parent.GetComponent<BoxCollider>().bounds;
                Vector3 min = bounds.min;
                Vector3 max = bounds.max;
                newPos = new Vector3(Mathf.Clamp(newPos.x, min.x, max.x), Mathf.Clamp(newPos.y, min.y, max.y), 90);
                #endregion

                obj.transform.position = new Vector3(newPos.x, newPos.y, 90);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (obj != null)
            {
                //Debug.Log("id : " + id);
                obj.transform.position = ballCtrl.list_AllballPos[obj.GetComponent<Ball>().id - 1];
                CheckLink();
                obj = null;
            }
        }
    }

    #endregion

    #region 初始化的部分
    public void ListInit()
    {
        list_ReadyToEliminate = new List<List<Ball>>();
    }
    #endregion

    #region 找出有無可連線的資料(有沒有1Combo)

    /// <summary>
    /// 第一次判斷(前半部分)
    /// </summary>
    /// <param name="newHitsDirection">判斷的方向</param>
    /// <param name="count">判斷方向的相同類型數量</param>
    private void FirstJudgmentFirstHalf(RaycastHit2D[] newHitsDirection, int count)
    {
        #region 計算相同顏色數量
        for (int i = 0; i < newHitsDirection.Length; i++)
        {
            if (newHitsDirection[i].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
            {
                //顏色有相同的珠子就會紀錄數量           
                count++;
            }
            else
            {
                //遇到顏色不同的珠子就會停止檢查
                break;
            }
        }

        #endregion

        #region 相同顏色數量可連線
        if (count >= 3)
        {
            foreach (var ball in newHitsDirection)
            {
                if (ball.collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType && ball.collider.GetComponent<Ball>().isEliminated == false)
                {
                    if (newHitsDirection == newHitsLeft)
                    {
                        newHitsDirection[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBall.Add(ball.collider.GetComponent<Ball>());
                    }
                    
                    if (newHitsDirection  == newHitsUp)
                    {
                        newHitsDirection[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBallBigUp.Add(ball.collider.GetComponent<Ball>());
                    }

                    ball.collider.GetComponent<Ball>().isEliminated = true;
                }
                else
                {
                    break;
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// 第一次判斷(後半部分)
    /// </summary>
    /// <param name="newHitsDirection"></param>
    /// <param name="samecount"></param>
    private void FirstJudgmentSecondHalf(List<Ball> list_1stFirsrHalfData, int horcount, int varcount)
    {

        #region 加入計算數量清單
        if (list_1stFirsrHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBall)
        {
            for (int i = 0; i < list_1stFirsrHalfData.Count; i++)
            {
                list_1ndVarCountData.Add(varcount);
            }
        }

        if (list_1stFirsrHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBallBigUp)
        {
            for (int i = 0; i < list_1stFirsrHalfData.Count; i++)
            {
                list_1stHorCountDataBigUp.Add(horcount);
            }
        }
        #endregion

        #region 第一次判斷前半部份資料清單有相同顏色計算數量
        if (list_1stFirsrHalfData.Count >= 3)
        {
            for (int i = 0; i < list_1stFirsrHalfData.Count; i++)
            {
                if (list_1stFirsrHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBall)
                {                   
                    RaycastHit2D[] hitUpBall = list_1stFirsrHalfData[i].newHitsUp;
                    RaycastHit2D[] hitDownBall = list_1stFirsrHalfData[i].newHitsDown;
                    foreach (var ball in hitUpBall)
                    {                        
                        if (ball.collider.GetComponent<Ball>().elementType == list_1stFirsrHalfData[i].elementType)
                        {                           
                            for (int j = 0; j < list_1stFirsrHalfData.Count; j++)
                            {
                                if (i == j)
                                {                                   
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_1ndVarCountData[j]++; 
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var ball in hitDownBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_1stFirsrHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_1stFirsrHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_1ndVarCountData[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }


                if (list_1stFirsrHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBallBigUp)
                {                  
                    RaycastHit2D[] hitLeftBall = list_1stFirsrHalfData[i].newHitsLeft;
                    RaycastHit2D[] hitRightBall = list_1stFirsrHalfData[i].newHitsRight;
                    foreach (var ball in hitLeftBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_1stFirsrHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_1stFirsrHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_1stHorCountDataBigUp[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var ball in hitRightBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_1stFirsrHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_1stFirsrHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_1stHorCountDataBigUp[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region 次數減一
        for (int i = 0; i < list_1stFirsrHalfData.Count; i++)
        {
            if (list_1stFirsrHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBall)
            {
                newHitsLeft[0].collider.GetComponent<Ball>().list_1ndVarCountData[i]--;
            }

            if (list_1stFirsrHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBallBigUp)
            {
                newHitsLeft[0].collider.GetComponent<Ball>().list_1stHorCountDataBigUp[i]--;
            }
        }
        #endregion

        #region 判斷任一顆球數量可連線
        if (list_1stFirsrHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBall)
        {
            foreach (var count in newHitsLeft[0].collider.GetComponent<Ball>().list_1ndVarCountData)
            {
                if (count >= 3)
                {
                    newHitsLeft[0].collider.GetComponent<Ball>().varCountCanLink1st = true;
                }
            }
        }

        if (list_1stFirsrHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBallBigUp)
        {
            foreach (var count in newHitsLeft[0].collider.GetComponent<Ball>().list_1stHorCountDataBigUp)
            {
                if (count >= 3)
                {
                    newHitsLeft[0].collider.GetComponent<Ball>().horCountCanLink1stBigUp = true;
                }
            }
        }
        #endregion

        #region 確認可連線之後，把資料放入新的清單
        if (list_1stFirsrHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBall)
        {
            if (newHitsLeft[0].collider.GetComponent<Ball>().varCountCanLink1st)
            {
                for (int i = 0; i < list_1stFirsrHalfData.Count; i++)
                {
                    RaycastHit2D[] hitUpBall = list_1stFirsrHalfData[i].newHitsUp;
                    RaycastHit2D[] hitDownBall = list_1stFirsrHalfData[i].newHitsDown;
                    for (int j = 0; j < hitUpBall.Length; j++)
                    {
                        if (hitUpBall[j].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_1ndVarCountData.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_1ndVarCountData[k] >= 3 && i == k && hitUpBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBall.Add(hitUpBall[j].collider.GetComponent<Ball>());
                                        hitUpBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子向上判斷就會停止檢查，換向下判斷
                            break;
                        }
                    }

                    for (int j = 0; j < hitDownBall.Length; j++)
                    {
                        if (hitDownBall[j].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_1ndVarCountData.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_1ndVarCountData[k] >= 3 && i == k && hitDownBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBall.Add(hitDownBall[j].collider.GetComponent<Ball>());
                                        hitDownBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子就會停止檢查
                            break;
                        }
                    }
                }
            }
        }

        if (list_1stFirsrHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBallBigUp)
        {
            if (newHitsLeft[0].collider.GetComponent<Ball>().horCountCanLink1stBigUp)
            {
                for (int i = 0; i < list_1stFirsrHalfData.Count; i++)
                {
                    RaycastHit2D[] hitLeftBall = list_1stFirsrHalfData[i].newHitsLeft;
                    RaycastHit2D[] hitRightBall = list_1stFirsrHalfData[i].newHitsRight;
                    for (int j = 0; j < hitLeftBall.Length; j++)
                    {
                        if (hitLeftBall[j].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_1stHorCountDataBigUp.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_1stHorCountDataBigUp[k] >= 3 && i == k && hitLeftBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBallBigUp.Add(hitLeftBall[j].collider.GetComponent<Ball>());
                                        hitLeftBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子向上判斷就會停止檢查，換向下判斷
                            break;
                        }
                    }

                    for (int j = 0; j < hitRightBall.Length; j++)
                    {
                        if (hitRightBall[j].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_1stHorCountDataBigUp.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_1stHorCountDataBigUp[k] >= 3 && i == k && hitRightBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBallBigUp.Add(hitRightBall[j].collider.GetComponent<Ball>());
                                        hitRightBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子就會停止檢查
                            break;
                        }
                    }
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// 第二次判斷(前半部分)
    /// </summary>
    /// <param name="list_1stSecondHalfData">第一次判斷後半部分得到的資料</param>
    /// <param name="horcount"></param>
    /// <param name="varcount"></param>
    private void SecondJudgmentFirstHalf(List<Ball> list_1stSecondHalfData, int horcount, int varcount)
    {
        #region 加入計算數量清單
        if (list_1stSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBall)
        {
            for (int i = 0; i < list_1stSecondHalfData.Count; i++)
            {
                list_2ndHorCountData.Add(varcount);
            }
        }

        if (list_1stSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBallBigUp)
        {
            for (int i = 0; i < list_1stSecondHalfData.Count; i++)
            {
                list_2ndVarCountDataBigUp.Add(horcount);
            }
        }

        #endregion

        #region 第一次判斷後半部份資料清單有相同顏色計算數量
        if (list_1stSecondHalfData.Count > 0)
        {
            for (int i = 0; i < list_1stSecondHalfData.Count; i++)
            {
                if (list_1stSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBall)
                {
                    RaycastHit2D[] hitLeftBall = list_1stSecondHalfData[i].newHitsLeft;
                    RaycastHit2D[] hitRightBall = list_1stSecondHalfData[i].newHitsRight;
                    foreach (var ball in hitLeftBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_1stSecondHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_1stSecondHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountData[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var ball in hitRightBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_1stSecondHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_1stSecondHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountData[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }


                if (list_1stSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBallBigUp)
                {
                    RaycastHit2D[] hitUpBall = list_1stSecondHalfData[i].newHitsUp;
                    RaycastHit2D[] hitDownBall = list_1stSecondHalfData[i].newHitsDown;
                    foreach (var ball in hitUpBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_1stSecondHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_1stSecondHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountDataBigUp[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var ball in hitDownBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_1stSecondHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_1stSecondHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountDataBigUp[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region 次數減一
        //次數減一
        for (int i = 0; i < list_1stSecondHalfData.Count; i++)
        {
            if (list_1stSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBall)
            {
                newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountData[i]--;
            }

            if (list_1stSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBallBigUp)
            {
                newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountDataBigUp[i]--;
            }
        }
        #endregion

        #region 判斷任一顆球數量可連線
        if (list_1stSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBall)
        {
            foreach (var count in newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountData)
            {
                if (count >= 3)
                {
                    newHitsLeft[0].collider.GetComponent<Ball>().horCountCanLink2nd = true;
                }
            }
        }

        if (list_1stSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBallBigUp)
        {
            foreach (var count in newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountDataBigUp)
            {
                if (count >= 3)
                {
                    newHitsLeft[0].collider.GetComponent<Ball>().varCountCanLink2ndBigUp = true;
                }
            }
        }
        #endregion

        #region 確認可連線之後，把資料放入新的清單
        if (list_1stSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBall)
        {
            if (newHitsLeft[0].collider.GetComponent<Ball>().horCountCanLink2nd)
            {
                for (int i = 0; i < list_1stSecondHalfData.Count; i++)
                {
                    RaycastHit2D[] hitLeftBall = list_1stSecondHalfData[i].newHitsLeft;
                    RaycastHit2D[] hitRightBall = list_1stSecondHalfData[i].newHitsRight;
                    for (int j = 0; j < hitLeftBall.Length; j++)
                    {
                        if (hitLeftBall[j].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountData.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountData[k] >= 3 && i == k && hitLeftBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBall.Add(hitLeftBall[j].collider.GetComponent<Ball>());
                                        hitLeftBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子向上判斷就會停止檢查，換向下判斷
                            break;
                        }
                    }

                    for (int j = 0; j < hitRightBall.Length; j++)
                    {
                        if (hitRightBall[j].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountData.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountData[k] >= 3 && i == k && hitRightBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBall.Add(hitRightBall[j].collider.GetComponent<Ball>());
                                        hitRightBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子就會停止檢查
                            break;
                        }
                    }
                }
            }
        }

        if (list_1stSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBallBigUp)
        {
            if (newHitsLeft[0].collider.GetComponent<Ball>().varCountCanLink2ndBigUp)
            {
                for (int i = 0; i < list_1stSecondHalfData.Count; i++)
                {
                    RaycastHit2D[] hitUpBall = list_1stSecondHalfData[i].newHitsUp;
                    RaycastHit2D[] hitDownBall = list_1stSecondHalfData[i].newHitsDown;
                    for (int j = 0; j < hitUpBall.Length; j++)
                    {
                        if (hitUpBall[j].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountDataBigUp.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountDataBigUp[k] >= 3 && i == k && hitUpBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBallBigUp.Add(hitUpBall[j].collider.GetComponent<Ball>());
                                        hitUpBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子向上判斷就會停止檢查，換向下判斷
                            break;
                        }
                    }

                    for (int j = 0; j < hitDownBall.Length; j++)
                    {
                        if (hitDownBall[j].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountDataBigUp.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountDataBigUp[k] >= 3 && i == k && hitDownBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBallBigUp.Add(hitDownBall[j].collider.GetComponent<Ball>());
                                        hitDownBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子就會停止檢查
                            break;
                        }
                    }
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// 第二次判斷(後半部分)
    /// </summary>
    /// <param name="list_1stSecondHalfData"></param>
    /// <param name="horcount"></param>
    /// <param name="varcount"></param>
    private void SecondJudgmentSecondHalf(List<Ball> list_2ndFirstHalfData, int horcount, int varcount)
    {
        #region 加入計算數量清單
        if (list_2ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBall)
        {
            for (int i = 0; i < list_2ndFirstHalfData.Count; i++)
            {
                list_2ndVarCountData.Add(varcount);
            }
        }

        if (list_2ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBallBigUp)
        {
            for (int i = 0; i < list_2ndFirstHalfData.Count; i++)
            {
                list_2ndHorCountDataBigUp.Add(horcount);
            }
        }

        #endregion

        #region 第一次判斷後半部份資料清單有相同顏色計算數量
        if (list_2ndFirstHalfData.Count > 0)
        {
            for (int i = 0; i < list_2ndFirstHalfData.Count; i++)
            {
                if (list_2ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBall)
                {
                    RaycastHit2D[] hitUpBall = list_2ndFirstHalfData[i].newHitsUp;
                    RaycastHit2D[] hitDownBall = list_2ndFirstHalfData[i].newHitsDown;
                    foreach (var ball in hitUpBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_2ndFirstHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_2ndFirstHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountData[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var ball in hitDownBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_2ndFirstHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_2ndFirstHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountData[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }


                if (list_2ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBallBigUp)
                {
                    RaycastHit2D[] hitLeftBall = list_2ndFirstHalfData[i].newHitsLeft;
                    RaycastHit2D[] hitRightBall = list_2ndFirstHalfData[i].newHitsRight;              
                    foreach (var ball in hitLeftBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_2ndFirstHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_2ndFirstHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountDataBigUp[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var ball in hitRightBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_2ndFirstHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_2ndFirstHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountDataBigUp[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region 次數減一
        for (int i = 0; i < list_2ndFirstHalfData.Count; i++)
        {
            if (list_2ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBall)
            {
                newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountData[i]--;
            }

            if (list_2ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBallBigUp)
            {
                newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountDataBigUp[i]--;
            }
        }
        #endregion

        #region 判斷任一顆球數量可連線
        if (list_2ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBall)
        {
            foreach (var count in newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountData)
            {
                if (count >= 3)
                {
                    newHitsLeft[0].collider.GetComponent<Ball>().varCountCanLink2nd = true;
                }
            }
        }

        if (list_2ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBallBigUp)
        {
            foreach (var count in newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountDataBigUp)
            {
                if (count >= 3)
                {
                    newHitsLeft[0].collider.GetComponent<Ball>().horCountCanLink2ndBigUp = true;
                }
            }
        }
        #endregion

        #region 確認可連線之後，把資料放入新的清單
        //確認可連線之後，把資料放入新的清單
        if (list_2ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBall)
        {
            if (newHitsLeft[0].collider.GetComponent<Ball>().varCountCanLink2nd)
            {
                for (int i = 0; i < list_2ndFirstHalfData.Count; i++)
                {
                    RaycastHit2D[] hitUpBall = list_2ndFirstHalfData[i].newHitsUp;
                    RaycastHit2D[] hitDownBall = list_2ndFirstHalfData[i].newHitsDown;
       
                    for (int j = 0; j < hitUpBall.Length; j++)
                    {
                        if (hitUpBall[j].collider.GetComponent<Ball>().elementType == list_2ndFirstHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountData.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountData[k] >= 3 && i == k && hitUpBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBall.Add(hitUpBall[j].collider.GetComponent<Ball>());
                                        hitUpBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子向上判斷就會停止檢查，換向下判斷
                            break;
                        }
                    }

                    for (int j = 0; j < hitDownBall.Length; j++)
                    {
                        if (hitDownBall[j].collider.GetComponent<Ball>().elementType == list_2ndFirstHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountData.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_2ndVarCountData[k] >= 3 && i == k && hitDownBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBall.Add(hitDownBall[j].collider.GetComponent<Ball>());
                                        hitDownBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子就會停止檢查
                            break;
                        }
                    }
                }
            }
        }

        if (list_2ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBallBigUp)
        {
            if (newHitsLeft[0].collider.GetComponent<Ball>().horCountCanLink2ndBigUp)
            {
                for (int i = 0; i < list_2ndFirstHalfData.Count; i++)
                {
                    RaycastHit2D[] hitLeftBall = list_2ndFirstHalfData[i].newHitsLeft;
                    RaycastHit2D[] hitRightBall = list_2ndFirstHalfData[i].newHitsRight;
                    for (int j = 0; j < hitLeftBall.Length; j++)
                    {
                        if (hitLeftBall[j].collider.GetComponent<Ball>().elementType == list_2ndFirstHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountDataBigUp.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountDataBigUp[k] >= 3 && i == k && hitLeftBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBallBigUp.Add(hitLeftBall[j].collider.GetComponent<Ball>());
                                        hitLeftBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子向上判斷就會停止檢查，換向下判斷
                            break;
                        }
                    }

                    for (int j = 0; j < hitRightBall.Length; j++)
                    {
                        if (hitRightBall[j].collider.GetComponent<Ball>().elementType == list_2ndFirstHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountDataBigUp.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_2ndHorCountDataBigUp[k] >= 3 && i == k && hitRightBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBallBigUp.Add(hitRightBall[j].collider.GetComponent<Ball>());
                                        hitRightBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子就會停止檢查
                            break;
                        }
                    }
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// 第三次判斷(前半部分)
    /// </summary>
    /// <param name="list_2ndSecondHalfData"></param>
    /// <param name="horcount"></param>
    /// <param name="varcount"></param>
    private void ThirdJudgmentFirstHalf(List<Ball> list_2ndSecondHalfData, int horcount, int varcount)
    {
        #region 加入計算數量清單
        if (list_2ndSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBall)
        {
            for (int i = 0; i < list_2ndSecondHalfData.Count; i++)
            {
                list_3rdHorCountData.Add(varcount);
            }
        }

        if (list_2ndSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBallBigUp)
        {
            for (int i = 0; i < list_2ndSecondHalfData.Count; i++)
            {
                list_3rdVarCountDataBigUp.Add(horcount);
            }
        }

        #endregion

        #region 第一次判斷後半部份資料清單有相同顏色計算數量
        if (list_2ndSecondHalfData.Count > 0)
        {
            for (int i = 0; i < list_2ndSecondHalfData.Count; i++)
            {
                if (list_2ndSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBall)
                {
                    RaycastHit2D[] hitLeftBall = list_2ndSecondHalfData[i].newHitsLeft;
                    RaycastHit2D[] hitRightBall = list_2ndSecondHalfData[i].newHitsRight;

                    foreach (var ball in hitLeftBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_2ndSecondHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_2ndSecondHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountData[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var ball in hitRightBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_2ndSecondHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_2ndSecondHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountData[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (list_2ndSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBallBigUp)
                {
                    RaycastHit2D[] hitUpBall = list_2ndSecondHalfData[i].newHitsUp;
                    RaycastHit2D[] hitDownBall = list_2ndSecondHalfData[i].newHitsDown;
                    foreach (var ball in hitUpBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_2ndSecondHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_2ndSecondHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountDataBigUp[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var ball in hitDownBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_2ndSecondHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_2ndSecondHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountDataBigUp[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region 次數減一
        for (int i = 0; i < list_2ndSecondHalfData.Count; i++)
        {
            if (list_2ndSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBall)
            {
                newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountData[i]--;
            }

            if (list_2ndSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBallBigUp)
            {
                newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountDataBigUp[i]--;
            }
        }
        #endregion

        #region 判斷任一顆球數量可連線
        if (list_2ndSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBall)
        {
            foreach (var count in newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountData)
            {
                if (count >= 3)
                {
                    newHitsLeft[0].collider.GetComponent<Ball>().horCountCanLink3rd = true;
                }
            }
        }

        if (list_2ndSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBallBigUp)
        {
            foreach (var count in newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountDataBigUp)
            {
                if (count >= 3)
                {
                    newHitsLeft[0].collider.GetComponent<Ball>().varCountCanLink3rdBigUp = true;
                }
            }
        }
        #endregion

        #region 確認可連線之後，把資料放入新的清單
        if (list_2ndSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBall)
        {
            if (newHitsLeft[0].collider.GetComponent<Ball>().horCountCanLink3rd)
            {
                for (int i = 0; i < list_2ndSecondHalfData.Count; i++)
                {
                    RaycastHit2D[] hitLeftBall = list_2ndSecondHalfData[i].newHitsLeft;
                    RaycastHit2D[] hitRightBall = list_2ndSecondHalfData[i].newHitsRight;              
                    for (int j = 0; j < hitLeftBall.Length; j++)
                    {
                        if (hitLeftBall[j].collider.GetComponent<Ball>().elementType == list_2ndSecondHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountData.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountData[k] >= 3 && i == k && hitLeftBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBall.Add(hitLeftBall[j].collider.GetComponent<Ball>());
                                        hitLeftBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子向上判斷就會停止檢查，換向下判斷
                            break;
                        }
                    }

                    for (int j = 0; j < hitRightBall.Length; j++)
                    {
                        if (hitRightBall[j].collider.GetComponent<Ball>().elementType == list_2ndSecondHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountData.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountData[k] >= 3 && i == k && hitRightBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBall.Add(hitRightBall[j].collider.GetComponent<Ball>());
                                        hitRightBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子就會停止檢查
                            break;
                        }
                    }
                }
            }
        }

        if (list_2ndSecondHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBallBigUp)
        {
            if (newHitsLeft[0].collider.GetComponent<Ball>().varCountCanLink3rdBigUp)
            {
                for (int i = 0; i < list_2ndSecondHalfData.Count; i++)
                {
                    RaycastHit2D[] hitUpBall = list_2ndSecondHalfData[i].newHitsUp;
                    RaycastHit2D[] hitDownBall = list_2ndSecondHalfData[i].newHitsDown;
                    for (int j = 0; j < hitUpBall.Length; j++)
                    {
                        if (hitUpBall[j].collider.GetComponent<Ball>().elementType == list_2ndSecondHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountDataBigUp.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountDataBigUp[k] >= 3 && i == k && hitUpBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBallBigUp.Add(hitUpBall[j].collider.GetComponent<Ball>());
                                        hitUpBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子向上判斷就會停止檢查，換向下判斷
                            break;
                        }
                    }

                    for (int j = 0; j < hitDownBall.Length; j++)
                    {
                        if (hitDownBall[j].collider.GetComponent<Ball>().elementType == list_2ndSecondHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountDataBigUp.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountDataBigUp[k] >= 3 && i == k && hitDownBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBallBigUp.Add(hitDownBall[j].collider.GetComponent<Ball>());
                                        hitDownBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子就會停止檢查
                            break;
                        }
                    }
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// 第三次判斷(後半部分)
    /// </summary>
    /// <param name="list_2ndSecondHalfData"></param>
    /// <param name="horcount"></param>
    /// <param name="varcount"></param>
    private void ThirdJudgmentSecondHalf(List<Ball> list_3ndFirstHalfData, int horcount, int varcount)
    {
        #region 加入計算數量清單
        if (list_3ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBall)
        {
            for (int i = 0; i < list_3ndFirstHalfData.Count; i++)
            {
                list_3rdVarCountData.Add(varcount);
            }
        }

        if (list_3ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBallBigUp)
        {
            for (int i = 0; i < list_3ndFirstHalfData.Count; i++)
            {
                list_3rdHorCountDataBigUp.Add(horcount);
            }
        }

        #endregion

        #region 第一次判斷後半部份資料清單有相同顏色計算數量
        if (list_3ndFirstHalfData.Count > 0)
        {
            for (int i = 0; i < list_3ndFirstHalfData.Count; i++)
            {
                if (list_3ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBall)
                {
                    RaycastHit2D[] hitUpBall = list_3ndFirstHalfData[i].newHitsUp;
                    RaycastHit2D[] hitDownBall = list_3ndFirstHalfData[i].newHitsDown;                
                    foreach (var ball in hitUpBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_3ndFirstHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_3ndFirstHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountData[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var ball in hitDownBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_3ndFirstHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_3ndFirstHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountData[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (list_3ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBallBigUp)
                {
                    RaycastHit2D[] hitLeftBall = list_3ndFirstHalfData[i].newHitsLeft;
                    RaycastHit2D[] hitRightBall = list_3ndFirstHalfData[i].newHitsRight;
                    foreach (var ball in hitLeftBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_3ndFirstHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_3ndFirstHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountDataBigUp[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var ball in hitRightBall)
                    {
                        if (ball.collider.GetComponent<Ball>().elementType == list_3ndFirstHalfData[i].elementType)
                        {
                            for (int j = 0; j < list_3ndFirstHalfData.Count; j++)
                            {
                                if (i == j)
                                {
                                    newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountDataBigUp[j]++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region 次數減一
        for (int i = 0; i < list_3ndFirstHalfData.Count; i++)
        {
            if (list_3ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBall)
            {
                newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountData[i]--;
            }

            if (list_3ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBallBigUp)
            {
                newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountDataBigUp[i]--;
            }
        }
        #endregion

        #region 判斷任一顆球數量可連線
        if (list_3ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBall)
        {
            foreach (var count in newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountData)
            {
                if (count >= 3)
                {
                    newHitsLeft[0].collider.GetComponent<Ball>().varCountCanLink3rd = true;
                }
            }
        }

        if (list_3ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBallBigUp)
        {
            foreach (var count in newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountDataBigUp)
            {
                if (count >= 3)
                {
                    newHitsLeft[0].collider.GetComponent<Ball>().horCountCanLink3rdBigUp = true;
                }
            }
        }
        #endregion

        #region 確認可連線之後，把資料放入新的清單
        if (list_3ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBall)
        {
            if (newHitsLeft[0].collider.GetComponent<Ball>().varCountCanLink3rd)
            {
                for (int i = 0; i < list_3ndFirstHalfData.Count; i++)
                {
                    RaycastHit2D[] hitUpBall = list_3ndFirstHalfData[i].newHitsUp;
                    RaycastHit2D[] hitDownBall = list_3ndFirstHalfData[i].newHitsDown;
                    for (int j = 0; j < hitUpBall.Length; j++)
                    {
                        if (hitUpBall[j].collider.GetComponent<Ball>().elementType == list_3ndFirstHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountData.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountData[k] >= 3 && i == k && hitUpBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBall.Add(hitUpBall[j].collider.GetComponent<Ball>());
                                        hitUpBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子向上判斷就會停止檢查，換向下判斷
                            break;
                        }
                    }

                    for (int j = 0; j < hitDownBall.Length; j++)
                    {
                        if (hitDownBall[j].collider.GetComponent<Ball>().elementType == list_3ndFirstHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountData.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_3rdVarCountData[k] >= 3 && i == k && hitDownBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBall.Add(hitDownBall[j].collider.GetComponent<Ball>());
                                        hitDownBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子就會停止檢查
                            break;
                        }
                    }
                }
            }
        }

        if (list_3ndFirstHalfData == newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBallBigUp)
        {
            if (newHitsLeft[0].collider.GetComponent<Ball>().horCountCanLink3rdBigUp)
            {
                for (int i = 0; i < list_3ndFirstHalfData.Count; i++)
                {
                    RaycastHit2D[] hitLeftBall = list_3ndFirstHalfData[i].newHitsLeft;
                    RaycastHit2D[] hitRightBall = list_3ndFirstHalfData[i].newHitsRight;
                    for (int j = 0; j < hitLeftBall.Length; j++)
                    {
                        if (hitLeftBall[j].collider.GetComponent<Ball>().elementType == list_3ndFirstHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountDataBigUp.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountDataBigUp[k] >= 3 && i == k && hitLeftBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBallBigUp.Add(hitLeftBall[j].collider.GetComponent<Ball>());
                                        hitLeftBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子向上判斷就會停止檢查，換向下判斷
                            break;
                        }
                    }

                    for (int j = 0; j < hitRightBall.Length; j++)
                    {
                        if (hitRightBall[j].collider.GetComponent<Ball>().elementType == list_3ndFirstHalfData[i].elementType)
                        {
                            if (j >= 1)
                            {
                                for (int k = 0; k < newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountDataBigUp.Count; k++)
                                {
                                    if (newHitsLeft[0].collider.GetComponent<Ball>().list_3rdHorCountDataBigUp[k] >= 3 && i == k && hitRightBall[j].collider.GetComponent<Ball>().isEliminated == false)
                                    {
                                        newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBallBigUp.Add(hitRightBall[j].collider.GetComponent<Ball>());
                                        hitRightBall[j].collider.GetComponent<Ball>().isEliminated = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //遇到顏色不同的珠子就會停止檢查
                            break;
                        }
                    }
                }
            }
        }

        #endregion

    }

    #endregion

    #region 球的出現和隱藏
    /// <summary>
    /// 隱藏
    /// </summary>
    /// <param name="hit"></param>
    public void Fade()
    {
        #region 暴力隱藏
        //Image hitBall = hit.transform.GetComponent<Image>();       
        //hitBall.color = new Color(255, 255, 255, 0);
        //hitBall.color.a = 0; //不能這樣寫
        #endregion

        #region 動畫隱藏      
        transform.GetComponent<Animator>().SetTrigger("Fade");
        #endregion
    }

    /// <summary>
    /// 球出現
    /// </summary>
    public void Appear()
    {
        //Image hitBall = GetComponent<Image>();
        //hitBall.color = new Color(255, 255, 255, 255);
        transform.GetComponent<Animator>().SetTrigger("bailappearagain");
    }

    #endregion

    #region 碰到格子時交換位置
    /// <summary>
    /// 球交換位置(有類似插植移動的效果) (參數越大數度越快 0到1之間)
    /// </summary>
    /// <param name="speed"></param>
    private void BallChangePos(float speed)    
    {
        if (move) //只有要交換位置的那顆球的move = true，所以transform.position就會是指那顆要交換位置的球，id也是他自己的
        {
            transform.position = Vector3.MoveTowards(transform.position, ballCtrl.list_AllballPos[id - 1], speed * Time.deltaTime);
            if (ballCtrl.isDragedBall.ballHaveMove)
            {
                ballCtrl.isDragedBall.ballHaveMove = false;
            }

            if (transform.position == ballCtrl.list_AllballPos[id - 1])
            {
                move = false;
                //ballHaveMove = false; //這樣寫是對方的變成false 被拖曳的還是true

            }
        }
    }

    #endregion

    #region 可消除的球移動至上方並落下
    /// <summary>
    /// 移動至上方(動畫事件)
    /// </summary>
    public void MoveToUp()
    {            
        for (int i = ballCtrl.list_AllBall.Count; i > 0; i--)
        {
            if (id == i)
            {
                transform.position = ballCtrl.list_UpBallPos[i - 1];
                id *= -1; //ID變成負的代表在上面的球
            }
        }
        
    }

    /// <summary>
    /// 確認落下
    /// </summary>
    public IEnumerator CoCheckFall()
    {
        yield return CoChangeIdBeforeFall();       
        move = true; 
    }

    /// <summary>
    /// 落下之前改變ID
    /// </summary>
    public IEnumerator CoChangeIdBeforeFall()
    {
        RaycastHit2D[] HitsDown = Physics2D.RaycastAll(transform.position, Vector2.down, 50, 32);
        if (HitsDown.Length == 1)
        {
            //珠子下面"沒有"其他球(打到的那一顆是自己)
            int re = 0;
            re = Mathf.Abs(id % 6);
            if (re == 0)
            {
                id = 30;
            }
            else
            {
                id = re + 24;
            }
        }
        else
        {
            //珠子下面"有"其他球
            id = HitsDown[1].collider.GetComponent<Ball>().id - 6;
        }
        yield return null;
    }

    #endregion

    #region 是否可進行再次消除
    /// <summary>
    /// 再次消除檢查
    /// </summary>
    public void ReDeleteCheck()
    {
        RaycastHit2D[] leftDirectionHits = Physics2D.RaycastAll(transform.position, Vector2.left, 20, 32);
        RaycastHit2D[] UpDirectionHits = Physics2D.RaycastAll(transform.position, Vector2.up, 20, 32);
        int leftcount = 0;
        int Upcount = 0;

        for (int i = 0; i < leftDirectionHits.Length; i++)
        {
            if (leftDirectionHits[i].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
            {              
                leftcount++;
            }
            else
            {
                //遇到顏色不同的珠子就會停止檢查
                break;
            }
        }

        for (int i = 0; i < UpDirectionHits.Length; i++)
        {
            if (UpDirectionHits[i].collider.GetComponent<Ball>().elementType == transform.GetComponent<Ball>().elementType)
            {               
                Upcount++;
            }
            else
            {
                //遇到顏色不同的珠子就會停止檢查
                break;
            }
        }

        //次數減一 (只檢查單個方向不用)
        //leftcount--;
        //Upcount--;

        if (leftcount >= 3 || Upcount >= 3)
        {
            canReDelete = true;
        }
        else
        {
            //沒符合條件就要變成false(把上一次檢查有3顆以上的球，但這次卻沒有，把他的canReDelete改成false)
            canReDelete = false;
        }
    }

    #endregion

    #region 再次消除之前清除可消除的球資料(把取得的1Combo資料清掉)
    /// <summary>
    /// 消除資料初始化
    /// </summary>
    public void DeleteDataReset()
    {
        #region 大方向左邊判斷參數重製
        if (list_1stTimeCanDeleteHorBall.Count > 0)
        {
            list_1stTimeCanDeleteHorBall.Clear();
        }

        if (list_2ndTimeCanDeleteHorBall.Count > 0)
        {
            list_2ndTimeCanDeleteHorBall.Clear();
        }

        if (list_3rdTimeCanDeleteHorBall.Count > 0)
        {
            list_3rdTimeCanDeleteHorBall.Clear();
        }

        if (list_1stTimeCanDeleteVarBall.Count > 0)
        {
            list_1stTimeCanDeleteVarBall.Clear();
        }

        if (list_2ndTimeCanDeleteVarBall.Count > 0)
        {
            list_2ndTimeCanDeleteVarBall.Clear();
        }

        if (list_3rdTimeCanDeleteVarBall.Count > 0)
        {
            list_3rdTimeCanDeleteVarBall.Clear();
        }

        if (list_1ndVarCountData.Count > 0)
        {
            list_1ndVarCountData.Clear();
        }

        if (list_2ndHorCountData.Count > 0)
        {
            list_2ndHorCountData.Clear();
        }

        if (list_3rdHorCountData.Count > 0)
        {
            list_3rdHorCountData.Clear();
        }

        if (list_2ndVarCountData.Count > 0)
        {
            list_2ndVarCountData.Clear();
        }

        if (list_3rdVarCountData.Count > 0)
        {
            list_3rdVarCountData.Clear();
        }

        varCountCanLink1st = false;
        horCountCanLink2nd = false;
        varCountCanLink2nd = false;
        horCountCanLink3rd = false;
        varCountCanLink3rd = false;
        #endregion

        #region 大方向上邊判斷參數重製
        if (list_1stTimeCanDeleteVarBallBigUp.Count > 0)
        {
            list_1stTimeCanDeleteVarBallBigUp.Clear();
        }

        if (list_1stTimeCanDeleteHorBallBigUp.Count > 0)
        {
            list_1stTimeCanDeleteHorBallBigUp.Clear();
        }

        if (list_2ndTimeCanDeleteVarBallBigUp.Count > 0)
        {
            list_2ndTimeCanDeleteVarBallBigUp.Clear();
        }

        if (list_2ndTimeCanDeleteHorBallBigUp.Count > 0)
        {
            list_2ndTimeCanDeleteHorBallBigUp.Clear();
        }

        if (list_3rdTimeCanDeleteVarBallBigUp.Count > 0)
        {
            list_3rdTimeCanDeleteVarBallBigUp.Clear();
        }

        if (list_3rdTimeCanDeleteHorBallBigUp.Count > 0)
        {
            list_3rdTimeCanDeleteHorBallBigUp.Clear();
        }

        if (list_1stHorCountDataBigUp.Count > 0)
        {
            list_1stHorCountDataBigUp.Clear();
        }

        if (list_2ndVarCountDataBigUp.Count > 0)
        {
            list_2ndVarCountDataBigUp.Clear();
        }

        if (list_2ndHorCountDataBigUp.Count > 0)
        {
            list_2ndHorCountDataBigUp.Clear();
        }

        if (list_3rdVarCountDataBigUp.Count > 0)
        {
            list_3rdVarCountDataBigUp.Clear();
        }

        if (list_3rdHorCountDataBigUp.Count > 0)
        {
            list_3rdHorCountDataBigUp.Clear();
        }

        horCountCanLink1stBigUp = false;
        varCountCanLink2ndBigUp = false;
        horCountCanLink2ndBigUp = false;
        varCountCanLink3rdBigUp = false;
        horCountCanLink3rdBigUp = false;

        #endregion

        #region 待消除相關清單重製
        if (list_TestBall.Count >= 0)
        {
            list_TestBall.Clear();
        }

        if (list_ReadyToEliminate.Count >= 0)
        {
            list_ReadyToEliminate.Clear();
        }
        #endregion

        #region 被消除參數重製
        isEliminated = false;
        #endregion

    }

    #endregion

    #region 轉珠邏輯

    /// <summary>
    /// 開始轉珠
    /// </summary>
    private void StartTurnBeads()
    {
        if (ballCtrl.turnBeadsStatus == TurnBeadsStatus.WaitTurnBeads)
        {
            if (levelManager.TurnBeatsTimesUp())
            {
                //DeleteBall();
                OnMouseUp();
                return;
            }
            else
            {
                //Debug.Log("開始轉珠");
                useMousePosJudge = true;
                //Debug.Log("正在按著滑鼠");
                levelManager.ShowTurnBeatsTime();
                levelManager.ReduceTime();

                #region 拖曳的時候需要記得的內容
                isDraged = true;
                ballCtrl.isDragedBall = GetComponent<Ball>();
                #endregion

                //OnMouseDrag >> 只會執行點到的那個物件身上的OnMouseDrag的內容，其他的不會執行(非常重要)
                //Debug.Log("transform.name : " + transform.name);
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置(把滑鼠在螢幕上的位置轉換成射線)
                RaycastHit2D hit = Physics2D.Raycast(ray2.origin, ray2.direction, Mathf.Infinity, 32);  //UI在layerMask階層裡是第5階層，所以填layerMask的數值就是2的5次方，也就是32。
                if (hit.collider) //若射線有打到東西(滑鼠游標有碰到東西)
                {
                    //Debug.Log("hit.collider.name : " + hit.collider.name);
                    if (obj == null && hit.transform.tag == "Ball")
                    {
                        //startMousePoint = interactiveCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
                        //prePos = Camera.main.ScreenToWorldPoint(new Vector3(5751.8f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來

                        obj = hit.transform.gameObject; //把obj變成我滑鼠(射線)點到的東西
                                                        //objPrePos = obj.transform.position; //把obj的原始位置記下來
                        //Debug.Log("Drag.id : " + id);
                    }
                }

                //newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 90));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
                newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 90));//把滑鼠在螢幕上的位置轉換成世界座標後記起來

                //float mouseX = Input.GetAxis("Mouse X");
                //float mouseY = Input.GetAxis("Mouse Y");
                if (obj != null)
                {
                    newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 90));//把滑鼠在螢幕上的位置轉換成世界座標後記起來

                    #region 設定球的移動範圍
                    bounds = obj.transform.parent.GetComponent<BoxCollider>().bounds;
                    Vector3 min = bounds.min;
                    Vector3 max = bounds.max;
                    newPos = new Vector3(Mathf.Clamp(newPos.x, min.x, max.x), Mathf.Clamp(newPos.y, min.y, max.y), 90);
                    #endregion

                    obj.transform.position = new Vector3(newPos.x, newPos.y, 90);
                    //obj.transform.position = new Vector3(newPos.x - (mouseSpeed * Time.deltaTime), newPos.y - (mouseSpeed * Time.deltaTime), 90);
                    //savePos = true;
                }
                //mouseXVariety += mouseX * speed * Time.deltaTime;
                //mouseYVariety += mouseY * speed * Time.deltaTime;
                //obj.transform.position = new Vector3(newPos.x + mouseXVariety, newPos.y + mouseYVariety, 90);

                //因為在快速移動之下會碰不到格子，所以再用滑鼠位置來判斷是否需要交換位置。

                #region 滑鼠位置判斷移動

                //1到6格的水平判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 6; i++)  //(180 * i) - 90 + 80
                    {
                        if (Input.mousePosition.x >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.x <= (87 * i) - 43.5f + 38.7f && Input.mousePosition.y > 348)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;
                                for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                {
                                    if (id == 0 + i - 1 && id > 0)
                                    {
                                        if (id + 1 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }

                                    if (id == 0 + i + 1)
                                    {
                                        if (id - 1 == ballCtrl.list_AllBall[j].id)
                                        {
                                            if (id > ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                //7到12格的水平判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 6; i++) //(180 * i) - 90 - 80    (180 * i) - 90 + 80
                    {
                        if (Input.mousePosition.x >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.x <= (87 * i) - 43.5f + 38.7f && Input.mousePosition.y > 261 && Input.mousePosition.y <= 348)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;
                                for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                {
                                    if (id == 6 + i - 1 && id > 6)
                                    {
                                        if (id + 1 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }

                                    if (id == 6 + i + 1)
                                    {
                                        if (id - 1 == ballCtrl.list_AllBall[j].id)
                                        {
                                            if (id > ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                //13到18格的水平判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        if (Input.mousePosition.x >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.x <= (87 * i) - 43.5f + 38.7f && Input.mousePosition.y > 174 && Input.mousePosition.y <= 261)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;
                                for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                {
                                    if (id == 12 + i - 1 && id > 12)
                                    {
                                        if (id + 1 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }

                                    if (id == 12 + i + 1)
                                    {
                                        if (id - 1 == ballCtrl.list_AllBall[j].id)
                                        {
                                            if (id > ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                //19到24格的水平判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        if (Input.mousePosition.x >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.x <= (87 * i) - 43.5f + 38.7f && Input.mousePosition.y > 87 && Input.mousePosition.y <= 174)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;
                                for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                {
                                    if (id == 18 + i - 1 && id > 18)
                                    {
                                        if (id + 1 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }

                                    if (id == 18 + i + 1)
                                    {
                                        if (id - 1 == ballCtrl.list_AllBall[j].id)
                                        {
                                            if (id > ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                //25到30格的水平判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        if (Input.mousePosition.x >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.x <= (87 * i) - 43.5f + 38.7f && Input.mousePosition.y <= 87)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;
                                for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                {
                                    if (id == 24 + i - 1 && id > 24)
                                    {
                                        if (id + 1 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }

                                    if (id == 24 + i + 1)
                                    {
                                        if (id - 1 == ballCtrl.list_AllBall[j].id)
                                        {
                                            if (id > ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                //1到25的垂直判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 5; i++)  //(180 * i) - 90 - 80    (180 * i) - 90 + 80)
                    {
                        if (Input.mousePosition.x <= 87 && Input.mousePosition.y >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.y <= (87 * i) - 43.5f + 38.7f)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;

                                if (i == 1 && id == 19)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id + 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }

                                if (i == 2)
                                {
                                    if (id == 13)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }

                                    }

                                    if (id == 25)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }


                                if (i == 3)
                                {
                                    if (id == 7)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 19)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 4)
                                {
                                    if (id == 1)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 13)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 5 && id == 7)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id - 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                //2到26的垂直判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (Input.mousePosition.x > 87 && Input.mousePosition.x <= 174 && Input.mousePosition.y >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.y <= (87 * i) - 43.5f + 38.7f)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;

                                if (i == 1 && id == 20)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id + 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }

                                if (i == 2)
                                {
                                    if (id == 14)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }

                                    }

                                    if (id == 26)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }


                                if (i == 3)
                                {
                                    if (id == 8)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 20)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 4)
                                {
                                    if (id == 2)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 14)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 5 && id == 8)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id - 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                //3到27的垂直判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (Input.mousePosition.x > 174 && Input.mousePosition.x <= 261 && Input.mousePosition.y >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.y <= (87 * i) - 43.5f + 38.7f)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;

                                if (i == 1 && id == 21)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id + 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }

                                if (i == 2)
                                {
                                    if (id == 15)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }

                                    }

                                    if (id == 27)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }


                                if (i == 3)
                                {
                                    if (id == 9)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 21)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 4)
                                {
                                    if (id == 3)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 15)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 5 && id == 9)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id - 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                //4到28的垂直判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (Input.mousePosition.x > 261 && Input.mousePosition.x <= 348 && Input.mousePosition.y >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.y <= (87 * i) - 43.5f + 38.7f)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;

                                if (i == 1 && id == 22)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id + 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }

                                if (i == 2)
                                {
                                    if (id == 16)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }

                                    }

                                    if (id == 28)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }


                                if (i == 3)
                                {
                                    if (id == 10)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 22)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 4)
                                {
                                    if (id == 4)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 16)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 5 && id == 10)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id - 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                //5到29的垂直判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (Input.mousePosition.x > 348 && Input.mousePosition.x <= 435 && Input.mousePosition.y >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.y <= (87 * i) - 43.5f + 38.7f)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;

                                if (i == 1 && id == 23)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id + 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }

                                if (i == 2)
                                {
                                    if (id == 17)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }

                                    }

                                    if (id == 29)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }


                                if (i == 3)
                                {
                                    if (id == 11)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 23)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 4)
                                {
                                    if (id == 5)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 17)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 5 && id == 11)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id - 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                //6到30的垂直判斷
                if (!ballHaveMove)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (Input.mousePosition.x > 435 && Input.mousePosition.y >= (87 * i) - 43.5f - 38.7f && Input.mousePosition.y <= (87 * i) - 43.5f + 38.7f)
                        {
                            if (startMousePosJudge)
                            {
                                int temp = 0;
                                temp = id;

                                if (i == 1 && id == 24)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id + 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }

                                if (i == 2)
                                {
                                    if (id == 18)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }

                                    }

                                    if (id == 30)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }


                                if (i == 3)
                                {
                                    if (id == 12)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 24)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 4)
                                {
                                    if (id == 6)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id + 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }

                                    if (id == 18)
                                    {
                                        for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                        {
                                            if (id - 6 == ballCtrl.list_AllBall[j].id)
                                            {
                                                id = ballCtrl.list_AllBall[j].id;
                                                ballCtrl.list_AllBall[j].id = temp;
                                                //Debug.Log("使用滑鼠位置判斷來移動");
                                                levelManager.PlayTurnBeatsAudio();
                                                ballCtrl.list_AllBall[j].move = true;
                                                startMousePosJudge = false;
                                                break; //要break不然會錯
                                            }
                                        }
                                    }
                                }

                                if (i == 5 && id == 12)
                                {
                                    for (int j = 0; j < ballCtrl.list_AllBall.Count; j++)
                                    {
                                        if (id - 6 == ballCtrl.list_AllBall[j].id)
                                        {
                                            id = ballCtrl.list_AllBall[j].id;
                                            ballCtrl.list_AllBall[j].id = temp;
                                            //Debug.Log("使用滑鼠位置判斷來移動");
                                            levelManager.PlayTurnBeatsAudio();
                                            ballCtrl.list_AllBall[j].move = true;
                                            startMousePosJudge = false;
                                            break; //要break不然會錯
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            startMousePosJudge = true;
                        }
                    }
                }

                #endregion
            }



        }
    }

    //hit為碰到的對象
    private void OnTriggerEnter2D(Collider2D hit)
    {
        //初判是一play的時候，每個球都會碰到格子，所以才掛掉
        //碰到格子就交換位置
        for (int i = 0; i < 30; i++)
        {
            if (hit.transform.tag == "Grid")
            {
                if (hit.GetComponent<Grid>().id == ballCtrl.list_AllBall[i].id && hit.GetComponent<Grid>().id != id && isDraged) //後面的判斷是要防止一play就觸發條件
                {
                    //c = a;
                    //Debug.Log("c :" + c);
                    //Debug.Log("hit.transform.name : " + hit.transform.name); //碰得到格子沒問題
                    int temp = 0;
                    temp = id;
                    id = ballCtrl.list_AllBall[i].id;
                    ballCtrl.list_AllBall[i].id = temp;

                    #region 球互換位置
                    //ballCtrl.list_AllBall[i].transform.position = ballCtrl.list_AllballPos[obj.GetComponent<Ball>().id - 1]; //不能拿射線打到的obj的id，不然會搶obj的位置
                    //ballCtrl.list_AllBall[a].transform.position = ballCtrl.list_AllballPos[ballCtrl.list_AllBall[a].id - 1]; //不能拿射線打到的obj的id，不然會搶obj的位置(2顆球互換位置)
                    #endregion

                    #region 球互換位置(有類似插植移動的效果)
                    //Debug.Log("碰到格子才移動");
                    Debug.Log("格子的id : " + hit.GetComponent<Grid>().id);
                    ballCtrl.list_AllBall[i].move = true;
                    ballHaveMove = true;
                    #endregion
                }
            }
        }

        //怕球往格子和格子之間移動會碰不到格子，所以新增判定線判斷

        #region 判定線的判斷條件(水平線)
        if (hit.transform.tag == "DeHorLine_1To6Up" && id > 6 && id < 13)
        {
            if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
            {
                Debug.Log("hit.transform.name : " + hit.transform.name);
                int temp = 0;
                temp = id;

                for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                {
                    if (ballCtrl.list_AllBall[i].id + 6 == id)
                    {
                        id = ballCtrl.list_AllBall[i].id;
                        ballCtrl.list_AllBall[i].id = temp;
                        Debug.Log("使用判定線移動");
                        ballCtrl.list_AllBall[i].move = true;
                        break; //找到就馬上停止檢查省效能(不寫也不會跳錯)
                    }
                }


                #region 球互換位置
                //Debug.Log("使用判定線移動");
                //ballCtrl.list_AllBall[temp - 7].move = true;
                #endregion
            }
        }

        if (hit.transform.tag == "DeHorLine_7To12Up" && id > 12 && id < 19)
        {
            if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
            {
                Debug.Log("hit.transform.name : " + hit.transform.name);
                int temp = 0;
                temp = id;
                //id = ballCtrl.list_AllBall[temp - 7].id;
                //ballCtrl.list_AllBall[temp - 7].id = temp;

                for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                {
                    if (ballCtrl.list_AllBall[i].id + 6 == id)
                    {
                        id = ballCtrl.list_AllBall[i].id;
                        ballCtrl.list_AllBall[i].id = temp;
                        Debug.Log("使用判定線移動");
                        ballCtrl.list_AllBall[i].move = true;
                        break; //找到就馬上停止檢查省效能(不寫也不會跳錯)
                    }
                }

                #region 球互換位置
                //Debug.Log("使用判定線移動");
                //ballCtrl.list_AllBall[temp - 7].move = true;
                #endregion
            }
        }

        if (hit.transform.tag == "DeHorLine_13To18Up" && id > 18 && id < 25)
        {
            if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
            {
                Debug.Log("hit.transform.name : " + hit.transform.name);
                int temp = 0;
                temp = id;
                //id = ballCtrl.list_AllBall[temp - 7].id;
                //ballCtrl.list_AllBall[temp - 7].id = temp;

                for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                {
                    if (ballCtrl.list_AllBall[i].id + 6 == id)
                    {
                        id = ballCtrl.list_AllBall[i].id;
                        ballCtrl.list_AllBall[i].id = temp;
                        Debug.Log("使用判定線移動");
                        ballCtrl.list_AllBall[i].move = true;
                        break; //找到就馬上停止檢查省效能(不寫也不會跳錯)
                    }
                }

                #region 球互換位置
                //Debug.Log("使用判定線移動");
                //ballCtrl.list_AllBall[temp - 7].move = true;
                #endregion
            }
        }

        if (hit.transform.tag == "DeHorLine_19To24Up" && id > 24 && id < 31)
        {
            if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
            {
                Debug.Log("hit.transform.name : " + hit.transform.name);
                int temp = 0;
                temp = id;
                //id = ballCtrl.list_AllBall[temp - 7].id;
                //ballCtrl.list_AllBall[temp - 7].id = temp;

                for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                {
                    if (ballCtrl.list_AllBall[i].id + 6 == id)
                    {
                        id = ballCtrl.list_AllBall[i].id;
                        ballCtrl.list_AllBall[i].id = temp;
                        Debug.Log("使用判定線移動");
                        ballCtrl.list_AllBall[i].move = true;
                        break; //找到就馬上停止檢查省效能(不寫也不會跳錯)
                    }
                }

                #region 球互換位置
                //Debug.Log("使用判定線移動");
                //ballCtrl.list_AllBall[temp - 7].move = true;
                #endregion
            }
        }


        if (hit.transform.tag == "DeHorLine_7To12Down" && id > 0 && id < 7)
        {
            if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
            {
                Debug.Log("hit.transform.name : " + hit.transform.name);
                int temp = 0;
                temp = id;
                //id = ballCtrl.list_AllBall[temp + 5].id;
                //ballCtrl.list_AllBall[temp + 5].id = temp;

                for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                {
                    if (id + 6 == ballCtrl.list_AllBall[i].id)
                    {
                        id = ballCtrl.list_AllBall[i].id;
                        ballCtrl.list_AllBall[i].id = temp;
                        Debug.Log("使用判定線移動");
                        ballCtrl.list_AllBall[i].move = true;
                        break; //要break不然會錯
                    }
                }

                #region 球互換位置
                //Debug.Log("使用判定線移動");
                //ballCtrl.list_AllBall[temp + 5].move = true;


                #endregion
            }
        }

        if (hit.transform.tag == "DeHorLine_13To18Down" && id > 6 && id < 13)
        {
            if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
            {
                Debug.Log("hit.transform.name : " + hit.transform.name);
                int temp = 0;
                temp = id;
                //id = ballCtrl.list_AllBall[temp + 5].id;
                //ballCtrl.list_AllBall[temp + 5].id = temp;

                for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                {
                    if (id + 6 == ballCtrl.list_AllBall[i].id)
                    {
                        id = ballCtrl.list_AllBall[i].id;
                        ballCtrl.list_AllBall[i].id = temp;
                        Debug.Log("使用判定線移動");
                        ballCtrl.list_AllBall[i].move = true;
                        break; //要break不然會錯
                    }
                }

                #region 球互換位置
                //Debug.Log("使用判定線移動");
                //ballCtrl.list_AllBall[temp + 5].move = true;
                #endregion
            }
        }

        if (hit.transform.tag == "DeHorLine_19To24Down" && id > 12 && id < 19)
        {
            if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
            {
                Debug.Log("hit.transform.name : " + hit.transform.name);
                int temp = 0;
                temp = id;
                //id = ballCtrl.list_AllBall[temp + 5].id;
                //ballCtrl.list_AllBall[temp + 5].id = temp;

                for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                {
                    if (id + 6 == ballCtrl.list_AllBall[i].id)
                    {
                        id = ballCtrl.list_AllBall[i].id;
                        ballCtrl.list_AllBall[i].id = temp;
                        Debug.Log("使用判定線移動");
                        ballCtrl.list_AllBall[i].move = true;
                        break; //要break不然會錯
                    }
                }

                #region 球互換位置
                //Debug.Log("使用判定線移動");
                //ballCtrl.list_AllBall[temp + 5].move = true;
                #endregion
            }
        }

        if (hit.transform.tag == "DeHorLine_25To30Down" && id > 18 && id < 25)
        {
            if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
            {
                Debug.Log("hit.transform.name : " + hit.transform.name);
                int temp = 0;
                temp = id;
                //id = ballCtrl.list_AllBall[temp + 5].id;
                //ballCtrl.list_AllBall[temp + 5].id = temp;

                for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                {
                    if (id + 6 == ballCtrl.list_AllBall[i].id)
                    {
                        id = ballCtrl.list_AllBall[i].id;
                        ballCtrl.list_AllBall[i].id = temp;
                        Debug.Log("使用判定線移動");
                        ballCtrl.list_AllBall[i].move = true;
                        break; //要break不然會錯
                    }
                }

                #region 球互換位置
                //Debug.Log("使用判定線移動");
                //ballCtrl.list_AllBall[temp + 5].move = true;
                #endregion
            }
        }
        #endregion

        #region 判定線的判斷條件(垂直線)
        if (hit.transform.tag == "DeVarLine_2To26Right")
        {
            if (id == 1 || id == 7 || id == 13 || id == 19 || id == 25)
            {
                if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
                {
                    Debug.Log("hit.transform.name : " + hit.transform.name);
                    int temp = 0;
                    temp = id;
                    //id = ballCtrl.list_AllBall[temp].id;
                    //ballCtrl.list_AllBall[temp].id = temp;

                    for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                    {
                        if (id + 1 == ballCtrl.list_AllBall[i].id)
                        {
                            id = ballCtrl.list_AllBall[i].id;
                            ballCtrl.list_AllBall[i].id = temp;
                            Debug.Log("使用判定線移動");
                            ballCtrl.list_AllBall[i].move = true;
                            break; //要break不然會錯
                        }
                    }

                    #region 球互換位置
                    //Debug.Log("使用判定線移動");
                    //ballCtrl.list_AllBall[temp].move = true;
                    #endregion
                }
            }
        }

        if (hit.transform.tag == "DeVarLine_3To27Right")
        {
            if (id == 2 || id == 8 || id == 14 || id == 20 || id == 26)
            {
                if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
                {
                    Debug.Log("hit.transform.name : " + hit.transform.name);
                    int temp = 0;
                    temp = id;
                    //id = ballCtrl.list_AllBall[temp].id;
                    //ballCtrl.list_AllBall[temp].id = temp;

                    for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                    {
                        if (id + 1 == ballCtrl.list_AllBall[i].id)
                        {
                            id = ballCtrl.list_AllBall[i].id;
                            ballCtrl.list_AllBall[i].id = temp;
                            Debug.Log("使用判定線移動");
                            ballCtrl.list_AllBall[i].move = true;
                            break; //要break不然會錯
                        }
                    }

                    #region 球互換位置
                    //Debug.Log("使用判定線移動");
                    //ballCtrl.list_AllBall[temp].move = true;
                    #endregion
                }
            }
        }

        if (hit.transform.tag == "DeVarLine_4To28Right")
        {
            if (id == 3 || id == 9 || id == 15 || id == 21 || id == 27)
            {
                if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
                {
                    Debug.Log("hit.transform.name : " + hit.transform.name);
                    int temp = 0;
                    temp = id;
                    //id = ballCtrl.list_AllBall[temp].id;
                    //ballCtrl.list_AllBall[temp].id = temp;

                    for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                    {
                        if (id + 1 == ballCtrl.list_AllBall[i].id)
                        {
                            id = ballCtrl.list_AllBall[i].id;
                            ballCtrl.list_AllBall[i].id = temp;
                            Debug.Log("使用判定線移動");
                            ballCtrl.list_AllBall[i].move = true;
                            break; //要break不然會錯
                        }
                    }

                    #region 球互換位置
                    //Debug.Log("使用判定線移動");
                    //ballCtrl.list_AllBall[temp].move = true;
                    #endregion
                }
            }
        }

        if (hit.transform.tag == "DeVarLine_5To29Right")
        {
            if (id == 4 || id == 10 || id == 16 || id == 22 || id == 28)
            {
                if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
                {
                    Debug.Log("hit.transform.name : " + hit.transform.name);
                    int temp = 0;
                    temp = id;
                    //id = ballCtrl.list_AllBall[temp].id;
                    //ballCtrl.list_AllBall[temp].id = temp;

                    for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                    {
                        if (id + 1 == ballCtrl.list_AllBall[i].id)
                        {
                            id = ballCtrl.list_AllBall[i].id;
                            ballCtrl.list_AllBall[i].id = temp;
                            Debug.Log("使用判定線移動");
                            ballCtrl.list_AllBall[i].move = true;
                            break; //要break不然會錯
                        }
                    }

                    #region 球互換位置
                    //Debug.Log("使用判定線移動");
                    //ballCtrl.list_AllBall[temp].move = true;
                    #endregion
                }
            }
        }

        if (hit.transform.tag == "DeVarLine_6To30Right")
        {
            if (id == 5 || id == 11 || id == 17 || id == 23 || id == 29)
            {
                if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
                {
                    Debug.Log("hit.transform.name : " + hit.transform.name);
                    int temp = 0;
                    temp = id;
                    //id = ballCtrl.list_AllBall[temp].id;
                    //ballCtrl.list_AllBall[temp].id = temp;

                    for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                    {
                        if (id + 1 == ballCtrl.list_AllBall[i].id)
                        {
                            id = ballCtrl.list_AllBall[i].id;
                            ballCtrl.list_AllBall[i].id = temp;
                            Debug.Log("使用判定線移動");
                            ballCtrl.list_AllBall[i].move = true;
                            break; //要break不然會錯
                        }
                    }

                    #region 球互換位置
                    //Debug.Log("使用判定線移動");
                    //ballCtrl.list_AllBall[temp].move = true;
                    #endregion
                }
            }
        }

        if (hit.transform.tag == "DeVarLine_1To25Left")
        {
            if (id == 2 || id == 8 || id == 14 || id == 20 || id == 26)
            {
                if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
                {
                    Debug.Log("hit.transform.name : " + hit.transform.name);
                    int temp = 0;
                    temp = id;
                    //id = ballCtrl.list_AllBall[temp - 2].id;
                    //ballCtrl.list_AllBall[temp - 2].id = temp;

                    for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                    {
                        if (id - 1 == ballCtrl.list_AllBall[i].id)
                        {
                            id = ballCtrl.list_AllBall[i].id;
                            ballCtrl.list_AllBall[i].id = temp;
                            Debug.Log("使用判定線移動");
                            ballCtrl.list_AllBall[i].move = true;
                            break; //要break不然會錯
                        }
                    }

                    #region 球互換位置
                    //Debug.Log("使用判定線移動");
                    //ballCtrl.list_AllBall[temp - 2].move = true;
                    #endregion
                }
            }
        }

        if (hit.transform.tag == "DeVarLine_2To26Left")
        {
            if (id == 3 || id == 9 || id == 15 || id == 21 || id == 27)
            {
                if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
                {
                    Debug.Log("hit.transform.name : " + hit.transform.name);
                    int temp = 0;
                    temp = id;
                    //id = ballCtrl.list_AllBall[temp - 2].id;
                    //ballCtrl.list_AllBall[temp - 2].id = temp;

                    for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                    {
                        if (id - 1 == ballCtrl.list_AllBall[i].id)
                        {
                            id = ballCtrl.list_AllBall[i].id;
                            ballCtrl.list_AllBall[i].id = temp;
                            Debug.Log("使用判定線移動");
                            ballCtrl.list_AllBall[i].move = true;
                            break; //要break不然會錯
                        }
                    }

                    #region 球互換位置
                    //Debug.Log("使用判定線移動");
                    //ballCtrl.list_AllBall[temp - 2].move = true;
                    #endregion
                }
            }
        }

        if (hit.transform.tag == "DeVarLine_3To27Left")
        {
            if (id == 4 || id == 10 || id == 16 || id == 22 || id == 28)
            {
                if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
                {
                    Debug.Log("hit.transform.name : " + hit.transform.name);
                    int temp = 0;
                    temp = id;
                    //id = ballCtrl.list_AllBall[temp - 2].id;
                    //ballCtrl.list_AllBall[temp - 2].id = temp;

                    for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                    {
                        if (id - 1 == ballCtrl.list_AllBall[i].id)
                        {
                            id = ballCtrl.list_AllBall[i].id;
                            ballCtrl.list_AllBall[i].id = temp;
                            Debug.Log("使用判定線移動");
                            ballCtrl.list_AllBall[i].move = true;
                            break; //要break不然會錯
                        }
                    }

                    #region 球互換位置
                    //Debug.Log("使用判定線移動");
                    //ballCtrl.list_AllBall[temp - 2].move = true;
                    #endregion
                }
            }
        }

        if (hit.transform.tag == "DeVarLine_4To28Left")
        {
            if (id == 5 || id == 11 || id == 17 || id == 23 || id == 29)
            {
                if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
                {
                    Debug.Log("hit.transform.name : " + hit.transform.name);
                    int temp = 0;
                    temp = id;
                    //id = ballCtrl.list_AllBall[temp - 2].id;
                    //ballCtrl.list_AllBall[temp - 2].id = temp;

                    for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                    {
                        if (id - 1 == ballCtrl.list_AllBall[i].id)
                        {
                            id = ballCtrl.list_AllBall[i].id;
                            ballCtrl.list_AllBall[i].id = temp;
                            Debug.Log("使用判定線移動");
                            ballCtrl.list_AllBall[i].move = true;
                            break; //要break不然會錯
                        }
                    }

                    #region 球互換位置
                    //Debug.Log("使用判定線移動");
                    //ballCtrl.list_AllBall[temp - 2].move = true;
                    #endregion
                }
            }
        }

        if (hit.transform.tag == "DeVarLine_5To29Left")
        {
            if (id == 6 || id == 12 || id == 18 || id == 24 || id == 30)
            {
                if (isDraged && ballHaveMove == false) //防止一play就觸發的條件
                {
                    Debug.Log("hit.transform.name : " + hit.transform.name);
                    int temp = 0;
                    temp = id;
                    //id = ballCtrl.list_AllBall[temp - 2].id;
                    //ballCtrl.list_AllBall[temp - 2].id = temp;

                    for (int i = 0; i < ballCtrl.list_AllBall.Count; i++)
                    {
                        if (id - 1 == ballCtrl.list_AllBall[i].id)
                        {
                            id = ballCtrl.list_AllBall[i].id;
                            ballCtrl.list_AllBall[i].id = temp;
                            Debug.Log("使用判定線移動");
                            ballCtrl.list_AllBall[i].move = true;
                            break; //要break不然會錯
                        }
                    }

                    #region 球互換位置
                    //Debug.Log("使用判定線移動");
                    //ballCtrl.list_AllBall[temp - 2].move = true;
                    #endregion
                }
            }
        }
        #endregion
    }
    #endregion

    #region 消除邏輯相關
    /// <summary>
    /// 開始消除球
    /// </summary>
    private IEnumerator CoStartDeleteBall()
    {
        if (ballCtrl.turnBeadsStatus == TurnBeadsStatus.WaitTurnBeads)
        {
            ballCtrl.turnBeadsStatus = TurnBeadsStatus.DeleteBeads;
            if (obj != null)
            {
                yield return CoDragBallMoveNewPos();
                //StartCoroutine(ballCtrl.CoFullVersionDelete());
                yield return ballCtrl.CoFullVersionDelete();
                yield return CoWaitProcessFinish();
                obj = null;
            }
        }
    }

    /// <summary>
    /// 拖曳的球移動到新的位置
    /// </summary>
    private IEnumerator CoDragBallMoveNewPos()
    {
        obj.transform.position = ballCtrl.list_AllballPos[obj.GetComponent<Ball>().id - 1];
        yield return new WaitForSeconds(0.05f); //要格一段時間後再設限全版，不然會出問題
    }

    /// <summary>
    /// 偵測各方位珠子顏色
    /// </summary>
    public void RaycastAllDirections()
    {
        newHitsUp = Physics2D.RaycastAll(transform.position, Vector2.up, 25, 32);
        newHitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left, 25, 32);
        newHitsRight = Physics2D.RaycastAll(transform.position, Vector2.right, 25, 32);
        newHitsDown = Physics2D.RaycastAll(transform.position, Vector2.down, 25, 32);
    }

    /// <summary>
    /// 確認連線
    /// </summary>
    public void CheckLink()
    {
        //射線的原點都是obj.transform.position，也就是我控制的那顆珠子  >> 把程式碼放在OnMouseUp以後，就不用再加obj了
        //Physics2D.RaycastAll >> 自己也會被打到!!!

        int horCount = 0; //水平數量
        int ball_varCount1st = 0;
        int ball_horCount2nd = 0;
        int ball_varCount2nd = 0;
        int ball_horCount3rd = 0;
        int ball_varCount3rd = 0;

        int ball_varCount1stBigUp = 0;
        int ball_horCount1stBigUp = 0;
        int ball_varCount2ndBigUp = 0;
        int ball_horCount2ndBigUp = 0;
        int ball_varCount3rdBigUp = 0;
        int ball_horCount3rdBigUp = 0;
        //--------------------------------------

        #region 大方向左邊判斷
        FirstJudgmentFirstHalf(newHitsLeft, horCount);
        FirstJudgmentSecondHalf(newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBall, ball_horCount1stBigUp, ball_varCount1st);
        SecondJudgmentFirstHalf(newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBall, ball_horCount2nd, ball_varCount2ndBigUp);
        SecondJudgmentSecondHalf(newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBall, ball_horCount2ndBigUp, ball_varCount2nd);
        ThirdJudgmentFirstHalf(newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBall, ball_horCount3rd, ball_varCount3rdBigUp);
        ThirdJudgmentSecondHalf(newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBall, ball_horCount3rdBigUp, ball_varCount3rd);
        #endregion

        #region 大方向上邊判斷    
        FirstJudgmentFirstHalf(newHitsUp, ball_varCount1stBigUp);
        FirstJudgmentSecondHalf(newHitsUp[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBallBigUp, ball_horCount1stBigUp, ball_varCount1st);
        SecondJudgmentFirstHalf(newHitsUp[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBallBigUp, ball_horCount2nd, ball_varCount2ndBigUp);
        SecondJudgmentSecondHalf(newHitsUp[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBallBigUp, ball_horCount2ndBigUp, ball_varCount2nd);
        ThirdJudgmentFirstHalf(newHitsUp[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBallBigUp, ball_horCount3rd, ball_varCount3rdBigUp);
        ThirdJudgmentSecondHalf(newHitsUp[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBallBigUp, ball_horCount3rdBigUp, ball_varCount3rd);
        #endregion

        #region 這裡整理球的部分最後再做

        #region 把資料放入一個資料裡面
        //把資料放入一個資料裡面           
        foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBall)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBall)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBall)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBall)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBall)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBall)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        //------------------------------分隔線-------------------------------------------------------------------------------
        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBallBigUp)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBallBigUp)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBallBigUp)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBallBigUp)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBallBigUp)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBallBigUp)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Add(ball);
        }
        #endregion

        if (newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall.Count > 0)
        {
            newHitsLeft[0].collider.GetComponent<Ball>().list_ReadyToEliminate.Add(newHitsLeft[0].collider.GetComponent<Ball>().list_TestBall);
            //Debug.Log("newHitsLeft[0].collider.GetComponent<Ball>().list_ReadyToEliminate.Count : " + newHitsLeft[0].collider.GetComponent<Ball>().list_ReadyToEliminate.Count);
        }

        if (newHitsLeft[0].collider.GetComponent<Ball>().list_ReadyToEliminate.Count > 0)
        {
            for (int i = 0; i < newHitsLeft[0].collider.GetComponent<Ball>().list_ReadyToEliminate.Count; i++)
            {
                foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_ReadyToEliminate[i])
                {
                    //Debug.Log($"ID : { newHitsLeft[0].collider.GetComponent<Ball>().id}, 偵測到符合條件的球的ID : {ball.id}");
                }
            }
        }

        foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBall)
        {
            //Debug.Log($"ID : { newHitsLeft[0].collider.GetComponent<Ball>().id}, 第二次可被剃除的球資料(水平方向) : {ball.id}");
        }

        foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBall)
        {
            //Debug.Log($"ID : { newHitsLeft[0].collider.GetComponent<Ball>().id}, 第二次可被剃除的球資料(垂直方向) : {ball.id}");
        }

        foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBall)
        {
            //Debug.Log($"ID : { newHitsLeft[0].collider.GetComponent<Ball>().id}, 第三次可被剃除的球資料(水平方向) : {ball.id}");
        }

        foreach (var ball in newHitsLeft[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBall)
        {
            //Debug.Log($"ID : { newHitsLeft[0].collider.GetComponent<Ball>().id}, 第三次可被剃除的球資料(垂直方向) : {ball.id}");
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteVarBallBigUp)
        {
            //Debug.Log($"ID : { newHitsUp[0].collider.GetComponent<Ball>().id}, 第一次可被剃除的球資料垂直方向(大方向上邊判斷) : {ball.id}");
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_1stTimeCanDeleteHorBallBigUp)
        {
            //Debug.Log($"ID : { newHitsUp[0].collider.GetComponent<Ball>().id}, 第一次可被剃除的球資料水平方向(大方向上邊判斷) : {ball.id}");
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteVarBallBigUp)
        {
            //Debug.Log($"ID : { newHitsUp[0].collider.GetComponent<Ball>().id}, 第二次可被剃除的球資料垂直方向(大方向上邊判斷) : {ball.id}");
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_2ndTimeCanDeleteHorBallBigUp)
        {
            //Debug.Log($"ID : { newHitsUp[0].collider.GetComponent<Ball>().id}, 第二次可被剃除的球資料水平方向(大方向上邊判斷) : {ball.id}");
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteVarBallBigUp)
        {
            //Debug.Log($"ID : { newHitsUp[0].collider.GetComponent<Ball>().id}, 第三次可被剃除的球資料垂直方向(大方向上邊判斷) : {ball.id}");
        }

        foreach (var ball in newHitsUp[0].collider.GetComponent<Ball>().list_3rdTimeCanDeleteHorBallBigUp)
        {
            //Debug.Log($"ID : { newHitsUp[0].collider.GetComponent<Ball>().id}, 第三次可被剃除的球資料水平方向(大方向上邊判斷) : {ball.id}");
        }
        #endregion

    }

    #endregion

    /// <summary>
    /// 等待流程結束
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoWaitProcessFinish()
    {
        //ballCtrl.turnBeadsStatus = TurnBeadsStatus.WaitTurnBeads;
        ballCtrl.turnBeadsStatus = TurnBeadsStatus.WaitProcessFinish;
        mouseUp = false;
        //Debug.Log("可以轉珠");
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// 消除球
    /// </summary>
    public void DeleteBall()
    {
        if (!mouseUp)
        {
            mouseUp = true;
            isDraged = false;
            useMousePosJudge = false;
            //WaterLevel1Manager.inst.obj_CantTurnBeatsPanel.SetActive(true);
            //WaterLevel1Manager.inst.CantTurnBeatsPanelOpen();
            levelManager.CantTurnBeatsPanelOpen();
            StartCoroutine(CoStartDeleteBall());
        }
        useMousePosJudge = false;
    }

    private void OnMouseDrag()
    {
        //useMousePosJudge = true;  //改在StartTurnBeads內執行
        StartTurnBeads();
        //Debug.Log("正在按著滑鼠");
    }


    public void OnMouseUp()
    {       
        DeleteBall();
    }


    public void SetLevelManager(ILevelManager levelManager)
    {
        this.levelManager = levelManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
        BallChangePos(70);
        if (useMousePosJudge)
        {
            //Debug.Log("Update轉珠");
            StartTurnBeads();
        }

    }

    private void LateUpdate()
    {
        if (useMousePosJudge)
        {
            //Debug.Log("LateUpdate轉珠");
            StartTurnBeads();
        }
    }
    private void FixedUpdate()
    {
        if (useMousePosJudge)
        {
            //Debug.Log("FixedUpdate轉珠");
            StartTurnBeads();
        }
    }
}
