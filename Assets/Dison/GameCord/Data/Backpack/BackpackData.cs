using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackpackData 
{
    public List<CardData> list_BackpackData;
    public List<int> list_openBackpackContentButton;
    public List<GameObject> listObj_PreInstant;
    public List<int> list_cardDataIndex;
    public BackpackData()
    {
        list_BackpackData = new List<CardData>();
        list_openBackpackContentButton = new List<int>();
        listObj_PreInstant = new List<GameObject>();
        list_cardDataIndex = new List<int>();
    }
}
