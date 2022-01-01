using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "AxeRotateSystemData", menuName = "SystemData/AxeRotateSystemData", order = 1)]
    public class AxeRotateSystemData : ScriptableObject, IAxeJumpRotateSystemData, IAxeFallRotateSystemData, IMovementSystemData
    {
        [Header("Jump Rotation Settings")]
        [SerializeField, Rename("Curve")] public AnimationCurve rotationCurve;

        [Tooltip("Max rotation speed for Max possible degree")]
        [SerializeField, Rename("Curve Jump Speed")] public float jumpRotationCurveSpeed;
        [SerializeField, Rename("Curve FallFlip Speed")] public float fallFlipRotationCurveSpeed;
        [SerializeField] public float minFlipAngle; //if the difference between the angle 360 and the angle of the Axe before the jump is less than a this Value, then 360 will be added to the angle of rotation, otherwise the angle of rotation remains unchanged 
        [SerializeField] public float flipAngle;

        [Space]
        [SerializeField, Rename("Fall Max Angle")] public float fallRotationMaxAngle;
        [SerializeField, Rename("Curve Speed")] public float fallRotationCurveSpeed;

        public AnimationCurve Curve => rotationCurve;

        float IAxeFallRotateSystemData.MaxAngle => fallRotationMaxAngle;
        float IAxeFallRotateSystemData.CurveSpeed => fallRotationCurveSpeed;
        
        float IAxeJumpRotateSystemData.MinFlipAngle => minFlipAngle;
        float IAxeJumpRotateSystemData.CurveSpeedJump => jumpRotationCurveSpeed;
        float IAxeJumpRotateSystemData.CurveSpeedFallFlip => fallFlipRotationCurveSpeed;
        float IAxeJumpRotateSystemData.FlipAngle => flipAngle;
    }
}