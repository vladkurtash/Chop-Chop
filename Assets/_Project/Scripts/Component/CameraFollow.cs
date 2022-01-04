using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float smoothSpeed = 0.125f;
    private Vector3 _offset;
    private Transform _transform;
    private bool _moving = false;

    public void Stop()
    {
        _moving = false;
    }

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _offset = _transform.position - target.position;
        _moving = true;
    }

    private void LateUpdate()
    {
        if (!_moving)
            return;

        Vector3 desiredPosition = target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        _transform.position = smoothedPosition;
    }
}