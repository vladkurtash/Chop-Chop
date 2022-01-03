using UnityEngine;

namespace ChopChop
{
    public interface ISliceable
    {
        void Slice(Vector3 position, Vector3 direction);
    }
}