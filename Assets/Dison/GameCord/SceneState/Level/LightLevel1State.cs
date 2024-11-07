using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLevel1State : ISceneState
{
    public LightLevel1State(SceneStateController Controller) : base(Controller)
    {

    }

    public override void StateBegin()
    {
        //fire = UITool.FindGameComponent<WaterLevel1Manager>("Manager");
        LightLevel1Manager.inst.Init();

    }

    public override void StateUpdate()
    {
        ReturnMainMenu(LightLevel1Manager.inst.ReturnMainMenu());
    }

    /// <summary>
    /// 返回主選單
    /// </summary>
    /// <param name="b"></param>
    public void ReturnMainMenu(bool b)
    {
        if (b)
        {
            LightLevel1Manager.inst.SetParam();
            m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
        }
    }
}
