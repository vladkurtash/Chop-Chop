using UnityEngine;

namespace ChopChop
{
    public class ObjectDestroyer : MonoBehaviour
    {
        private float _distanceToDestroy = 3.0f;
        private Transform _cameraTransform = null;
        private Vector3 _vectorToCamera = Vector3.zero;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            DestroyDistantObject();
        }

        private void DestroyDistantObject()
        {
            _vectorToCamera = (_cameraTransform.position - this.gameObject.transform.position);

            if (!BehindCamera(_vectorToCamera, Camera.main.transform.forward))
                return;

            if (ObjectFarEnoughBehindCamera())
            {
                Destroy(this.gameObject);
            }

            #region LocalFunctions

            bool BehindCamera(Vector3 vector1, Vector3 vector2) =>
                Vector3.Angle(_vectorToCamera, Camera.main.transform.forward) > 90;

            bool ObjectFarEnoughBehindCamera()
                => _vectorToCamera.sqrMagnitude > _distanceToDestroy * _distanceToDestroy;

            #endregion
        }
    }
}