using System;
using EzySlice;
using UnityEngine;

namespace ChopChop
{
    // todo Make base class for inheritance 
    public class SliceableObject : MonoBehaviour, ISliceable
    {
        [SerializeField] private AddForceSide addForceSide;

        protected enum AddForceSide
        {
            None,
            Both,
            Left,
            Right
        }

        private enum CutOffSide
        {
            None,
            Left,
            Right
        }

        public virtual void Slice(Vector3 position, Vector3 direction)
        {
            DoSlice(position, direction);
            this.gameObject.SetActive(false);
        }

        protected virtual void DoSlice(Vector3 position, Vector3 direction)
        {
            Material crossSectionMaterial = GetCrossSectionMaterial();
            SlicedHull slicedHull = Slice(position, direction, crossSectionMaterial);

            SetupLeftPart(slicedHull, crossSectionMaterial);
            SetupRightPart(slicedHull, crossSectionMaterial);
        }

        protected virtual Material GetCrossSectionMaterial()
        {
            return CrossSectionMaterialProvider.GetMaterial();
        }

        protected virtual SlicedHull Slice(Vector3 position, Vector3 direction, Material crossSectionMaterial)
        {
            return this.gameObject.Slice(position, direction, crossSectionMaterial);
        }

        protected virtual void SetupLeftPart(SlicedHull slicedHull, Material crossSectionMaterial)
        {
            GameObject part = slicedHull.CreateLowerHull(this.gameObject, crossSectionMaterial);
            part.AddComponent<CutOffPart>();
        }

        protected virtual void SetupRightPart(SlicedHull slicedHull, Material crossSectionMaterial)
        {
            GameObject part = slicedHull.CreateUpperHull(this.gameObject, crossSectionMaterial);
            part.AddComponent<CutOffPart>();
        }

        protected virtual void AddForceToPart(CutOffPartDynamic cutOffPart, Vector3 force)
        {
            cutOffPart.AddForce(force);
        }

        protected virtual void OnDisable()
        {
            Destroy();
        }

        private void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}