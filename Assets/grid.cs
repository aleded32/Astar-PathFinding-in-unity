using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class grid<Tobject>
{
    // Start is called before the first frame update
    int width;
    int height;
    public Tobject[,] gridArray;
    Vector3 originOfGrid;
    float cellsize;


    public grid(int _width, int _height, float _cellsize, Vector3 _originOfGrid , Func<grid<Tobject>, int, int,   Tobject> nodesInGrid)
    {
        this.width = _width;
        this.height = _height;
        this.cellsize = _cellsize;
        this.originOfGrid = _originOfGrid;

        gridArray = new Tobject[width, height];

       
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Debug.DrawLine(WorldPos(i, j), WorldPos(i,j + 1), Color.white, 100f);
                Debug.DrawLine(WorldPos(i, j), WorldPos(i + 1 , j), Color.white, 100f);
                
                gridArray[i, j] = nodesInGrid(this, i,j);
                
            }
        }
        Debug.DrawLine(WorldPos(0, height), WorldPos(width, height), Color.white, 100f);
        Debug.DrawLine(WorldPos(width, 0), WorldPos(width, height), Color.white, 100f);


    }


    

    private Vector3 WorldPos(int x, int y)
    {
        return new Vector3(x, y) * cellsize - originOfGrid;
    }

    public Tobject getGridObjectPos(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default;
        }
    }

    public void getXY(Vector3 mousePos, out int x, out int y)
    {
        
        x = Mathf.FloorToInt(mousePos.x / cellsize);
        y = Mathf.FloorToInt(mousePos.y / cellsize);
        if (x < 0 || y < 0)
        {
            x = 0;
            y = 0;
        }
        
        
        
        
    }

    public Tuple<int,int> getXYofObstacles(List<GameObject> obstacles, int obsX, int obsY)
    {
      
        for (int i = 0; i < obstacles.Count; i++)
        {
            obsX = Mathf.FloorToInt(obstacles[i].transform.position.x / cellsize);
            obsY = Mathf.FloorToInt(obstacles[i].transform.position.y / cellsize);
        }

        return new Tuple<int, int>(obsX, obsY);

    }

    public void  setWalkable(Vector3 worldPos, Tobject value)
    {
        int x, y;
        getXY(worldPos, out x, out y);
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }


    public int getWidth()
    {
        return width;
    }
    public int getHeight()
    {
        return height;
    }
  
    
}
