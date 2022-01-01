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

        protected override void Setup()
        {
            GameObject rootGameObject = this.gameObject;
            _bladeView = rootGameObject.GetComponentInChildren<AxeBladeView>();
            _backView = rootGameObject.GetComponentInChildren<AxeBackView>();
        }
    }
}