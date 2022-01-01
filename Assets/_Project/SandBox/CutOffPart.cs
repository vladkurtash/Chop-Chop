using UnityEngine;

namespace ChopChop
{
    public class CutOffPart : MonoBehaviour
    {
        private void Awake()
        {
            AddDefaultComponents();
        }

        protected virtual void AddDefaultComponents()
        {
            this.gameObject.AddComponent<BoxCollider>();
        }
    }

    public class CutOffPartStatic : CutOffPart
    {
        protected override void AddDefaultComponents()
        { }
    }

    public class CutOffPartDynamic : CutOffPart
    {
        protected override void AddDefaultComponents()
        {
            this.gameObject.AddComponent<BoxCollider>();
            this.gameObject.AddComponent<Rigidbody>();
        }

        public void AddForce(Vector3 force)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}