using System;
using UnityEngine;

namespace ChopChop
{
    public class AxeBladeView : AAxeView, IAxeBladeView
    {
        public event Action<Collider> OnTriggerEnterEvent;
        public event Action<RaycastHit> OnRaycastHitEvent;

        [SerializeField] private float rayLength;

        private void OnTriggerEnter(Collider otherCollider)
        {
            OnTriggerEnterEvent?.Invoke(otherCollider);
        }

        private void FixedUpdate()
        {
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