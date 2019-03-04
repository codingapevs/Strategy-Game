using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    private static BuildingManager _instance;
    public static BuildingManager Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Instantiate new building and set it to controller manager
    public void CreateBuilding(string buildingType)
    {
        GameObject building = Instantiate(Resources.Load(buildingType, typeof(GameObject)), new Vector3(0,0,0), Quaternion.identity, gameObject.transform) as GameObject;
        ControllerManager.Instance.target = building;
        ControllerManager.Instance.buildingMode = true;
        ControllerManager.Instance.unitMode = false;
    }
    //spawn unit in front of building
    public void SpawnUnit()
    {
        
        Building building = ControllerManager.Instance.selectedBuilding.GetComponent<Building>();
        int checkPosX = building.coordX;
        int checkPosY = building.coordY-4;
        while(true)
        {
            if (ControllerManager.Instance.CheckGrid(checkPosX, checkPosY, 1, 1,false))
            {
                GameObject unit = Instantiate(Resources.Load("Soldier", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform) as GameObject;
                unit.transform.position = GridSystem.Instance.gridParts[checkPosX, checkPosY].transform.position;
                unit.transform.Translate(new Vector3(0, 0, -5));
                GridSystem.Instance.gridParts[checkPosX, checkPosY].isOccupied = true;
                Unit unitS = unit.GetComponent<Unit>();
                unitS.coordX = checkPosX;
                unitS.coordY = checkPosY;
                break;
            }
            else
            {
                checkPosX--;
                if(checkPosX< building.coordX - 5)
                {
                    checkPosX = building.coordX;
                    checkPosY--;
                }
            }
            
        }
        

    }
}
