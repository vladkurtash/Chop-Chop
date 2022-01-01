using UnityEngine;

namespace ChopChop
{
    public class AxeFallRotateSubSystem : AAxeRotateSubSystem<IAxeFallRotateSystemData>
    {
        /// <summary>Based on specific time part of the Common curve for the Jump and Fall phases</summary>
        public AxeFallRotateSubSystem(IAxeFallRotateSystemData systemData, float curveTimeStartValue) : base(systemData, curveTimeStartValue)
        { }

        sealed protected override float GetAngleTotal()
        {
            if (SystemStateFall())
                return _targetAngle * (Mathf.Abs(_systemData.Curve.Evaluate(_curveTimeTotal) - 1));

            return 0.0f;
        }

        sealed protected override void SetTargetAngle(float currentAngle)
        {
            float target = _systemData.MaxAngle - currentAngle;

            if (currentAngle > _systemData.MaxAngle)
                target = 0.0f;

            _targetAngle = target;
        }

        sealed protected override void UpdateTime(float deltaTime)
        {
            _curveTimeTotal += deltaTime / _systemData.CurveSpeed;
        }

        private bool SystemStateFall()
        {
            return _state == AxeRotateSystem.SystemState.Fall;
        }

        sealed protected override bool SystemStateSatisfied(AxeRotateSystem.SystemState state)
        {
            return SystemStateFall();

            bool SystemStateFall()
            {
                return state == AxeRotateSystem.SystemState.Fall;
            }
        }
    }
}