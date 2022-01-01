using System;
using UnityEngine;

namespace ChopChop
{
    public class AxeModel : IAxeModel
    {
        private ObservableVariable<Vector3> _position = new ObservableVariable<Vector3>(Vector3.zero);
        private AxeRotationVariable _rotataion = new AxeRotationVariable(Vector3.zero);

        public event Action Moved;
        public event Action Rotated;

        public Vector3 Position => _position.Value;
        public Vector3 Rotataion => _rotataion.Value;

        public AxeModel(Vector3 position, Vector3 rotataion)
        {
            _position.Value = position;
            _rotataion.Value = rotataion;

            _position.ValueChanged += OnMoved;
            _rotataion.ValueChanged += OnRotated;
        }

        private void OnRotated()
        {
            Rotated.Invoke();
        }
        private void OnMoved()
        {
            Moved.Invoke();
        }

        public void SetPosition(Vector3 position)
        {
            _position.Value = position;
        }

        public void AddPosition(Vector3 position)
        {
            _position.Value += position;
        }

        public void AddPosition(float y)
        {
            _position.Value += new Vector3(0, y, 0);
        }

        public void AddPosition(float y, float z)
        {
            _position.Value += new Vector3(0, y, z);
        }

        public void SetRotation(Vector3 rotation)
        {
            _rotataion.Value = rotation;
        }

        public void SetRotation(float x)
        {
            _rotataion.Value = new Vector3(x, _rotataion.Value.y, _rotataion.Value.z);
        }

        public void AddRotation(float x)
        {
            _rotataion.Value += new Vector3(x, 0, 0);
        }

        public void AddRotation(Vector3 rotation)
        {
            _rotataion.Value += rotation;
        }
    }
}