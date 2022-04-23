using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "citizen", menuName = "citizen", order = 2)]
public class Citizen: ScriptableObject
{
    public WorkPlace citizenWorkPlace;
    public ResidentalBuilding citizenHouse;
    int happines;

    public void init(WorkPlace workPlace, ResidentalBuilding house)
    {
        citizenWorkPlace = workPlace;
        citizenHouse = house;
    }

    public void AddCitizenWorkPlace(WorkPlace workPlace)
    {
        citizenWorkPlace = workPlace;
        happines = 1;
    }

}
