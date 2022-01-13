using UnityEngine;

namespace ChopChop
{
    public class CameraPresenter : MonoBehaviour
    {
        public enum State
        {
            None,
            Follow,
            Align
        }

        private State _state = State.None;

        [SerializeField] public Transform target;
        [SerializeField] public float followMovementSpeed;
        [SerializeField] private Vector3 _offset;
        private Transform _transform = null;

        [SerializeField] public float alignmentMovementSpeed;
        [SerializeField] private Vector3 alignmentOffset;
        [SerializeField] private Vector3 alignmentRotation;
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
            SetCameraTransform(_offset, Quaternion.Euler(_currentRotation), followMovementSpeed);
        }

        private void SetCameraTransform(Vector3 positionOffset, Quaternion rotation, float movementSpeed)
        {
            Vector3 desiredPosition = target.position + positionOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, movementSpeed * Time.deltaTime);

            _nextRotation = Quaternion.Lerp(_transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
            _transform.SetPositionAndRotation(smoothedPosition, _nextRotation);
        }

        private void Align()
        {
            SetCameraTransform(alignmentOffset, Quaternion.Euler(alignmentRotation), alignmentMovementSpeed);
        }
    }
}