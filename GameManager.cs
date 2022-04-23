using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public List<Vector2> buildingsPositions;
    public bool destroyMode, buildMode;

    public GameObject tmp;

    [SerializeField] public GameObject upgradeMenu;
    [SerializeField] public GameObject buildMenu;
    public TextMeshProUGUI infoBoxText;

    [SerializeField] public WorkPlace workPlace;
    [SerializeField] public List<ResidentalBuilding> residentalBuildings;
    
    [SerializeField] Slider timeToNextDay;
    //TODO obliczanie przyrostu miezkanców po zadowoleniu <- DO POPRAWY
    
    [SerializeField] public List<Citizen> citizens;
    public int copperAmount, moneyAmount, cityPopulation, powerNeeded, powerProduced;
    public int maxCityPopulation;

    public int populationDailyIncrese;
    public int copperDailyIncrese, moneyDailyIncrese;

    [SerializeField] MineAndSteelworks mineAndSteelworks;

    [SerializeField] TextMeshProUGUI moneyInfo, populationInfo, copperInfo, powerInfo;
    [SerializeField] List<GameObject> templates;
    [SerializeField] GameObject road;
    

    void Start()
    {
        buildingsPositions = new List<Vector2>();
        destroyMode = false;
        buildMode = false;
            
        copperAmount = 2500;
        moneyAmount = 2500;
        cityPopulation = 0;
        maxCityPopulation = 0;

        UpdateInfo();
        populationDailyIncrese = 1;
        moneyDailyIncrese = cityPopulation * 30;

        for(int i = -21; i<23; i++)
        {
            Vector2 place = new Vector2(i, -4);
            GameObject obj = Instantiate(road, place, Quaternion.identity);
            buildingsPositions.Add(place);
        }

        infoBoxText.text = "Wybuduj dom i źródło energii elektrycznej dla mieszkańców";
        infoBoxText.gameObject.SetActive(true);
        ShowInfoboxOnStart();

        upgradeMenu.SetActive(false);
        
    }

    void ShowInfoboxOnStart()
    {
        StartCoroutine( startCorutine());
    }
    IEnumerator startCorutine()
    {
        infoBoxText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        infoBoxText.text = "Ulepsz kopalnie";
        yield return new WaitForSeconds(3);
        infoBoxText.gameObject.SetActive(false);
    }
    public void ShowInfoBox()
    {
        StartCoroutine(InfoBoxCorutine());
    }

    IEnumerator InfoBoxCorutine()
    {
        infoBoxText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        infoBoxText.gameObject.SetActive(false);
    }

    void Update()
    {
        timeToNextDay.value += Time.deltaTime;
        if(timeToNextDay.value == timeToNextDay.maxValue)
        {
            DailyResourcesIncrese();
            timeToNextDay.value = 0;
        }

        if (destroyMode)
        {
            if (Input.GetMouseButtonDown(1))
            {
                infoBoxText.gameObject.SetActive(false);
                destroyMode = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D rayHit = Physics2D.Raycast(GetMousePosition(), Vector2.zero, Mathf.Infinity);

                if(rayHit.collider != null)
                {
                    if(rayHit.collider.gameObject.tag == "ResidentialBuilding")
                    {
                        maxCityPopulation -= rayHit.collider.gameObject.GetComponent<ResidentalBuilding>().maxInhabitantsNumber;
                        UpdateInfo();
                    }
                    else if (rayHit.collider.gameObject.tag == "PowerBuilding")
                    {
                        powerProduced -= rayHit.collider.gameObject.GetComponent<PowerBuilding>().powerProduction;
                        buildingsPositions.Remove(rayHit.collider.transform.position + new Vector3(1, 0));
                        buildingsPositions.Remove(rayHit.collider.transform.position + new Vector3(1, 1));
                        buildingsPositions.Remove(rayHit.collider.transform.position + new Vector3(0, 1));
                        UpdateInfo();
                    }
                    buildingsPositions.Remove(rayHit.collider.transform.position);
                    Destroy(rayHit.collider.gameObject);
                }
            }
        }
        if (buildMode)
        { 
            if (Input.GetMouseButtonDown(1))
            {
                buildMenu.SetActive(false);
                Destroy(tmp);
                buildMode = false;
            }
        }
    }

    public Vector2 GetMousePosition()
    {
       Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       return new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
    }

    public void TurnOnBuildMode()
    {
        buildMode = true;
    }

    public void TurnOnDestroyMode()
    {
        destroyMode = true;
        infoBoxText.text = "Tryb burzenia. Naciśninj prawy przycisk myszy aby wyjść";
        infoBoxText.gameObject.SetActive(true);
    }

    public void TurnOffAllModes()
    {
        destroyMode = false;
        Destroy(tmp);
        buildMode = false;
    }

    void DailyResourcesIncrese()
    {
        
        if(workPlace.currentWorkers != 0)
        {
            populationDailyIncrese = (int)(((workPlace.maxWorkers / workPlace.currentWorkers) * (powerProduced/  powerNeeded)) + 0.5f);
            Debug.Log(powerProduced + " " + powerNeeded) ;
        }
        else
            populationDailyIncrese = 5;


        if(cityPopulation < maxCityPopulation)
        {
            if (populationDailyIncrese + citizens.Count <= maxCityPopulation)
            {
                for (int i = 0; i < populationDailyIncrese; i++)
                {
                    ResidentalBuilding tmpHouse = null;
                    foreach (var place in residentalBuildings)
                    {
                        if (place.currentInhabitants < place.maxInhabitantsNumber)
                        {
                            tmpHouse = place;
                            place.currentInhabitants++;
                            break;
                        }
                    }
                
                    var newCitizen = ScriptableObject.CreateInstance("Citizen") as Citizen;
                    newCitizen.init(workPlace, tmpHouse);
                    citizens.Add(newCitizen);
                    Debug.Log(i + " " + newCitizen.citizenWorkPlace.name + " " + newCitizen.citizenHouse.name);
                }
            }
            
            else
            {
                populationDailyIncrese = maxCityPopulation - citizens.Count;
                for (int i = 0; i < populationDailyIncrese; i++)
                {
                    ResidentalBuilding tmpHouse = null;
                   
                    foreach (var place in residentalBuildings)
                    {
                        if (place.currentInhabitants < place.maxInhabitantsNumber)
                        {
                            tmpHouse = place;
                            place.currentInhabitants++;
                            break;
                        }
                    }

                    var newCitizen = ScriptableObject.CreateInstance("Citizen") as Citizen;
                    newCitizen.init(workPlace, tmpHouse);
                    citizens.Add(newCitizen);

                    Debug.Log(i + " " + newCitizen.citizenWorkPlace.name + " " + newCitizen.citizenHouse.name);
                }
            }

            workPlace.currentWorkers = 0;
            foreach(var citizen in citizens)
            {
                if (citizen.citizenWorkPlace == workPlace)
                    workPlace.currentWorkers++;
            }
            if (cityPopulation > maxCityPopulation)
                cityPopulation = maxCityPopulation;
            if (cityPopulation < 0)
                cityPopulation = 0;
        }
        mineAndSteelworks.CalculateCopperIncrese();
        copperAmount += copperDailyIncrese;
        moneyAmount += citizens.Count*20;

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        moneyInfo.text = moneyAmount.ToString();
        copperInfo.text = copperAmount.ToString();
        if (citizens != null)
            populationInfo.text = citizens.Count.ToString() + "/" + maxCityPopulation;
        else
            populationInfo.text = "0/" + maxCityPopulation;
        powerNeeded = 0;
        foreach(var building in residentalBuildings)
        {
            powerNeeded += (int)building.CountEnergyNeeded();
        }

        powerNeeded += (int)workPlace.powerNeeded;

        powerInfo.text = powerNeeded.ToString() + "/" + powerProduced;
    }
}
