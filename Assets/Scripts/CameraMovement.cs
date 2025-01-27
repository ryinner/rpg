using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField, Range(0, 50)]
    private float speed = 20;

    [SerializeField]
    private new Camera camera;

    private Vector2 moveVector = Vector2.zero;
    private Vector2 moveAcceleration = Vector2.zero;

    private float zoomValue = 60;

    private void Update()
    {
        moveAcceleration = Vector2.Lerp(moveAcceleration, Quaternion.Euler(0, 0, transform.eulerAngles.y) * moveVector * speed, 0.1f);

        var direction = new Vector3(moveAcceleration.x, 0, moveAcceleration.y) * Time.deltaTime;

        if (Physics.Raycast(transform.position + direction + Vector3.up * 100, Vector3.down, out var hit))
        {
            var point = hit.point;

            point.y = Mathf.Lerp(transform.position.y, point.y, 0.01f);
            transform.position = point;
        }
    }

    private void OnMove(InputValue inputValue)
    {
        moveVector = inputValue.Get<Vector2>();
    }

    private void OnZoom(InputValue inputValue)
    {
       zoomValue = Mathf.Clamp(zoomValue - inputValue.Get<Vector2>().y * 10f, 30, 90);
       camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, zoomValue, 0.1f);
    }
}
