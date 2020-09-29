using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update

    pathFinding PathFinding;
    public Transform player;
    public Transform obstacles;
    float velocity;
    int i = 0;
    public List<GameObject> obstacle;
    int obsX;
    int obsY;

    public void Start()
    {
        PathFinding = new pathFinding(20, 20);


    }

    private void Update()
    {

        
        Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
        PathFinding.GetGrid().getXY(mousePos, out int x, out int y);
        PathFinding.GetGrid().getXY(player.position, out int playerX, out int playerY);
        PathFinding.GetGrid().getXY(transform.position, out int EnemyX, out int EnemyY);
        // PathFinding.GetGrid().getXY(obstacles.position, out int NwX, out int NwY);
       
            PathFinding.getNode(PathFinding.GetGrid().getXYofObstacles(obstacle, obsX, obsY).Item1, PathFinding.GetGrid().getXYofObstacles(obstacle, obsX, obsY).Item2).setWalkableNode(obstacles.position, false);



        List<node> path = PathFinding.retracePath(EnemyX, EnemyY, playerX, playerY);

      //  enemyMove(path, i);



        if (path != null)
        {

            for (int i = 0; i < path.Count - 1; i++)
            {

                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.red);

            }
        }






        if (Input.GetKey(KeyCode.L))
        {


            Debug.Log(PathFinding.getNode(x, y).isWalkable);
        }
    }

    private void enemyMove(List<node> path, int i)
    {
        if (path.Count > i)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, Time.deltaTime * 3f);
            i++;
            enemyMove(path, i);
        }


    }


          
    


}
