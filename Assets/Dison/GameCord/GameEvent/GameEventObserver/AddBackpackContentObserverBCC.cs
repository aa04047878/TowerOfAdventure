using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBackpackContentObserverBCC : IGameEventObserver
{
    private AddBackpackContentSubject m_Subject;
    private BackpackCardContent m_BCC;
    public AddBackpackContentObserverBCC(BackpackCardContent BCC) : base()
    {
        m_BCC = BCC;
    }

    public override void SetSubject(IGameEventSubject Subject)
    {
        m_Subject = Subject as AddBackpackContentSubject;
    }

    public override void Update()
    {
        m_BCC.InstantiateCard(m_Subject.ShowBackpackButtonContent(), m_Subject.GetCardDataIndex());
        m_BCC.AddBackpackContent(m_Subject.GetCardDataIndex(), m_Subject.GetBehavior());
    }
}
