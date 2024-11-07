using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//主題介面實作
public class AddBackpackContentSubject : IGameEventSubject
{
    private int openBackpackContentButton = 0;
    private int _cardDataIndex = 0;
    private ENUM_Behavior _behavior;
    public int ShowBackpackButtonContent()
    {
        return openBackpackContentButton;
    }

    public int GetCardDataIndex()
    {
        return _cardDataIndex;
    }


    public ENUM_Behavior GetBehavior()
    {
        return _behavior;
    }

    public override void SetParam(int cardDataIndex, ENUM_Behavior behavior)
    {
        openBackpackContentButton++;
        _cardDataIndex = cardDataIndex;
        _behavior = behavior;
        Notify();
    }
}
