using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "shopItem", menuName ="NewShopItem", order =1)]
public class Building : ScriptableObject
{
    public string buidingName;
    public int buildingPrice;
    public int buildingCopperPrice;
    public GameObject building;
    public string description;
    public string whereINeedCopperHere;
    public Sprite buildingImage;
}
