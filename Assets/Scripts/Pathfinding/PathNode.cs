using UnityEngine;

public class PathNode
{

    private Grid<PathNode> _grid;
    public int _x, _z;

    public int x { get { return _x; } set { _x = value; } }
    public int z { get { return _z; } set { _z = value; } }

    private bool _isWalkable;
    public bool isWalkable { get { return _isWalkable; } set { _isWalkable = value; } }

    private int _gCost;
    public int gCost { get { return _gCost; } set { _gCost = value; } }
    private int _hCost;
    public int hCost { get { return _hCost; } set { _hCost = value; } }
    private int _fCost;
    public int fCost { get { return _fCost; } set { _fCost = value; } }

    private PathNode _cameFromNode;
    public PathNode cameFromNode { get { return _cameFromNode; } set { _cameFromNode = value; } }

    public PathNode(Grid<PathNode> grid, int x, int z)
    {
        _grid = grid;
        _x = x;
        _z = z;
        _isWalkable = true;
    }

    public override string ToString()
    {
        return _x + ", " + _z;
    }

    public void CalculateFCost()
    {
        _fCost = _gCost + _hCost;
    }

}
