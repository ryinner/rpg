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
            Grid<PathNode> grid = pathfinding.GetGrid();
            grid.GetXZ(position, out int x, out int z);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, z);
            if (path != null)
                for (int i = 0; i < path.Count - 1; i++)
                {

                    if (grid.GetShowDebug()) Debug.DrawLine(new Vector3(path[i].x, 0, path[i].z) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f, new Vector3(path[i + 1].x, 0, path[i + 1].z) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f, Color.green, 1f);
                }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 position = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXZ(position, out int x, out int z);

            PathNode pathNode = pathfinding.GetNode(x, z);
            if (pathNode != null) pathNode.isWalkable = !pathNode.isWalkable;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPosition = Vector3.zero;
        if (new Plane(Vector3.up, 0).Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        return worldPosition;
    }
}

