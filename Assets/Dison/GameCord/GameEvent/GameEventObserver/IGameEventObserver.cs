using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGameEventObserver
{
    public abstract void Update(); //提供主題叫觀察者更新的方法(主題會叫每一個觀察者更新)
    public abstract void SetSubject(IGameEventSubject Subject);
}
