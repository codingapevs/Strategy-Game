using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit: MonoBehaviour {

    //unit coordination
    public int coordX;
    public int coordY;
    //path of unit
    public List<TileInfo> path;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        //when path has tile to go, unity step to it
        if(path.Count>0)
        {
            TileInfo step = path[0];
            transform.position = new Vector3(step.transform.position.x, step.transform.position.y, -5);
            coordX = step.coorX;
            coordY = step.coorY;
            path.Remove(step);
        }
        

    }

   

    private void OnMouseUp()
    {
        ControllerManager.Instance.selectedUnit = gameObject;
        ControllerManager.Instance.unitMode = true;
    }
    
}
