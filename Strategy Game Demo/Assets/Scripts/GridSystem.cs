using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {

    public GameObject tilePrefab;
    public Vector2 spriteSize;

    public TileInfo[,] gridParts;
    public int gridSizeX;
    public int gridSizeY;
    private static GridSystem _instance;
    public static GridSystem Instance { get { return _instance; } }

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


    void Start()
    {
        spriteSize = tilePrefab.GetComponent<SpriteRenderer>().size;
        
        gridParts = new TileInfo[gridSizeX, gridSizeY];

        

        GenerateGrid();
    }
    // grid ganeration according to grid size;
    void GenerateGrid()
    {
        float ofsetX = spriteSize.x;
        float ofsetY = spriteSize.y;
        GameObject gridPart;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                gridPart = Instantiate(tilePrefab, new Vector3(x*ofsetX, y*ofsetY, 0), Quaternion.identity,gameObject.transform);
                gridParts[x, y] = gridPart.GetComponent<TileInfo>();
                gridPart.name = "Tile" + x + "" + y;
                TileInfo tileInfo = gridPart.GetComponent<TileInfo>();
                tileInfo.coorX = x;
                tileInfo.coorY = y;
            }
        }

    }
    //get neigbors of a tile
    public List<TileInfo> GetNeigbors(TileInfo tile)
    {
        List<TileInfo> neigbors = new List<TileInfo>();

        for(int x = -1; x<= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = tile.coorX + x;
                int checkY = tile.coorY + y;
                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neigbors.Add(gridParts[checkX, checkY]);
                }
            }
        }

        return neigbors;
    }

    public int GetDistance(TileInfo tileA,TileInfo tileB)
    {
        int distX = Mathf.Abs(tileA.coorX - tileB.coorX);
        int distY = Mathf.Abs(tileA.coorY - tileB.coorY);
        if(distX > distY )
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }
    //finding path for unit to move
    public void FindPath(Unit unit, TileInfo endTile)
    {   
        TileInfo startTile = gridParts[unit.coordX, unit.coordY];
        startTile.isOccupied = false;
        List<TileInfo> openSet = new List<TileInfo>();
        HashSet<TileInfo> closeSet = new HashSet<TileInfo>();
        openSet.Add(startTile);

        while(openSet.Count>0)
        {
            
            TileInfo currentTile = openSet[0];

            for(int i=1; i<openSet.Count;i++)
            {
                if(openSet[i].fCost < currentTile.fCost || openSet[i].fCost == currentTile.fCost && openSet[i].hCost < currentTile.hCost)
                {
                    currentTile = openSet[i];
                }
            }

            openSet.Remove(currentTile);
            closeSet.Add(currentTile);
            if(currentTile == endTile)
            {
                List<TileInfo> path = new List<TileInfo>();
                TileInfo cTile = endTile;
                while(cTile != startTile)
                {
                    path.Add(cTile);
                    cTile = cTile.parent;
                }
                path.Reverse();
                unit.path = path;
                endTile.isOccupied = true;
                return;
            }

            foreach(TileInfo neigbor in GetNeigbors(currentTile))
            {
                if(neigbor.isOccupied || closeSet.Contains(neigbor))
                {
                    continue;
                }
                int newCost = currentTile.gCost + GetDistance(currentTile,neigbor);
                if(newCost < currentTile.gCost || !openSet.Contains(neigbor))
                {
                    neigbor.gCost = newCost;
                    neigbor.hCost = GetDistance(neigbor,endTile);
                    neigbor.parent = currentTile;
                    if(!openSet.Contains(neigbor))
                    {
                        openSet.Add(neigbor);
                    }
                }
            }
        }
    }
}
