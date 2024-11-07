using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrawCard : MonoBehaviour
{
    public GameObject obj;
    public Vector3 prePos;
    public int touchCard;
    public Vector3 v3Org;
    public Vector3 endPoint;
    public Vector3 textPos;
    public GameObject obj_Test;
    public bool drawcard;
    public bool startDetect;

    #region Lerp
    public float moveDuration;
    public float elapsedTime;
    private bool isMoving = false;
    #endregion

    #region 動畫
    public Animator ani_DrawCard;
    #endregion

    #region 抽卡相關音效
    public AudioSource audioSource;
    public List<AudioClip> list_DrawCardBGM;
    public bool playDrawCardAudio;
    #endregion

    void Raycast_All2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float distance = Mathf.Infinity;
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);//滑鼠位置(把滑鼠在螢幕上的位置轉換成射線)     
            prePos = Camera.main.ScreenToWorldPoint(new Vector3(0f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray2, distance);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.transform.tag == "Card" && hit.transform.name == "Card_father")
                {
                    touchCard++;
                    obj = hit.transform.gameObject; //把obj變成我滑鼠(射線)點到的東西
                    if (touchCard == 1)
                    {
                        v3Org = obj.transform.position; //把obj的原始位置記下來   
                    }
                    Debug.Log($"hit.transform.name : {hit.transform.name}");
                }
                //Debug.Log(string.Format("Touch - Name:{0}, Position:{1}, Point:{2}", hit.transform.name, hit.transform.position, hit.point));
                
            }
        }
    }

    private void MoveObject()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= moveDuration)// 移动完成
        {
            //transform.position = endPoint.position; //此腳本所掛的物件的位置 = 終點 (原始資料)
            //obj.transform.position = new Vector3(0f, 0, 90);  
            obj.transform.localPosition = new Vector3(0f, -2000, 0);
            isMoving = false;
            Debug.Log(isMoving);
            elapsedTime = 0f;
            Debug.Log("移動完成");
            drawcard = true;
            ani_DrawCard.SetTrigger("drawcardeffectopen");
        }
        else
        {
            float t = elapsedTime / moveDuration;
            //obj.transform.position = Vector3.Lerp(obj.transform.position, new Vector3(0f, -40, 90), t);
            obj.transform.localPosition = Vector3.Lerp(obj.transform.position, new Vector3(0f, -2000, 0), t);

        }
        
    }

    private void StartMoving()
    {
        elapsedTime = 0f;
        isMoving = true;
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
        if (startDetect)
        {
            if (Input.GetMouseButtonDown(0))
            {
                obj.transform.localPosition = new Vector3(0f, 920, 0);
                ani_DrawCard.SetTrigger("nonestate");
                startDetect = false;
                obj = null;
                playDrawCardAudio = false;
                Stop();
                GameLoop.inst.Play();
                TowerOfAdventureGame.Inst.DrawCardPictureClose();
            }
        }
    }

    /// <summary>
    /// 主選單音樂暫停(動畫事件)
    /// </summary>
    public void MainMenuAudioPause()
    {
        GameLoop.inst.Pause();
    }

    /// <summary>
    /// 播放準備抽卡音效(動畫事件)
    /// </summary>
    public void PlayReadyDrawCardAudio()
    {
        audioSource.clip = list_DrawCardBGM[2];
        audioSource.Play();
    }

    /// <summary>
    /// 播放卡音效(動畫事件)
    /// </summary>
    public void PlayCardAudio()
    {
        audioSource.clip = list_DrawCardBGM[0];
        audioSource.Play();
    }

    /// <summary>
    /// 播放顯示卡片音效(動畫事件)
    /// </summary>
    public void PlayShowCardAudio()
    {
        audioSource.clip = list_DrawCardBGM[3];
        audioSource.Play();
    }

    /// <summary>
    /// 抽卡音效停止
    /// </summary>
    public void Stop()
    {
        if (audioSource.clip != null)
            audioSource.Stop();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PictureClose(); 
        Raycast_All2();
        //按著滑鼠左鍵(持續)
        if (obj != null && Input.GetMouseButton(0))
        {
            Vector3 nowPos = Camera.main.ScreenToWorldPoint(new Vector3(0f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
            obj.transform.position += nowPos - prePos;
            prePos = Camera.main.ScreenToWorldPoint(new Vector3(0f, Input.mousePosition.y, 100));//把滑鼠在螢幕上的位置轉換成世界座標後記起來
            if (obj.transform.localPosition.y < 920 && !playDrawCardAudio)
            {
                playDrawCardAudio = true;
                audioSource.clip = list_DrawCardBGM[1];
                audioSource.Play();
            }
        }

        if (Input.GetMouseButtonUp(0) && !isMoving)
        {
            if (obj != null && obj.transform.localPosition.y <= 640)
            {
                StartMoving();
            }
            //obj = null;

        }

        if (isMoving)
        {
            MoveObject();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"位置 : {obj_Test.transform.position}");
        }
        
    }
}
