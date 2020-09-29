using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class pathFinding : MonoBehaviour
{
    int HEUSTRIC_COST = 14;
    int STRAIGHT_COST = 10;
    private grid<node> GridNodes;
    private List<node> openList;
    private HashSet<node> closedList;

    public grid<node> GetGrid()
    {
        return GridNodes;
    }

    public pathFinding(int width, int height)
    {
       
        GridNodes = new grid<node>(width, height, 10f, Vector2.zero,  (grid<node> g, int , int y) => new node(g,x,y));
    }

    public List<node> retracePath(int startX, int startY, int endX, int endY)
    {
        //intialisation
        node startNode = GridNodes.getGridObjectPos(startX, startY);
        node TargetNode = GridNodes.getGridObjectPos(endX, endY);

        openList = new List<node>();
        closedList = new HashSet<node>();

        openList.Add(startNode);

        for (int i = 0; i < GridNodes.getWidth(); i++)
        {
            for (int j = 0; j < GridNodes.getHeight(); j++)
            {
                node pathNode = GridNodes.getGridObjectPos(i, j);
                pathNode.gCost = int.MaxValue;
                pathNode.fCost();
                
                pathNode.prevNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = getDistance(startNode,TargetNode);
        startNode.fCost();
       
        //end of intialisiing pathFinding

        //pathFinding
        while (openList.Count > 0)
        {
            node currentNodeInList = GetlowestFcostInList(openList);
            
            //reached endNode
            if (currentNodeInList == TargetNode)
            {
               
                return calculatedPath(TargetNode);
            }
           
            openList.Remove(currentNodeInList);
            closedList.Add(currentNodeInList);
            

            foreach (node neigbourNode in getNeighbours(currentNodeInList))
            {
                
                if (closedList.Contains(neigbourNode))
                {
                    continue;
                }
                if (neigbourNode.isWalkable == false)
                {
                    closedList.Add(neigbourNode);
                    continue;
                }

                //checking to see if this gCost is lower than the current gCost if so then update the path 
                int tentativeGCost = currentNodeInList.gCost + getDistance(currentNodeInList, neigbourNode);
                
                if (tentativeGCost < neigbourNode.gCost)
                {
                   
                    neigbourNode.prevNode = currentNodeInList;
                    neigbourNode.gCost = tentativeGCost;
                    neigbourNode.hCost = getDistance(neigbourNode, TargetNode);
                    neigbourNode.fCost();
                    
                    if (!openList.Contains(neigbourNode))
                    {
                        openList.Add(neigbourNode);
                        
                    }
                }

            }
        }

        // out of the nodes on the open list
        
        return null;
    }

    private List<node> getNeighbours(node currentNode)
    {
        List<node> neighbours = new List<node>();
        //left neighbour
        if (currentNode.x - 1 >= 0)
        {

            neighbours.Add(getNode(currentNode.x - 1, currentNode.y));
            //leftDown
            if (currentNode.y - 1 >= 0)
            {
                neighbours.Add(getNode(currentNode.x - 1, currentNode.y - 1));
            }
            //leftup
            if (currentNode.y + 1 >= 0)
            {
                neighbours.Add(getNode(currentNode.x - 1, currentNode.y + 1));
            }
        }

        //right Neighbour
        if (currentNode.x + 1 >= 0)
        {
            neighbours.Add(getNode(currentNode.x + 1, currentNode.y));

            //rightDown
            if (currentNode.y - 1 >= 0)
            {
                neighbours.Add(getNode(currentNode.x + 1, currentNode.y - 1));

            }
            //rightup
            if (currentNode.y + 1 >= 0)
            {
                neighbours.Add(getNode(currentNode.x + 1, currentNode.y + 1));

            }
        }

        //upNeighbour
        if (currentNode.y + 1 >= 0)
        {
            neighbours.Add(getNode(currentNode.x, currentNode.y + 1 ));
        }
        //leftNeigbour
        if (currentNode.y - 1 >= 0)
        {
            neighbours.Add(getNode(currentNode.x, currentNode.y - 1));
        }

        return neighbours;
    }

    public node getNode(int x, int y)
    {
        
        return GridNodes.getGridObjectPos(x,y);
    }

    private List<node> calculatedPath(node endNode)
    {
        List<node> pathToTarget = new List<node>();

        pathToTarget.Add(endNode);
        node currentNode = endNode;

        while(currentNode.prevNode != null)
        {
            pathToTarget.Add(currentNode.prevNode);
            currentNode = currentNode.prevNode;
        }

        pathToTarget.Reverse();

        return pathToTarget;
    }

    private int getDistance(node a, node b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return HEUSTRIC_COST * Mathf.Min(xDistance, yDistance) + STRAIGHT_COST * remaining;

    }

    private node GetlowestFcostInList(List<node> openlistNode)
    {
        node lowestFcost = openlistNode[0];
        for (int i = 1; i < openlistNode.Count; i++)
        {
            if (openlistNode[i].fCost() < lowestFcost.fCost())
            {
                lowestFcost = openlistNode[i];
            }
        }
        return lowestFcost;
    }
}
