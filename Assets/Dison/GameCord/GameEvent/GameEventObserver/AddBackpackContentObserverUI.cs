using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBackpackContentObserverUI : IGameEventObserver
{
    private AddBackpackContentSubject m_Subject;   
    private BackpackInfoUI m_InfoUI = null;
    public AddBackpackContentObserverUI(BackpackInfoUI InfoUI) :base()
    {
        m_InfoUI = InfoUI;
    }

    public override void SetSubject(IGameEventSubject Subject)
    {
        m_Subject = Subject as AddBackpackContentSubject;
    }

    public override void Update()
    {
        Debug.Log("增加背包內容");
        m_InfoUI.AddBackpackContentButton(m_Subject.ShowBackpackButtonContent());
        m_InfoUI.AddBackpackItem(m_Subject.ShowBackpackButtonContent(), m_Subject.GetCardDataIndex(), m_Subject.GetBehavior());
    }
}
