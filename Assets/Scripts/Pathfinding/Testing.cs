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
            Grid<PathNode> grid = pathfinding.GetGrid();
            grid.GetXZ(GetMouseWorldPosition(), out int x, out int z);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, z); //Вместо "0,0" координаты персонажа
            if (path != null)
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, 0, path[i].z) * grid.GetCellSize() + new Vector3(1, 0, 1) * grid.GetCellSize() * .5f,
                    new Vector3(path[i + 1].x, 0, path[i + 1].z) * grid.GetCellSize() + new Vector3(1, 0, 1) * grid.GetCellSize() * .5f,
                    Color.green,
                    1f);

                    //Обоработка пути / движение


                }

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

