using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLevel1State : ISceneState
{
    public WoodLevel1State(SceneStateController Controller) : base(Controller)
    {

    }

    public override void StateBegin()
    {
        //fire = UITool.FindGameComponent<WaterLevel1Manager>("Manager");
        WoodLevel1Manager.inst.Init();

    }

    public override void StateUpdate()
    {
        ReturnMainMenu(WoodLevel1Manager.inst.ReturnMainMenu());
    }

    /// <summary>
    /// 返回主選單
    /// </summary>
    /// <param name="b"></param>
    public void ReturnMainMenu(bool b)
    {
        if (b)
        {
            WoodLevel1Manager.inst.SetParam();
            m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
        }
    }
}
