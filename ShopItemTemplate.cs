using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemTemplate : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI buildingName;
    [SerializeField] public TextMeshProUGUI buildingDescription;
    [SerializeField] public Image buildingImage;
    [SerializeField] public TextMeshProUGUI copperPriceText;
    [SerializeField] public TextMeshProUGUI moneyPriceText;
    [SerializeField] public TextMeshProUGUI whereINeedCopper;

    public GameObject buildingTemplate;
    public int moneyPrice, copperPrice;

    public void OnButtonClick()
    {
        GameManager.Instance.buildMode = true;
        
        if (GameManager.Instance.moneyAmount >= moneyPrice && GameManager.Instance.copperAmount >= copperPrice)
        {
            GameManager.Instance.moneyAmount -= moneyPrice;
            GameManager.Instance.copperAmount -= copperPrice;

            GameManager.Instance.tmp = Instantiate(buildingTemplate, transform.position, Quaternion.identity);

            GameManager.Instance.UpdateInfo();
            GameManager.Instance.buildMenu.SetActive(false);
        }
        else if (GameManager.Instance.moneyAmount <= moneyPrice && GameManager.Instance.copperAmount >= copperPrice)
        {
            GameManager.Instance.infoBoxText.text = "Brakuje pieniędzy";
            GameManager.Instance.ShowInfoBox();
        }
        else if (GameManager.Instance.moneyAmount >= moneyPrice && GameManager.Instance.copperAmount <= copperPrice)
        {
            GameManager.Instance.infoBoxText.text = "Brakuje miedzi";
            GameManager.Instance.ShowInfoBox();
        }
        else
        {
            GameManager.Instance.infoBoxText.text = "Brakuje pieniędzy i miedzi";
            GameManager.Instance.ShowInfoBox();
        }

    }
}
