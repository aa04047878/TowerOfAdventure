using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData 
{
    public int Lv;
    public float nowEXP;
    public float totalEXP;
    public string playerName;
    
    public PlayerData()
    {
        Lv = 1;
        nowEXP = 0;
        totalEXP = 500;
        playerName = "神魔之塔";
    }


    
    
}
