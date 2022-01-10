using UnityEngine;

namespace ChopChop
{
    public class StaticObject : InteractiveObject, IHittable
    {
        public void Hit()
        {
            MakeSound();
        }
    }
}