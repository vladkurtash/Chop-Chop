using UnityEngine;
using System;

namespace ChopChop
{
    public interface IPresenter : IDisposable
    {
        event Action Disabling;
        void Setup();
    }

    public interface IAxePresenter : IPresenter
    { 
        void OnBladeTriggerEnterReact(Collider otherCollider);
        void OnBackTriggerEnterReact(Collider otherCollider);
        void OnMoved();
        void OnRotated();
        void Death();
    }
}