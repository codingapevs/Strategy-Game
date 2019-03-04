using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerManager : MonoBehaviour {

    private static ControllerManager _instance;
    public static ControllerManager Instance { get { return _instance; } }
    public GameObject target;
    public bool buildingMode;
    public bool unitMode;
    public GameObject selectedUnit;
    public GameObject selectedBuilding;


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

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (buildingMode)
        {
            //raycast to determine grid coordination
            int layerMask = 1 << LayerMask.NameToLayer("Building");
            layerMask = ~layerMask;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, -Mathf.Infinity, layerMask);
            if (target != null)
            {
                if (hit.collider != null)
                {
                    
                    target.transform.position = hit.collider.gameObject.transform.position;
                    target.transform.Translate(new Vector3(0, 0, -5));
                }
            }
            //place building on grid
            if (Input.GetMouseButtonDown(0))
            {
                
                TileInfo tileInfo = hit.collider.gameObject.GetComponent<TileInfo>();
                int coordX = tileInfo.coorX;
                int coordY = tileInfo.coorY;
                Building building = target.GetComponent<Building>();
                if(CheckGrid(coordX,coordY, building.dimX, building.dimY,true))
                {
                    for (int x = coordX; x < coordX + building.dimX; x++)
                    {
                        for (int y = coordY; y < coordY + building.dimY; y++)
                        {
                            
                            GridSystem.Instance.gridParts[x, y].isOccupied = true;
                            building.coordX = x;
                            building.coordY = y;
                            building.gameObject.GetComponent<Collider2D>().enabled = true;
                        }
                    }
                    buildingMode = false;
                    target = null;
                }
                
            }
        }
        //select grid tile to move
        if(unitMode)
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                int layerMask = 1 << LayerMask.NameToLayer("Building");
                layerMask = ~layerMask;
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, -Mathf.Infinity, layerMask);
                if (Input.GetMouseButtonDown(1))
                {
                    if (hit.collider != null && hit.transform.tag == "Tile")
                    {

                        TileInfo endTile = hit.collider.gameObject.GetComponent<TileInfo>();
                        GridSystem.Instance.FindPath(selectedUnit.GetComponent<Unit>(), endTile);
                        
                    }
                }
            }
            
        }
        
    }
    //check coordination for ant occupied tile
    public bool CheckGrid(int coordX,int coordY,int buildDimX,int buildDimY,bool alert)
    {
        for (int x = coordX; x < coordX + buildDimX; x++)
        {
            for (int y = coordY; y < coordY + buildDimY; y++)
            {
                bool occupied = GridSystem.Instance.gridParts[x, y].isOccupied;
                if(occupied)
                {
                    if(alert)
                    {
                        UIContoller.Instance.ShowAlertBox();
                    }
                    
                    return false;
                }

            }
        }
        return true;
    }
}
