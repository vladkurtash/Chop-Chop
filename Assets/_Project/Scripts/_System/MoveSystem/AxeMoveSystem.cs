using System;
using UnityEngine;

namespace ChopChop
{
    public class AxeMoveSystem
    {
        private readonly IAxeMoveSystemData _systemData = null;
        private float _curveTimeTotal = 0.0f;
        private float _heightTotal = 0.0f;
        private float _distanceTotal = 0.0f;
        private SystemState _state = SystemState.None;
        private MoveDirection _direction = MoveDirection.Forward;
        private Vector3 _positionDelta = Vector3.zero;

        public AxeMoveSystem(IAxeMoveSystemData systemData)
        {
            _systemData = systemData;
        }

        public enum SystemState
        {
            None,
            Jump,
            SpringBack
        }

        private enum MoveDirection
        {
            Forward = 1,
            Back = -1
        }

        public Vector3 PositionDelta { get => _positionDelta; }

        ///<summary>Need to call on each MovementState change</summary>
        private void Setup(MoveDirection side)
        {
            _direction = side;
        }

        ///<param name="state">SystemState.SystemState</param>
        public void SetState(SystemState systemState)
        {
            Reset();

            _state = systemState;

            if (systemState == SystemState.Jump)
                Setup(MoveDirection.Forward);
            else if (systemState == SystemState.SpringBack)
                Setup(MoveDirection.Back);
        }

        private void Reset()
        {
            _curveTimeTotal = _heightTotal = _distanceTotal = 0.0f;
            _positionDelta = Vector3.zero;
            _direction = MoveDirection.Forward;
            _state = SystemState.None;
        }

        public void UpdateLocal(float deltaTime)
        {
            if (Completed())
                return;

            _curveTimeTotal += deltaTime / _systemData.CurveSpeed;

            UpdateVariables(deltaTime);
        }

        private void UpdateVariables(float deltaTime)
        {
            UpdateHeight();
            UpdateDistance();

            if (FallCurvePart())
            {
                IncreaseFallSpeed();
                DecreaseDistance();
            }
        }

        private void UpdateHeight()
        {
            float previousHeightTotal = _heightTotal;
            _heightTotal = _systemData.HeightMax * _systemData.Curve.Evaluate(_curveTimeTotal);

            _positionDelta.y = _heightTotal - previousHeightTotal;
        }

        private void UpdateDistance()
        {
            float previousDistanceTotal = _distanceTotal;
            _distanceTotal = _systemData.DistanceMax * _curveTimeTotal * (int)_direction;

            _positionDelta.z = _distanceTotal - previousDistanceTotal;
        }

        private void IncreaseFallSpeed()
        {
            _positionDelta.y += _systemData.FallSpeedMax * _systemData.Curve.Evaluate(_curveTimeTotal);
        }

        ///<summary>Multiplies the distanceDelta by a percentage from one to zero 
        ///so that gradually the distanceDelta is zero</summary>
        private void DecreaseDistance()
        {
            _positionDelta.z *= (1 - Mathf.Abs(_systemData.Curve.Evaluate(_curveTimeTotal)));
        }

        ///<summary>The "None" state is set by the AxeMoveSystemRouter using the "Stop" method (when the Axe is stucked) </summary>
        private bool Completed()
        {
            return _state == SystemState.None;
        }

        private bool FallCurvePart()
        {
            return _curveTimeTotal >= 1.0f;
        }

        private bool SystemStateSatisfied()
        {
            return _state == SystemState.Jump || _state == SystemState.SpringBack;
        }
    }
}