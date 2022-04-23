using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineAndSteelworks : WorkPlace
{
    [SerializeField] TextMeshProUGUI currentWorkersText, maxWorkersText, nextMaxWorkersText, copperPriceText, moneyPriceText;

    public int copperUpgradeCost;
    public int moneyUpgradeCost;
    private void Start()
    {
        maxWorkers = 15;
        copperUpgradeCost = 300;
        moneyUpgradeCost = 500;
        GameManager.Instance.workPlace = this;
        maxWorkersText.text = maxWorkers.ToString();
        currentWorkersText.text = currentWorkers.ToString();
        moneyPriceText.text = moneyUpgradeCost.ToString();
        copperPriceText.text = copperUpgradeCost.ToString();
        maxWorkersText.text = maxWorkers.ToString();
        nextMaxWorkersText.text = (maxWorkers + 15).ToString();
    }

    public void CalculateCopperIncrese()
    {
        currentWorkersText.text = currentWorkers.ToString();
        powerNeeded += 2*currentWorkers;
        GameManager.Instance.copperDailyIncrese = currentWorkers * 50;
    }

    public void OnMineUpgradeButtonClick()
    {
        if(moneyUpgradeCost < GameManager.Instance.moneyAmount && copperUpgradeCost < GameManager.Instance.copperAmount)
        {
            maxWorkers += 15;
            GameManager.Instance.moneyAmount -= moneyUpgradeCost;
            GameManager.Instance.copperAmount -= copperUpgradeCost;
            CalculateCopperIncrese();
            moneyUpgradeCost *= 2;
            copperUpgradeCost *= 2;
            moneyPriceText.text = moneyUpgradeCost.ToString();
            copperPriceText.text = copperUpgradeCost.ToString();
            maxWorkersText.text = maxWorkers.ToString();
            nextMaxWorkersText.text = (maxWorkers + 15).ToString();

            
        }
        else if (GameManager.Instance.moneyAmount <= moneyUpgradeCost && GameManager.Instance.copperAmount >= copperUpgradeCost)
        {
            GameManager.Instance.infoBoxText.text = "Brakuje pieniędzy";
            GameManager.Instance.ShowInfoBox();
        }
        else if (GameManager.Instance.moneyAmount >= moneyUpgradeCost && GameManager.Instance.copperAmount <= copperUpgradeCost)
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
