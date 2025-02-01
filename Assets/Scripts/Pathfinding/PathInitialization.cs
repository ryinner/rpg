using UnityEngine;

public class PathInitialization : MonoBehaviour
{
    [SerializeField, Range(1, 20)]
    private int _xSize = 10;

    [SerializeField, Range(1, 20)]
    private int _zSize = 10;

    private Pathfinding _pathfinding;
    public Pathfinding Pathfinding { get => _pathfinding; }

    private void Awake()
    {
        _pathfinding = new Pathfinding(_xSize, _zSize);
    }
}

