using System;
using UnityEngine;

namespace ChopChop
{
    public class AxeBladeView : AAxeView, IAxeBladeView
    {
        public event Action<Collider> OnTriggerEnterEvent;
        public event Action<RaycastHit> OnRaycastHitEvent;

        private void OnTriggerEnter(Collider otherCollider)
        {
            OnTriggerEnterEvent?.Invoke(otherCollider);
        }

        private void FixedUpdate()
        {
            if(Physics.Raycast(transform.position, -transform.right, out RaycastHit hit, 0.1f))
            {
                Debug.DrawRay(transform.position, -transform.right * hit.distance, Color.green);
                OnRaycastHitEvent?.Invoke(hit);
            }
            else
            {
                Debug.DrawRay(transform.position, -transform.right * 0.1f, Color.red);
            }
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            EzySlice.Plane cuttingPlane = new EzySlice.Plane();
            cuttingPlane.Compute(transform);
            cuttingPlane.OnDebugDraw();
        }
#endif
    }
}