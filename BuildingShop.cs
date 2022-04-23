using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingShop : MonoBehaviour
{
    [SerializeField] List<Building> buildingToBuy;
    [SerializeField] List<ShopItemTemplate> shopTemplate;

    private void Start()
    {
        LoadPanels();
    }

    void LoadPanels()
    {
        for(int i =0; i<buildingToBuy.Count; i++)
        {
            shopTemplate[i].buildingName.text = buildingToBuy[i].buidingName;
            shopTemplate[i].buildingDescription.text = buildingToBuy[i].description.ToString();
            shopTemplate[i].buildingImage.sprite = buildingToBuy[i].building.GetComponent<TemplateScript>().finalObject.GetComponent<SpriteRenderer>().sprite;
            shopTemplate[i].copperPrice = buildingToBuy[i].buildingCopperPrice;
            shopTemplate[i].copperPriceText.text = buildingToBuy[i].buildingCopperPrice.ToString();
            shopTemplate[i].moneyPrice = buildingToBuy[i].buildingPrice;
            shopTemplate[i].moneyPriceText.text = buildingToBuy[i].buildingPrice.ToString();
            shopTemplate[i].whereINeedCopper.text = buildingToBuy[i].whereINeedCopperHere.ToString();
            shopTemplate[i].buildingTemplate = buildingToBuy[i].building;
        }
    }
}
