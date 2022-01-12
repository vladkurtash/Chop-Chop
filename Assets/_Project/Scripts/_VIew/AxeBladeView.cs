using System;
using UnityEngine;

namespace ChopChop
{
    public class AxeBladeView : AAxeView, IAxeBladeView
    {
        public event Action<Collider> OnTriggerEnterEvent;
        public event Action<RaycastHit> OnRaycastHitEvent;

        [SerializeField] private float rayLength;
        private bool _wasFirstCollision = false;

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (_wasFirstCollision)
            {
                OnTriggerEnterEvent?.Invoke(otherCollider);
                return;
            }

            _wasFirstCollision = true;
        }

        private void FixedUpdate()
        {
            //Since the function "OnTriggerEnter" occurs after "FixedUpdate" function, the flag "_wasFirstCollision" is updated in it 
            if (!_wasFirstCollision)
                return;

            if (Physics.Raycast(transform.position, -transform.right, out RaycastHit hit, rayLength))
            {
#if UNITY_EDITOR
                Debug.DrawRay(transform.position, -transform.right * hit.distance, Color.green);
#endif
                OnRaycastHitEvent?.Invoke(hit);
            }
#if UNITY_EDITOR
            else
            {
                Debug.DrawRay(transform.position, -transform.right * rayLength, Color.red);
            }
#endif
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            EzySlice.Plane cuttingPlane = new EzySlice.Plane();
            cuttingPlane.Compute(transform);
            cuttingPlane.OnDebugDraw();

            Debug.DrawRay(transform.position, -transform.right * rayLength, Color.red);
        }
#endif
    }
}