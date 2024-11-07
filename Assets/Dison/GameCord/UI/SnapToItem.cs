using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SnapToItem : EventTrigger
{
    /*
    注意事項 :
    1. 繼承EventTrigger之前，需要先using UnityEngine.EventSystems;
    2. 此腳本因為有繼承EventTrigger，所以把腳本掛在物件上的時候，public宣告的內容都不會顯示，需要顯示的話要把 EventTrigger 改回 MonoBehaviour。
    3. 此腳本程式碼來自這段影片 >> https://www.youtube.com/watch?v=UjWH5VYMmDs&t=2s
    */
    public ScrollRect scrollRect;
    public RectTransform contantPanel;
    public RectTransform sampleListItem;
    public HorizontalLayoutGroup HLG;

    bool isSnapped;
    float snapSpeed;
    public float snapForce;
    int currentItem;


    public int GetcurrentItem()
    {
        return currentItem;
    }

    // Start is called before the first frame update
    void Start()
    {
        isSnapped = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region 物件離中心點的距離
        //Mathf.RoundToInt(值) >> 把你代入的值四捨五入至整數
        currentItem = Mathf.RoundToInt(0 - contantPanel.localPosition.x / (sampleListItem.rect.width + HLG.spacing));
        //移動距離佔了子物件(1個)的寬+間距多少比例    (用0去減的用意是負負得正)

        //Debug.Log("currentItem : " + currentItem);

        //Debug.Log("contantPanel.localPosition.x  : " + contantPanel.localPosition.x);

        //Debug.Log("contantPanel.localPosition  : " + contantPanel.localPosition);

        //Debug.Log("sampleListItem.rect.width (contant子物件的width)  : " + sampleListItem.rect.width); 
        #endregion

        #region 移動ScrollRect後把物件定位在中間(Normal版本 : 比較難拖拉)
        //if (scrollRect.velocity.magnitude < 70) 
        //{
        //    //scrollRect.velocity.magnitude >> 此值代表拖拉scrollRect後，scrollRect的速度(會遞減)，當此速度的值到一定程度就定位在中間
        //    contantPanel.localPosition = new Vector3(0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)), contantPanel.localPosition.y, contantPanel.localPosition.z);
        //}
        #endregion

        #region 移動ScrollRect後把物件定位在中間(Advanced版本 : 比較好拖拉)
        //if (scrollRect.velocity.magnitude < 200 && !isSnapped)
        //{
        //    //scrollRect.velocity.magnitude >> 此值代表拖拉scrollRect後，scrollRect的速度(會遞減)，當此速度的值到一定程度就定位在中間
        //    scrollRect.velocity = Vector2.zero;
        //    snapSpeed += snapForce * Time.deltaTime;
        //    contantPanel.localPosition = new Vector3(
        //        Mathf.MoveTowards(contantPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)), snapSpeed),
        //        contantPanel.localPosition.y,
        //        contantPanel.localPosition.z);

        //    if (contantPanel.localPosition.x == 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)))
        //    {
        //        Debug.Log("移動後定位在中間完成");
        //        isSnapped = true;
        //    }
        //}

        //if (scrollRect.velocity.magnitude > 200)
        //{
        //    Debug.Log("正在移動中");
        //    isSnapped = false;
        //    snapSpeed = 0;
        //}
        #endregion

        #region 移動ScrollRect後把物件定位在中間(Extra版本 : 移動超過第一項/最後一項的時候，就會馬上回到第一項/最後一項，手遊UI必備技能)

        //Debug.Log("scrollRect.velocity.magnitude : " + scrollRect.velocity.magnitude);
        if (contantPanel.localPosition.x <= 0 - (5 * (sampleListItem.rect.width + HLG.spacing)) && !isSnapped) //先判斷最後一個物件是否已經在中間了  //5代表contant裡的最大的index
        {
            if (scrollRect.velocity.magnitude < 200000)
            {
                //不管怎麼拉scrollRect，scrollRect.velocity.magnitude的值都不可能有20萬的值，這個20萬也只不過為了要讓最後一項馬上回到中心點的判斷而已。
                scrollRect.velocity = Vector2.zero;
                snapSpeed += snapForce * Time.deltaTime;
                contantPanel.localPosition = new Vector3(
                    Mathf.MoveTowards(contantPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)), snapSpeed),
                    contantPanel.localPosition.y,
                    contantPanel.localPosition.z);

                if (contantPanel.localPosition.x == 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)))
                {
                    contantPanel.localPosition = new Vector3(0 - (5 * (sampleListItem.rect.width + HLG.spacing)), 0, 0);
                    Debug.Log("移動後定位在中間完成");
                    isSnapped = true;
                }
            }
        }
        else if (contantPanel.localPosition.x >= 0 && !isSnapped)
        {
            if (scrollRect.velocity.magnitude < 200000)
            {
                //不管怎麼拉scrollRect，scrollRect.velocity.magnitude的值都不可能有20萬的值，這個20萬也只不過為了要讓第一項馬上回到中心點的判斷而已。
                scrollRect.velocity = Vector2.zero;
                snapSpeed += snapForce * Time.deltaTime;
                contantPanel.localPosition = new Vector3(
                    Mathf.MoveTowards(contantPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)), snapSpeed),
                    contantPanel.localPosition.y,
                    contantPanel.localPosition.z);

                if (contantPanel.localPosition.x == 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)))
                {
                    contantPanel.localPosition = new Vector3(0, 0, 0);
                    Debug.Log("移動後定位在中間完成");
                    isSnapped = true;
                }
            }
        }
        else
        {
            if (scrollRect.velocity.magnitude < 500 && !isSnapped)
            {
                //scrollRect.velocity.magnitude >> 此值代表拖拉scrollRect後，scrollRect的速度(會遞減)，當此速度的值到一定程度就定位在中間
                scrollRect.velocity = Vector2.zero;
                snapSpeed += snapForce * Time.deltaTime;
                contantPanel.localPosition = new Vector3(
                    Mathf.MoveTowards(contantPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)), snapSpeed),
                    contantPanel.localPosition.y,
                    contantPanel.localPosition.z);

                if (contantPanel.localPosition.x == 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)))
                {
                    Debug.Log("移動後定位在中間完成");
                    isSnapped = true;
                }
            }
        }



        if (scrollRect.velocity.magnitude > 100) //這種設定方式拉快沒問題，拉慢會有點問題
        {
            Debug.Log("正在移動中");

        }

        #endregion
    }

    public override void OnEndDrag(PointerEventData data)
    {
        Debug.Log("OnEndDrag called. - 結束拖曳");
        isSnapped = false;
        snapSpeed = 0;
    }
}
