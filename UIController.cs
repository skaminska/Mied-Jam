using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    
    public void OnBuildButtonClick()
    {
        GameManager.Instance.buildMenu.SetActive(true);

        GameManager.Instance.TurnOffAllModes();
        GameManager.Instance.TurnOnBuildMode();
    }

    public void OnUpgradeButton()
    {
        GameManager.Instance.upgradeMenu.SetActive(true);
    }

    public void Quit()
    {
        GameManager.Instance.upgradeMenu.SetActive(false);
        GameManager.Instance.buildMenu.SetActive(false);
    }

    public void OnDestroyButtonClick()
    {
        GameManager.Instance.TurnOffAllModes();
        GameManager.Instance.TurnOnDestroyMode();
    }
}
