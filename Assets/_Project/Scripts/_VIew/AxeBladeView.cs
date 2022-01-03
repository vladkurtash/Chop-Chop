using System;
using UnityEngine;

namespace ChopChop
{
    public class AxeBladeView : AView, IAxeBladeView
    {
        public event Action<Collider> OnTriggerEnterEvent;

        private void OnTriggerEnter(Collider otherCollider)
        {
            OnTriggerEnterEvent?.Invoke(otherCollider);
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