using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController 
{
    private ISceneState m_State;
    private bool m_bRunBegin = false;
    private AsyncOperation asyncOperation;

    /// <summary>
    /// 設定狀態
    /// </summary>
    /// <param name="state">狀態</param>
    /// <param name="loadSceneName">場景名稱</param>
    public void SetState(ISceneState state, string loadSceneName)
    {
        m_bRunBegin = false;

        //載入場景
        if(loadSceneName != null && loadSceneName.Length != 0)
        {
            asyncOperation = SceneManager.LoadSceneAsync(loadSceneName);
        }
        
        //通知前一個狀態結束
        if(m_State != null)
        {
            m_State.StateEnd();
        }

        //設定新的狀態
        m_State = state;
    }

    public void StateUpdate()
    {       
        ////是否還在載入場景
        //if(asyncOperation != null && asyncOperation.isDone)
        //{           
        //    return;
        //}

        ////通知新的State開始
        //if(m_State != null && m_bRunBegin == false)
        //{
        //    m_State.StateBegin();
        //    m_bRunBegin = true;
        //}

        ////State更新
        //if (m_State != null)
        //{
        //    m_State.StateUpdate();
        //}


        //正在載入場景
        if (asyncOperation != null)
        {
            //場景是否載入完成
            if (asyncOperation.isDone)
            {
                //通知新的State開始
                if (m_State != null && m_bRunBegin == false)
                {
                    m_State.StateBegin();
                    m_bRunBegin = true;
                }

                //State更新
                if (m_State != null)
                {
                    m_State.StateUpdate();
                }
            }
        }
        else
        {
            //還沒載入場景
            if (m_State != null && m_bRunBegin == false)
            {
                m_State.StateBegin();
                m_bRunBegin = true;
            }

            //State更新
            if (m_State != null)
            {
                m_State.StateUpdate();
            }
        }       
    }
}
