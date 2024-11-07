using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : ISceneState
{
    float time = 0;
    public StartState(SceneStateController Controller):base(Controller)
    {
        StateName = "StartState";
    }

    public override void StateBegin()
    {
       
        Debug.Log("StateBegin");
        Button tmpBtn = UITool.GetUIComponent<Button>("StartGameButton");
        Image tmpImage = UITool.GetUIComponent<Image>("LoginBG");
        Animator ani = tmpImage.GetComponent<Animator>();
        tmpBtn.onClick.AddListener(delegate () {
            tmpBtn.transform.SetAsFirstSibling();
            ani.SetBool("Login", true);          
            //OnStartGameBtnClick();
        });
    }

    public override void StateUpdate()
    {
        Image tmpImage = UITool.GetUIComponent<Image>("LoginBG");
        Animator ani = tmpImage.GetComponent<Animator>();
        
        time += Time.deltaTime;
        if (ani.GetBool("Login"))
        {            
            Debug.Log("time :" + time);
            if (time >= 3f)
            {
                time = 0;
                OnStartGameBtnClick();
                //ani.SetBool("Login", false);
            }
        }
    }


    public void OnStartGameBtnClick()
    {
        m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene");
    }
}
