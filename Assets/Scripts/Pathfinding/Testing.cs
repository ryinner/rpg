using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Testing : MonoBehaviour
{

    private Pathfinding pathfinding;
    private void Start()
    {
        pathfinding = new Pathfinding(10, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXZ(position, out int x, out int z);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, z);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, 0, path[i].z) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, 0, path[i + 1].z) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 position = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXZ(position, out int x, out int z);
            
            PathNode pathNode = pathfinding.GetNode(x, z);
            if(pathNode != null) pathNode.isWalkable = !pathNode.isWalkable;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPosition = Vector3.zero;
        if (new Plane(Vector3.up, 0).Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        return worldPosition;
    }
}

