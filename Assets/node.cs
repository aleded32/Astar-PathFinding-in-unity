using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node
{

    public int x;
    public int y;
    private grid<node> nodeGrid;
    public bool isWalkable;

    public int gCost;
    public int hCost;
    public int fCost()
    {
        return gCost + hCost;
    }
    
    public node prevNode;

    public node(grid<node> nodeGrid, int x, int y)
    {
        this.nodeGrid = nodeGrid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    public void setWalkableNode(Vector3 worldpos,bool isWalkable)
    {
        this.isWalkable = isWalkable;
        nodeGrid.setWalkable(worldpos, this);

    }
    

}
