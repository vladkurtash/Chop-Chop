using System;
using UnityEngine;

namespace ChopChop
{
    public class AxeBackView : AView, IAxeBackView
    {
        public event Action<Collider> OnTriggerEnterEvent;
        private void OnTriggerEnter(Collider otherCollider)
        {
            OnTriggerEnterEvent.Invoke(otherCollider);
        }
    }
}