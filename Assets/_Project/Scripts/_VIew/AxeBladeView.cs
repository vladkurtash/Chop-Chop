using System;
using UnityEngine;

namespace ChopChop
{
    public class AxeBladeView : AAxeView, IAxeBladeView
    {
        public event Action<Collider> OnTriggerEnterEvent;

        private void OnTriggerEnter(Collider otherCollider)
        {
            OnTriggerEnterEvent?.Invoke(otherCollider);
        }

        private void FixedUpdate()
        {
            //Physics.Raycast(transform.position, transform.forward, )
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            EzySlice.Plane cuttingPlane = new EzySlice.Plane();
            cuttingPlane.Compute(transform);
            cuttingPlane.OnDebugDraw();

            Debug.DrawRay(transform.position, -transform.right, Color.red);
        }
#endif
    }
}