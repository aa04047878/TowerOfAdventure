using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInterface 
{
    protected TowerOfAdventureGame m_TOAGame = null;

    public IUserInterface(TowerOfAdventureGame TOAGame)
    {
        m_TOAGame = TOAGame;
    }

    public virtual void Initialize() { }

    public virtual void Release() { }

    public virtual void Update() { }
}
