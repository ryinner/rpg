using System.Collections.Generic;
using UnityEngine;

class MovementByPoints : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> _points = new();

    public List<Vector3> Points { get => _points; }

    [SerializeField, Range(1, 20), Tooltip("How fast it will be moved between points")]
    private int _speed = 1;

    private int _currentPointIndex = 0;

    private bool _isMoving = false;

    public bool IsMoving { get => _isMoving; }

    private void Update()
    {
        if (_isMoving)
        {
            if (_currentPointIndex < _points.Count)
            {
                var currentPoint = _points[_currentPointIndex];
                transform.position = Vector3.MoveTowards(transform.position, currentPoint, _speed * Time.deltaTime);

                var direction = transform.position - currentPoint;
                var targetDirection = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetDirection, 5 * Time.deltaTime);

                var distance = Vector3.Distance(transform.position, currentPoint);

                if (distance <= 0.05f)
                {
                    _currentPointIndex++;
                }
            }
            else
            {
                _isMoving = false;
            }
        }
    }

    public void StartMoving(List<Vector3> points)
    {
        _points = points;
        _isMoving = true;
        _currentPointIndex = 0;
    }
}