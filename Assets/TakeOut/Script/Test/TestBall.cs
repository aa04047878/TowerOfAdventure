using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBall : MonoBehaviour
{
    public GameObject obj;
    public Vector3 objPrePos;
    public Vector3 newPos;
    public Bounds bounds;

    //-----------------
 
    public List<int> list_num;
    public List<List<int>> list_xdddd;


   

    public void ListInit()
    {
        list_num = new List<int>();
        list_xdddd = new List<List<int>>();
        list_num.Add(1);
        list_num.Add(2);
        list_num.Add(3);
        list_num.Add(4);
        list_num.Add(5);
        list_xdddd.Add(list_num);

        for (int i = 0; i < list_xdddd.Count; i++)
        {
            foreach (var item in list_xdddd[0])
            {
                Debug.Log(item);
            }
        }
        
    }

    void Raycast_Ray2D()  //偵測2D的collider需要用Physics2D.Raycast
    {
        if (Input.GetMouseButtonDown(0)) //輸入滑鼠左鍵(按滑鼠左鍵)(只偵測一次)
        {
            Debug.Log("1");
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置(把滑鼠在螢幕上的位置轉換成射線)
            RaycastHit2D hit = Physics2D.Raycast(ray2.origin, ray2.direction, 100, 32);  //UI在layerMask階層裡是第5階層，所以填layerMask的數值就是2的5次方，也就是32。
            if (hit.collider) //若射線有打到東西(滑鼠游標有碰到東西)
            {
                //Debug.Log("hit.collider.name : " +hit.transform.name);
                if (obj == null && hit.transform.tag == "Ball")
                {
                    //startMousePoint = interactiveCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
                    //prePos = Camera.main.ScreenToWorldPoint(new Vector3(5751.8f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
                    //Debug.Log("prePos : " + prePos);
                    obj = hit.transform.gameObject; //把obj變成我滑鼠(射線)點到的東西
                    objPrePos = obj.transform.position; //把obj的原始位置記下來
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
                //obj.transform.position = ballCtrl.list_AllballPos[obj.GetComponent<Ball>().id - 1];
                obj = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        Debug.Log("hit.transform.name : " + hit.transform.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        //ListInit();
    }

    // Update is called once per frame
    void Update()
    {
        Raycast_Ray2D();
        if (Input.GetKeyDown(KeyCode.D))
        {
            list_num.Clear();
            Debug.Log("已刪除");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < list_xdddd.Count; i++)
            {
                foreach (var item in list_xdddd[0])
                {
                    Debug.Log(item);
                }
            }
        }
    }
}
