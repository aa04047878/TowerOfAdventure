using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel1State : ISceneState
{
    WaterLevel1Manager waterLevel1Manager;
    public WaterLevel1State(SceneStateController Controller) : base(Controller)
    {
        
    }

    public override void StateBegin()
    {
        waterLevel1Manager = UITool.FindGameComponent<WaterLevel1Manager>("Manager");
        waterLevel1Manager.Init();

    }

    public override void StateUpdate()
    {
        ReturnMainMenu(WaterLevel1Manager.inst.ReturnMainMenu());
    }

    /// <summary>
    /// 返回主選單
    /// </summary>
    /// <param name="b"></param>
    public void ReturnMainMenu(bool b)
    {
        if (b)
        {
            WaterLevel1Manager.inst.SetParam();
            m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
        }
    }
}
