using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {

    public int coorX;
    public int coorY;
    public bool isOccupied = false;
    public TileInfo parent;
    public int gCost;
    public int hCost;

    private void OnMouseUp()
    {
        
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
