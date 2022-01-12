using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public enum State
    {
        None,
        Follow,
        Align
    }

    private State _state = State.None;

    [SerializeField] public Transform target;
    [SerializeField] public float smoothSpeed = 0.125f;
    private Vector3 _offset = Vector3.zero;
    private Transform _transform = null;

    [SerializeField] private Vector3 _alignmentOffset = Vector3.zero;
    [SerializeField] private Vector3 _alignmentRotation = Vector3.zero;
    [SerializeField] private float _rotationSpeed;
    private Vector3 _currentRotation;
    private Quaternion _nextRotation;

    public void SetState(State state)
    {
        _state = state;
    }

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _offset = _transform.position - target.position;
        _alignmentOffset += _offset;

        _currentRotation = _transform.rotation.eulerAngles;
        _state = State.Follow;
    }

    private void LateUpdate()
    {
        if (_state == State.None)
            return;

        if (_state == State.Follow)
            Follow();
        else if (_state == State.Align)
            Align();
    }

    private void Follow()
    {
        SetCameraTransform(_offset, Quaternion.Euler(_currentRotation));
    }

    private void SetCameraTransform(Vector3 positionOffset, Quaternion rotation)
    {
        Vector3 desiredPosition = target.position + positionOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        _nextRotation = Quaternion.Lerp(_transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        _transform.SetPositionAndRotation(smoothedPosition, _nextRotation);
    }

    private void Align()
    {
        SetCameraTransform(_alignmentOffset, Quaternion.Euler(_alignmentRotation));
    }
}