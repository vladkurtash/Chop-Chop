using System;
using UnityEngine;

namespace ChopChop
{
    public abstract class AAxeRotateSubSystem<T> where T : IMovementSystemData
    {
        protected readonly T _systemData;
        protected readonly float _curveTimeStartValue = 0.0f;
        protected float _curveTimeTotal = 0.0f;
        protected float _targetAngle = 0.0f;
        protected float _angleTotal = 0.0f;
        protected AxeRotateSystem.SystemState _state = AxeRotateSystem.SystemState.None;
        protected Vector3 _rotationDelta = Vector3.zero;
        public event Action Completed;

        public AAxeRotateSubSystem(T systemData, float curveTimeStartValue)
        {
            _systemData = systemData;
            _curveTimeStartValue = curveTimeStartValue;
        }
        
        ///<summary>Need to call on each SystemState change</summary>
        ///<param name="systemState">AxeRotateSystem.SystemState</param>
        public void Setup(float currentAngle, AxeRotateSystem.SystemState systemState)
        {
            if(!SystemStateSatisfied(systemState))
            {
                Debug.LogWarning($"{typeof(T)} SystemState is NOT supported for this SubSystem");
                return;
            }

            Reset();

            _state = systemState;

            SetTargetAngle(currentAngle);
        }

        public Vector3 RotationDelta { get => _rotationDelta; }

        public void UpdateLocal(float deltaTime)
        {
            if (ReachedTargetAngle())
            {
                OnCompleted();
                return;
            }

            UpdateVariables(deltaTime);
        }

        private void UpdateVariables(float deltaTime)
        {
            UpdateTime(deltaTime);

            float previousRotation = _angleTotal;
            _angleTotal = GetAngleTotal();
            _rotationDelta.x = _angleTotal - previousRotation;
        }

        protected abstract void UpdateTime(float deltaTime);

        protected virtual float GetAngleTotal() => 0.0f;

        private bool ReachedTargetAngle()
        {
            return _angleTotal >= _targetAngle;
        }

        private void OnCompleted()
        {
            Completed?.Invoke();
        }

        private void Reset()
        {
            _state = AxeRotateSystem.SystemState.None;
            _curveTimeTotal = _curveTimeStartValue;
            _targetAngle = _angleTotal = 0.0f;
            _rotationDelta = Vector3.zero;
        }

        protected virtual void SetTargetAngle(float currentAngle) => _targetAngle = 0.0f;
        protected abstract bool SystemStateSatisfied(AxeRotateSystem.SystemState state);
    }
}