using System;
using UnityEngine;

namespace ChopChop
{
    public interface IModel
    { }

    public interface IAxeModel : IModel, IMove, IRotate
    { }

    public interface IMove
    {
        Vector3 Position { get; }
        // Vector3 Direction { get; }
        // float Speed { get; }

        event Action Moved; 

        void SetPosition(Vector3 position);
        void AddPosition(Vector3 position);
        void AddPosition(float y);
        void AddPosition(float y, float z);
        // void SetDirection(Vector3 direction);
    }

    public interface IRotate
    {
        Vector3 Rotataion { get; }
        event Action Rotated;

        void SetRotation(Vector3 rotation);
        void SetRotation(float x);
        void AddRotation(float x);
        void AddRotation(Vector3 rotation);
        // float RotationSpeed { get; }
    }
}