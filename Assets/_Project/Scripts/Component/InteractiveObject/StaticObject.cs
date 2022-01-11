using UnityEngine;

namespace ChopChop
{
    public class StaticObject : SoundableObject, IHittable
    {
        private void Awake()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        public void Hit()
        {
            MakeSound();
            Raise();
        }
    }
}