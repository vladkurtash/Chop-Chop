using System;
using UnityEngine;

namespace ChopChop
{
    public interface IView
    {
        Transform Transform { get; }
        void SetPosition(Vector3 position);
        void SetRotation(Vector3 rotation);
    }

    public interface IAxeView : IView
    {
        IAxeBladeView BladeView { get; }
        IAxeBackView BackView { get; }
    }

    public interface IAxeBladeView : IView, ITriggerable
    { }

    public interface IAxeBackView : IView, ITriggerable
    { }

    public interface ITriggerable
    {
        event Action<Collider> OnTriggerEnterEvent;
    }

    public interface ICollisionable
    {
        event Action<Collision> OnCollisionEnterEvent;
    }
}