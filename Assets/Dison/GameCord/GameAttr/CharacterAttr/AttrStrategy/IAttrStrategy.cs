using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色數值計算界面
/// </summary>
public abstract class IAttrStrategy 
{
    /// <summary>
    /// 初始的數值
    /// </summary>
    /// <param name="CharacterAttr"></param>
    public abstract void InitAttr(ICharacterAttr CharacterAttr);


    
}
