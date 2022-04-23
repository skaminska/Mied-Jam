using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBuilding : WorkPlace
{
    [SerializeField] public int powerProduction;

    void Start()
    {        
        GameManager.Instance.powerProduced += powerProduction;
        GameManager.Instance.UpdateInfo();
    }

}
