using System;
using EzySlice;
using UnityEngine;

namespace ChopChop
{
    public class SliceableObject : InteractiveObject, ISliceable
    {
        [SerializeField] private DynamicPart dynamicPart = DynamicPart.Right;

        protected enum DynamicPart
        {
            Both,
            Right
        }

        public virtual void Slice(Vector3 position, Vector3 direction)
        {
            DoSlice(position, direction);
            MakeSound();
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

            if (LeftPartDynamic())
            {
                CutOffPartDynamic cutOffPartDynamic = part.AddComponent<CutOffPartDynamic>();
                AddForceToPart(cutOffPartDynamic, -Config.Instance.cutOffPartDefaultImpulseForce);
                return;
            }

            part.AddComponent<CutOffPartStatic>();
        }

        protected virtual void SetupRightPart(SlicedHull slicedHull, Material crossSectionMaterial)
        {
            GameObject part = slicedHull.CreateUpperHull(this.gameObject, crossSectionMaterial);

            if (DynamicPartRight())
            {
                CutOffPartDynamic cutOffPartDynamic = part.AddComponent<CutOffPartDynamic>();
                AddForceToPart(cutOffPartDynamic, Config.Instance.cutOffPartDefaultImpulseForce);
                return;
            }

            part.AddComponent<CutOffPartStatic>();
        }

        private bool LeftPartDynamic()
        {
            return dynamicPart == DynamicPart.Both;
        }

        private bool DynamicPartRight()
        {
            return dynamicPart == DynamicPart.Both || dynamicPart == DynamicPart.Right;
        }

        protected virtual void AddForceToPart(CutOffPartDynamic cutOffPart, Vector3 force)
        {
            cutOffPart.AddForce(force);
        }

        protected virtual void OnDisable()
        {
            SpawnSliceEffect();
            Destroy();
        }

        private GameObject SpawnSliceEffect()
        {
            //This returned false both when quitting the app/editor and when unloading the scene
            if (!this.gameObject.scene.isLoaded)
                return null;

            return Instantiate(SliceEffect.Instance.effect, transform.position, Quaternion.identity);
        }

        private void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}