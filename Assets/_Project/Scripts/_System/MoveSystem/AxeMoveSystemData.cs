using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "AxeMoveSystemData", menuName = "SystemData/AxeMoveSystemData")]
    public class AxeMoveSystemData : ScriptableObject, IAxeMoveSystemData
    {
        [SerializeField, Rename("Curve")] public AnimationCurve curve;
        [SerializeField, Rename("Curve Speed")] public float curveSpeed;
        [SerializeField] public float heightMax;
        [SerializeField] public float distanceMax;

        [Space]
        [SerializeField] public float fallSpeedMax;

        AnimationCurve IAxeMoveSystemData.Curve => curve;
        float IAxeMoveSystemData.CurveSpeed => curveSpeed;
        float IAxeMoveSystemData.HeightMax => heightMax;
        float IAxeMoveSystemData.DistanceMax => distanceMax;
        float IAxeMoveSystemData.FallSpeedMax => fallSpeedMax;
    }
}