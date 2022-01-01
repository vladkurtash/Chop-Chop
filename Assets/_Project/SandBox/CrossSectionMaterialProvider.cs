using UnityEngine;
using System.Collections.Generic;

namespace ChopChop
{
    public static class CrossSectionMaterialProvider
    {
        private static Material[] _assetMaterials = null;
        private static IEnumerator<Material> _materials;

        private static IEnumerator<Material> GetMaterials()
        {
            if (_assetMaterials == null)
                yield break;

            for (int i = 0; i < _assetMaterials.Length; i++)
            {
                yield return _assetMaterials[i];

                if (i >= _assetMaterials.Length - 1)
                    i = -1;
            }
        }

        public static void Setup()
        {
            _assetMaterials = Resources.LoadAll<Material>("CrossSectionMaterials");
            if (_assetMaterials == null || _assetMaterials.Length <= 0)
            {
                Debug.LogWarning($"{typeof(CrossSectionMaterialProvider)} There is no Materials in the folder 'CrossSectionMaterials'");
                return;
            }

            _materials = GetMaterials();
        }

        public static Material GetMaterial()
        {
            _materials.MoveNext();

            return _materials.Current;
        }
    }
}