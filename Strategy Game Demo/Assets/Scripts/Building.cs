using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public string name;
    public string type;
    public int dimX;
    public int dimY;
    public int coordX;
    public int coordY;
    public Sprite buildingSprite;
    public GameObject spawnUnit;

    // Use this for initialization
    void Start () {
        buildingSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
	
	void Update () {
		
	}

    private void OnMouseUp()
    {
        //show info if system not in building mode
        if(!ControllerManager.Instance.buildingMode)
        {
            UIContoller.Instance.ShowInfo(this);
            ControllerManager.Instance.selectedBuilding = gameObject;
        }
            
    }

    
}
