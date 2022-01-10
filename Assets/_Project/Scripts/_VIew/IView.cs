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

    public interface IAxeView : IView, ITriggerable
    {
        Renderer Renderer { get; }
    }

    public interface IAxeRootView : IView
    {
        IAxeBladeView BladeView { get; }
        IAxeBackView BackView { get; }
        void OnDisable();
    }

    public interface IAxeBladeView : IAxeView
    { }

    public interface IAxeBackView : IAxeView
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