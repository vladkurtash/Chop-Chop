using UnityEngine;

namespace ChopChop
{
    public interface ISystemData
    { }

    public interface IMovementSystemData
    {
        AnimationCurve Curve { get; }
    }

    public interface IAxeMoveSystemData
    {
        AnimationCurve Curve { get; }
        float CurveSpeed { get; }
        float HeightMax { get; }
        float DistanceMax { get; }
        float FallSpeedMax { get; }
    }

    public interface IAxeJumpRotateSystemData : ISystemData, IMovementSystemData
    {
        float CurveSpeedJump { get; }
        float CurveSpeedFallFlip { get; }
        float MinFlipAngle { get; }
        float FlipAngle { get; }
    }

    public interface IAxeFallRotateSystemData : ISystemData, IMovementSystemData
    {
        float CurveSpeed { get; }
        float MaxAngle { get; }
    }
}