using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISceneState //場景類別介面，定義場景轉換及執行時需要呼叫的方法。
{
    //場景狀態
    private string m_StateName = "ISceneState"; //除錯debug使用
    public string StateName
    {
        get
        {
            return m_StateName;
        }
        set
        {
            m_StateName = value;
        }
    }

    //場景狀態控制者
    protected SceneStateController m_Controller = null;

    //建構式
    public ISceneState(SceneStateController Controller)
    {
        m_Controller = Controller;
    }

    //開始
    public virtual void StateBegin()
    {
        //只執行一次，執行場景狀態初始化用的
    }

    //結束
    public virtual void StateEnd()
    {

    }

    //更新
    public virtual void StateUpdate()
    {
        //該場景狀態的Update
    }

    public override string ToString()
    {
        return string.Format("I_SceneState : StateName = {0}", StateName);
    }
}
