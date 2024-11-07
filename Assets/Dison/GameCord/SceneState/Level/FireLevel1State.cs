using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLevel1State : ISceneState
{
    public FireLevel1State(SceneStateController Controller) : base(Controller)
    {

    }

    public override void StateBegin()
    {
        //fire = UITool.FindGameComponent<WaterLevel1Manager>("Manager");
        FireLevel1Manager.inst.Init();

    }

    public override void StateUpdate()
    {
        ReturnMainMenu(FireLevel1Manager.inst.ReturnMainMenu());
    }

    /// <summary>
    /// 返回主選單
    /// </summary>
    /// <param name="b"></param>
    public void ReturnMainMenu(bool b)
    {
        if (b)
        {
            FireLevel1Manager.inst.SetParam();
            m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
        }
    }
}
