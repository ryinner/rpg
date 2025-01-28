using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Grid<TGridObject>
{

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int z;
    }

    private int _width;
    private int _height;
    private float _cellSize;
    Vector3 _originPosition;
    private TGridObject[,] _gridArray;

    private bool _showDebug = true;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        _height = height;
        _width = width;
        _cellSize = cellSize;
        _originPosition = originPosition;

        _gridArray = new TGridObject[width, height];

        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < _gridArray.GetLength(1); z++)
            {
                _gridArray[x, z] = createGridObject(this, x, z);
            }
        }

        if (_showDebug)
        {
            TextMesh[,] _debugTextArray = new TextMesh[width, height];
            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < _gridArray.GetLength(1); z++)
                {
                    _debugTextArray[x, z] = CreateWorldText(null, _gridArray[x, z]?.ToString(), GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter, TextAlignment.Left, 5000);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 1000f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 1000f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                _debugTextArray[eventArgs.x, eventArgs.z].text = _gridArray[eventArgs.x, eventArgs.z]?.ToString();
            };
        }
    }

    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public void SetGridObject(int x, int z, TGridObject value)
    {
        if (x >= 0 && z >= 0 && x < _width && z < _height)
        {
            _gridArray[x, z] = value;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, z = z });
        }
    }

    public void TriggerGridObjectChanged(int x, int z)
    {
        if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, z = z });
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetGridObject(x, z, value);
    }

    public TGridObject GetGridObject(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < _width && z < _height)
        {
            return _gridArray[x, z];
        }
        else
        {
            return default(TGridObject);
        }

    }
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetGridObject(x, z);
    }


    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        z = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
    }
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * _cellSize + _originPosition;
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetHeigth()
    {
        return _height;
    }

    public float GetCellSize()
    {
        return _cellSize;
    }

    public bool GetShowDebug()
    {
        return _showDebug;
    }

}

