using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBackpackContentObserverTeamUI : IGameEventObserver
{
    private AddBackpackContentSubject m_Subject;
    private TeamInfoUI m_InfoUI;

    public AddBackpackContentObserverTeamUI(TeamInfoUI InfoUI)
    {
        m_InfoUI = InfoUI;
    }
    public override void SetSubject(IGameEventSubject Subject)
    {
        m_Subject = Subject as AddBackpackContentSubject;
    }

    public override void Update()
    {
        m_InfoUI.EditorTeamItemUpdate(m_Subject.ShowBackpackButtonContent(), m_Subject.GetCardDataIndex(), m_Subject.GetBehavior());
    }
}
