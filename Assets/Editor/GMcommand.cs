using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GMcommand 
{
    [MenuItem("GMCommand/刪除背包資料")]
    public static void DeletePackageData()
    {      
        BackpackInfoUI backpackInfoUI = TowerOfAdventureGame.Inst.GetBackageInfo();
        backpackInfoUI.DeleteBackpackArchive();
    }
}
