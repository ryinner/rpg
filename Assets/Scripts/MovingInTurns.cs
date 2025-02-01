using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementByPoints))]
[RequireComponent(typeof(Character))]
public class MovingInTurns : MonoBehaviour
{
    [SerializeField]
    private PathInitialization _pathfinding;

    private Character _character;

    private MovementByPoints _movement;

#warning ADD ORDER TO INPUT SYSTEM
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Down");
            Grid<PathNode> grid = _pathfinding.Pathfinding.GetGrid();

            grid.GetXZ(GetMouseWorldPosition(), out int x, out int z);
            grid.GetXZ(transform.position, out int characterX, out int characterZ);

            List<PathNode> path = _pathfinding.Pathfinding.FindPath(characterX, characterZ, x, z);

            int countAvailablePathNodes = path.Count;

            int characterCountAvailablePathNodes = _character.Speed / 5;
            if (characterCountAvailablePathNodes < path.Count)
            {
                countAvailablePathNodes = characterCountAvailablePathNodes;
            }

            if (path != null)
            {
                var _path = new List<Vector3>();
                for (int i = 1; i < countAvailablePathNodes; i++)
                {
                    var Point =  new Vector3(path[i].x, 0, path[i].z) * grid.GetCellSize() + .5f * grid.GetCellSize() * new Vector3(1, 0, 1);
                    Point.y = transform.position.y;
                    _path.Add(Point);
                }
                _movement.StartMoving(_path);
                _character.Speed -= _path.Count * 5;
            }
        }
    }

    private void Awake()
    {
        _character = GetComponent<Character>();
        _movement = GetComponent<MovementByPoints>();
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
