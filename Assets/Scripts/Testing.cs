using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField]
    private int _weigth = 8;

    [SerializeField]
    private int _height = 8;

    private Grid _grid;

    TerrainCollider terrainCollider;
    Ray ray;
    Plane plane = new Plane(Vector3.up, 0);

    void Start()
    {
        _grid = new Grid(_weigth, _height, 10f);

        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _grid.SetValue(GetMouseWorldPosition(), 1);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(_grid.GetValue(GetMouseWorldPosition()));
        }
    }

    private Vector3 GetMouseWorldPosition()
    {

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPosition = Vector3.zero;
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        return worldPosition;
    }

}
