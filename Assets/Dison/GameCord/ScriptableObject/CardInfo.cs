using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardData
{
    public string CharacterName;
    public Sprite Attr;
    public int Space;
    public Sprite Character;   
    public int CharacterLevel;  
    public int HP;
    public int Recover;
    public int ATK;
    public string Race; 
    public int ActiveCD;
    public int ActiveLV;
    public string Active;
    public string Leader;
    public Sprite CharacterAvatar;
    public PlayerCharacter playerCharacter;
    /// <summary>
    /// 元素類型
    /// </summary>
    public ElementType elementType;
}

[CreateAssetMenu(fileName = "CardInfo", menuName = "CreateCard/Create Card Info", order = 0)]
public class CardInfo : ScriptableObject
{
    public List<CardData> cardData;
}
