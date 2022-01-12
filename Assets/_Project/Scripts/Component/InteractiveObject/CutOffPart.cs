using UnityEngine;

namespace ChopChop
{
    //TODO: Implement CutOffParts utilization
    public abstract class CutOffPart : MonoBehaviour
    {
        private void Awake()
        {
            AddDefaultComponents();
            AddSpecificComponents();
        }

        protected virtual void AddDefaultComponents()
        {
            this.gameObject.AddComponent<ObjectDestroyer>();
        }

        protected abstract void AddSpecificComponents();
    }

    public class CutOffPartStatic : CutOffPart
    {
        protected override void AddSpecificComponents()
        { }
    }

    public class CutOffPartDynamic : CutOffPart
    {
        public void AddForce(Vector3 force)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }

        protected override void AddSpecificComponents()
        {
            this.gameObject.AddComponent<BoxCollider>();
            this.gameObject.AddComponent<Rigidbody>();
        }
    }
}