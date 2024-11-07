using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCardAction : MonoBehaviour
{
    public RawImage DrawCardOncePicture;
    private bool startDetect;

    public GameObject obj;
    public Vector3 v3Org;
    public LayerMask _CardLayerMask;
    public Camera interactiveCamera;
    public int touchCard;
    public Vector3 endPoint;
    public Vector3 prePos;

    public Vector3 textPos;

    #region Lerp
    public float moveDuration;
    public float elapsedTime;
    private bool isMoving = false;
    #endregion

    public Animator ani_DrawCardEffect;

    //RaycastAll (ray : Ray,                              distance : float = Mathf.Infinity, layerMask : int = kDefaultRaycastLayers) : RaycastHit[]
    //            ray包辦(原始座標、從原始座標建立方向).  射線距離(長度)                     遮罩(限定碰撞)
    //与所有在ray这条射线上的物体碰撞并返回所有被碰撞体，RaycastHit[]。
    void Raycast_All2()
    {       
        if (Input.GetMouseButtonDown(0))
        {           
            float distance = Mathf.Infinity;
            Ray ray2 = interactiveCamera.ScreenPointToRay(Input.mousePosition);//滑鼠位置(把滑鼠在螢幕上的位置轉換成射線)     
            prePos = interactiveCamera.ScreenToWorldPoint(new Vector3(5751.8f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray2, distance);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.transform.tag == "Card" && hit.transform.name == "Image")
                {
                    touchCard++;
                    obj = hit.transform.gameObject; //把obj變成我滑鼠(射線)點到的東西
                    if(touchCard == 1)
                    {
                        v3Org = obj.transform.position; //把obj的原始位置記下來   
                    }                                                       
                }
                Debug.Log(string.Format("Touch - Name:{0}, Position:{1}, Point:{2}", hit.transform.name, hit.transform.position, hit.point));
            }
        }       
    }

    private void StartMoving()
    {
        elapsedTime = 0f;
        isMoving = true;
    }

    private void MoveObject()
    {
        elapsedTime += Time.deltaTime;
        

        if (elapsedTime >= 0.7f)
        {           
            //Debug.Log("動畫效果開始表演");
            ani_DrawCardEffect.SetBool("drawcardeffect", true);      
        }

        if (elapsedTime >= moveDuration)// 移动完成
        {
            //transform.position = endPoint.position; //此腳本所掛的物件的位置 = 終點 (原始資料)
            obj.transform.position = new Vector3(5751.8f, 844, 100);                        
            //ani_DrawCardEffect.SetBool("drawcardeffect", true);
            isMoving = false;
            Debug.Log(isMoving);
            elapsedTime = 0f;
        }
        else
        {          
            float t = elapsedTime / moveDuration;
            obj.transform.position = Vector3.Lerp(obj.transform.position, new Vector3(5751.8f, 844, 100), t);  
            
        }
    }

    /// <summary>
    /// 動畫事件 >> 抽卡一次畫面關閉
    /// </summary>
    public void DrawCardOncePictureClose()
    {
        startDetect = true;       
    }

    private void PictureClose()
    {
        if(startDetect)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ani_DrawCardEffect.SetTrigger("nonestate");
                ani_DrawCardEffect.SetBool("drawcardeffect", false);
                DrawCardOncePicture.transform.gameObject.SetActive(false);
                interactiveCamera.depth = 0;
                startDetect = false;
                obj.transform.position = v3Org;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PictureClose();
        //Raycast_Ray2();
        Raycast_All2();
        //按著滑鼠左鍵(持續)
        if (obj != null && Input.GetMouseButton(0))
        {                                                       
            Vector3 nowPos = interactiveCamera.ScreenToWorldPoint(new Vector3(5751.8f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
            obj.transform.position += nowPos - prePos;
            prePos = interactiveCamera.ScreenToWorldPoint(new Vector3(5751.8f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
        }
        //Debug.Log("obj.transform.position : " + obj.transform.position);

        if (Input.GetMouseButtonUp(0) && !isMoving)
        {
            if(obj != null && obj.transform.position.y <= 999)
            {
                Debug.Log("obj.transform.position : " +  obj.transform.position);
                StartMoving();
            }            
        }

        if (isMoving)
        {
            MoveObject();
        }

    }
}
