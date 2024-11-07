using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yuna : IPlayerCharacter
{
    public Yuna()
    {
        m_PlayerCharacter = PlayerCharacter.Yuna;
        m_AttrID = 5;
    }
}
