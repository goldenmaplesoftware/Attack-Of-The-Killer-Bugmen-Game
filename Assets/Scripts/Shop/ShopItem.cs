using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ShopMenu:",menuName ="Scriptable Object/New Shop Item",order =1)]
public class ShopItem : ScriptableObject
{
    public string title;
    public string description;
    public int baseCost;
}
