using UnityEngine;

namespace ChopChop
{
    public class AxeJumpRotateSubSystem : AAxeRotateSubSystem<IAxeJumpRotateSystemData>
    {
        /// <summary>Based on specific time part of the Common curve for the Jump and Fall phases</summary>
        public AxeJumpRotateSubSystem(IAxeJumpRotateSystemData systemData, float curveTimeStartValue) : base(systemData, curveTimeStartValue)
        { }

        sealed protected override float GetAngleTotal()
        {
            return _targetAngle * _systemData.Curve.Evaluate(_curveTimeTotal);
        }

        sealed protected override void SetTargetAngle(float currentAngle)
        {
            float target = _systemData.FlipAngle - currentAngle;

            if (target < _systemData.MinFlipAngle)
                target += _systemData.FlipAngle;

            _targetAngle = target;
        }

        sealed protected override void UpdateTime(float deltaTime)
        {
            if (SystemStateJumpOrSpringBack())
                _curveTimeTotal += deltaTime / _systemData.CurveSpeedJump;
            else if (SystemStateFallFlip())
                _curveTimeTotal += deltaTime / _systemData.CurveSpeedFallFlip;
        }

        private bool SystemStateJumpOrSpringBack()
        {
            return _state == AxeRotateSystem.SystemState.Jump || _state == AxeRotateSystem.SystemState.SpringBack;
        }

        private bool SystemStateFallFlip()
        {
            return _state == AxeRotateSystem.SystemState.FallFlip;
        }

        sealed protected override bool SystemStateSatisfied(AxeRotateSystem.SystemState state)
        {
            return SystemStateJumpOrSpringBack() ||
                SystemStateFallFlip();

            bool SystemStateJumpOrSpringBack()
            {
                return state == AxeRotateSystem.SystemState.Jump || state == AxeRotateSystem.SystemState.SpringBack;
            }

            bool SystemStateFallFlip()
            {
                return state == AxeRotateSystem.SystemState.FallFlip;
            }
        }
    }
}