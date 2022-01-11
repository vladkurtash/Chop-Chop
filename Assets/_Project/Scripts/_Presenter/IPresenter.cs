using UnityEngine;
using System;

namespace ChopChop
{
    public interface IPresenter : IDisposable
    {
        event Action Disabling;
        event Action Destroying;
        void Setup();
    }

    public interface IAxePresenter : IPresenter
    { 
        event Action Hit;
        void OnBladeTriggerEnterReact(Collider otherCollider);
        void OnBackTriggerEnterReact(Collider otherCollider);
        void OnMoved();
        void OnRotated();
        void Death();
    }
}