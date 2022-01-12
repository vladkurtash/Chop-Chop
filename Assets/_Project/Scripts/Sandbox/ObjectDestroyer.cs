using UnityEngine;

namespace ChopChop
{
    public class ObjectDestroyer : MonoBehaviour
    {
        private float _distanceToDestroy = 5.0f;
        private Transform _cameraTransform = null;
        private Vector3 _vectorToCamera = Vector3.zero;
        private Transform _transform = null;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
            _transform = this.gameObject.GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            DestroyDistantObject();
        }

        private void DestroyDistantObject()
        {
            _vectorToCamera = (_cameraTransform.position - this.gameObject.transform.position);

            if (BehindCamera(_vectorToCamera, _transform.position))
            {
                if (ObjectFarEnoughBehindCamera())
                    Destroy(this.gameObject);
            }

            #region LocalFunctions

            bool BehindCamera(Vector3 vector1, Vector3 vector2) =>
                Vector3.Dot(vector1, vector2) < 0;

            bool ObjectFarEnoughBehindCamera()
                => _vectorToCamera.sqrMagnitude > _distanceToDestroy * _distanceToDestroy;

            #endregion

#if UNITY_EDITOR
            if (!BehindCamera(_vectorToCamera, _transform.position))
                Debug.DrawLine(_cameraTransform.position, _transform.position, Color.green);
            else if (BehindCamera(_vectorToCamera, _transform.position))
                Debug.DrawLine(_cameraTransform.position, _transform.position, Color.red);
#endif
        }
    }
}