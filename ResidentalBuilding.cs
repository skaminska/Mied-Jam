using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentalBuilding : MonoBehaviour
{
    [SerializeField] public int maxInhabitantsNumber;
    [SerializeField] public float energyUserPerInhabitant;

    public int currentInhabitants;
    float powerNeed;
    void Start()
    {
        GameManager.Instance.maxCityPopulation += maxInhabitantsNumber;
        GameManager.Instance.residentalBuildings.Add(this);
        GameManager.Instance.UpdateInfo();
    }

    public float CountEnergyNeeded()
    {
        powerNeed = currentInhabitants * energyUserPerInhabitant;
        return powerNeed;
    }
}
