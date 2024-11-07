using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLevel : MonoBehaviour
{
    public Animator ani;
    private bool enterWaterLevel01;
    private bool enterFireLevel01;
    private bool enterWoodLevel01;
    private bool enterLightLevel01;
    private bool enterDarkLevel01;
    public string s;

    /// <summary>
    /// 進入關卡動畫
    /// </summary>
    public void EnterLevelAni()
    {           
        ani.SetTrigger("enterlevel");         
    }

    /// <summary>
    /// 進入火關01動畫
    /// </summary>
    public void EnterFireLevel01Ani()
    {
        ani.SetTrigger("enterfirelevel01");
    }

    /// <summary>
    /// 進入木關01動畫
    /// </summary>
    public void EnterWoodLevel01Ani()
    {
        ani.SetTrigger("enterwoodlevel01");
    }

    /// <summary>
    /// 進入光關01動畫
    /// </summary>
    public void EnterLightLevel01Ani()
    {
        ani.SetTrigger("enterlightlevel01"); 
    }

    /// <summary>
    /// 進入暗關01動畫
    /// </summary>
    public void EnterDarkLevel01Ani()
    {
        ani.SetTrigger("enterdarklevel01");
    }

    /// <summary>
    /// 進入水關第一關(動畫事件)
    /// </summary>
    public void EnterWaterLevel1()
    {
        enterWaterLevel01 = true;
    }

    /// <summary>
    /// 進入火關第一關(動畫事件)
    /// </summary>
    public void EnterFireLevel1()
    {
        enterFireLevel01 = true;
    }

    /// <summary>
    /// 進入木關第一關(動畫事件)
    /// </summary>
    public void EnterWoodLevel1()
    {
        enterWoodLevel01 = true;
    }

    /// <summary>
    /// 進入光關第一關(動畫事件)
    /// </summary>
    public void EnterLightLevel1()
    {
        enterLightLevel01 = true;
    }

    /// <summary>
    /// 進入暗關第一關(動畫事件)
    /// </summary>
    public void EnterDarkLevel1()
    {
        enterDarkLevel01 = true;
    }

    /// <summary>
    /// 進入水關第1關
    /// </summary>
    /// <returns></returns>
    public bool GetEnterWaterLevel01()
    {
        return enterWaterLevel01;
    }

    /// <summary>
    /// 進入火關第1關
    /// </summary>
    /// <returns></returns>
    public bool GetEnterFireLevel01()
    {
        return enterFireLevel01; //要補做動畫
    }

    /// <summary>
    /// 進入木關第1關
    /// </summary>
    /// <returns></returns>
    public bool GetEnterWoodLevel01()
    {
        return enterWoodLevel01; 
    }

    /// <summary>
    /// 進入光關第1關
    /// </summary>
    /// <returns></returns>
    public bool GetEnterLightLevel01()
    {
        return enterLightLevel01;
    }

    /// <summary>
    /// 進入暗關第1關
    /// </summary>
    /// <returns></returns>
    public bool GetEnterDarkLevel01()
    {
        return enterDarkLevel01;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
