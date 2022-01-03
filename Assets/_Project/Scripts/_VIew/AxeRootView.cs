using UnityEngine;

namespace ChopChop
{
    [SelectionBase]
    public class AxeRootView : ARootView, IAxeView
    {
        [SerializeField] private AxeBladeView _bladeView;
        [SerializeField] private AxeBackView _backView;

        public IAxeBladeView BladeView => _bladeView;
        public IAxeBackView BackView => _backView;

        public void OnDeath()
        {
            AddComponenets();
        }

        private void AddComponenets()
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            AddForce(rigidbody, Config.Instance.axeDeathDefaultImpulseForce);
        }

        private void AddForce(Rigidbody rigidbody, Vector3 force)
        {
            rigidbody.AddForce(force);
        }

        protected override void Setup()
        {
            GameObject rootGameObject = this.gameObject;
            _bladeView = rootGameObject.GetComponentInChildren<AxeBladeView>();
            _backView = rootGameObject.GetComponentInChildren<AxeBackView>();
        }
    }
}