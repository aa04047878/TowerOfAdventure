using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : ISceneState
{
    public MainMenuState(SceneStateController m_Controller):base(m_Controller)
    {
        StateName = "MainMenuState";
    }

    public override void StateBegin()
    {
        
        TowerOfAdventureGame.Inst.Initinal();
        Debug.Log("MainMenuState : StateBegin");
    }

    public override void StateUpdate()
    {
        TowerOfAdventureGame.Inst.Update();
        EnterWaterLevel1(TowerOfAdventureGame.Inst.GetEnterWaterLevel1());
        EnterFireLevel1(TowerOfAdventureGame.Inst.GetEnterFireLevel1());
        EnterWoodLevel1(TowerOfAdventureGame.Inst.GetEnterWoodLevel1());
        EnterLightLevel1(TowerOfAdventureGame.Inst.GetEnterLightLevel1());
        EnterDarkLevel1(TowerOfAdventureGame.Inst.GetEnterDarkLevel1());
    }

    /// <summary>
    /// 進入水關第1關
    /// </summary>
    public void EnterWaterLevel1(bool b)
    {
        if (b)
        {
            //TowerOfAdventureGame.Inst.SetParam();
            m_Controller.SetState(new WaterLevel1State(m_Controller), "WaterLevel1");

        }
        
    }

    /// <summary>
    /// 進入火關第1關
    /// </summary>
    /// <param name="b"></param>
    public void EnterFireLevel1(bool b)
    {
        if (b)
        {
            //TowerOfAdventureGame.Inst.SetParamFireLevel();
            m_Controller.SetState(new FireLevel1State(m_Controller), "FireLevel1");
        }
    }

    /// <summary>
    /// 進入木關第1關
    /// </summary>
    /// <param name="b"></param>
    public void EnterWoodLevel1(bool b)
    {
        if (b)
        {
            //TowerOfAdventureGame.Inst.SetParamFireLevel();
            m_Controller.SetState(new WoodLevel1State(m_Controller), "WoodLevel1");
        }
    }

    /// <summary>
    /// 進入光關第1關
    /// </summary>
    /// <param name="b"></param>
    public void EnterLightLevel1(bool b)
    {
        if (b)
        {
            //TowerOfAdventureGame.Inst.SetParamFireLevel();
            m_Controller.SetState(new LightLevel1State(m_Controller), "LightLevel1");
        }
    }

    /// <summary>
    /// 進入暗關第1關
    /// </summary>
    /// <param name="b"></param>
    public void EnterDarkLevel1(bool b)
    {
        if (b)
        {
            //TowerOfAdventureGame.Inst.SetParamFireLevel();
            m_Controller.SetState(new DarkLevel1State(m_Controller), "DarkLevel1");
        }
    }
}
