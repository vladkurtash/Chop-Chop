using EzySlice;
using UnityEngine;

namespace ChopChop
{
    public class SliceableObjectRightPartFall : SliceableObject
    {
        sealed protected override void SetupRightPart(SlicedHull slicedHull, Material crossSectionMaterial)
        {
            GameObject part = slicedHull.CreateUpperHull(this.gameObject, crossSectionMaterial);

            part.AddComponent<CutOffPartDynamic>();
            CutOffPartDynamic cutOffPart = part.GetComponent<CutOffPartDynamic>();
            AddForceToPart(cutOffPart, Config.Instance.cutOffPartImpulseForce);
        }
    }
}