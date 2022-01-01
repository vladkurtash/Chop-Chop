using System;
using UnityEngine;

namespace ChopChop
{
    public class AxeRotateSystem : IDisposable
    {
        private readonly IRotate _rotateData = null;
        private readonly AxeJumpRotateSubSystem _jumpSystem = null;
        private readonly AxeFallRotateSubSystem _fallSystem = null;
        private SystemState _state = SystemState.None;

        public AxeRotateSystem(IRotate rotateData, AxeRotateSystemData systemData)
        {
            _rotateData = rotateData;

            _jumpSystem = new AxeJumpRotateSubSystem(systemData, 0.0f);
            _fallSystem = new AxeFallRotateSubSystem(systemData, 1.0f);

            _jumpSystem.Completed += OnJumpStateCompleted;
            _fallSystem.Completed += OnFallStateCompleted;
        }

        public enum SystemState
        {
            None,
            Jump,
            SpringBack,
            Fall,
            FallFlip
        }

        public Vector3 RotationDelta { get; private set; }

        private void Reset()
        {
            RotationDelta = Vector3.zero;
            _state = SystemState.None;
        }

        public void SetState(SystemState state)
        {
            Reset();

            _state = state;

            if (StateJumpOrSpringBackOrFallFlip())
            {
                _jumpSystem.Setup(_rotateData.Rotataion.x, state);
            }
            else if (state == SystemState.Fall)
            {
                _fallSystem.Setup(_rotateData.Rotataion.x, state);
            }
        }

        public void UpdateLocal(float deltaTime)
        {
            if (_state == SystemState.None)
                return;

            if (StateJumpOrSpringBackOrFallFlip())
            {
                _jumpSystem.UpdateLocal(deltaTime);
                RotationDelta = _jumpSystem.RotationDelta;
            }
            else if (_state == SystemState.Fall)
            {
                _fallSystem.UpdateLocal(deltaTime);
                RotationDelta = _fallSystem.RotationDelta;
            }
        }

        private bool StateJumpOrSpringBackOrFallFlip()
        {
            return _state == SystemState.Jump || _state == SystemState.SpringBack ||
                _state == SystemState.FallFlip;
        }

        private void OnJumpStateCompleted()
        {
            SetState(SystemState.Fall);
        }

        private void OnFallStateCompleted()
        {
            SetState(SystemState.FallFlip);
        }

        public void Dispose()
        {
            _jumpSystem.Completed -= OnJumpStateCompleted;
            _fallSystem.Completed -= OnFallStateCompleted;
        }
    }
}