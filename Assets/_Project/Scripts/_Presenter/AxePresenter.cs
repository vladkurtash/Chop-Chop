using System;
using UnityEngine;

namespace ChopChop
{
    public class AxePresenter : IAxePresenter
    {
        private IAxeModel _model;
        private IAxeRootView _view;
        private IAxeMoveSystem _moveSystem;
        private IAxeRotateSystem _rotateSystem;
        private IColorBlinkSystem _colorBlinkSystem;
        private IAxeSoundSystem _soundSystem;

        public event Action Disabling;
        public event Action Hit;

        public AxePresenter(IAxeModel model, IAxeRootView view, IAxeMoveSystem moveSystem, IAxeRotateSystem rotateSystem, IColorBlinkSystem colorBlinkSystem, IAxeSoundSystem soundSystem)
        {
            _model = model;
            _view = view;
            _moveSystem = moveSystem;
            _rotateSystem = rotateSystem;
            _colorBlinkSystem = colorBlinkSystem;
            _soundSystem = soundSystem;

            Setup();
        }

        public void Setup()
        {
            _colorBlinkSystem.Setup(_view.BladeView.Renderer, _view.BackView.Renderer);
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            _view.BladeView.OnTriggerEnterEvent += OnBladeTriggerEnterReact;
            _view.BladeView.OnRaycastHitEvent += OnBladeRaycastHitReact;
            _view.BackView.OnTriggerEnterEvent += OnBackTriggerEnterReact;

            _model.Moved += OnMoved;
            _model.Rotated += OnRotated;
        }

        public void OnBladeTriggerEnterReact(Collider otherCollider)
        {
            int layer = otherCollider.gameObject.layer;

            switch (layer)
            {
                case (int)Element.Death:
                    Death();
                    break;
                case (int)Element.Static:
                    StopMovementSystems();
                    HitObject(otherCollider);
                    break;
                case (int)Element.Slicable:
                    Slice(otherCollider);
                    break;
                case (int)Element.ScoreMultiplier:
                    OnBladeHitScoreMultiplier(otherCollider);
                    break;
            }
        }

        private void OnBladeHitScoreMultiplier(Collider otherCollider)
        {
            HitObject(otherCollider);
            Disable();
        }

        public void OnBladeRaycastHitReact(RaycastHit raycastHit)
        {
            int layer = raycastHit.collider.gameObject.layer;
            if (layer == (int)Element.ScoreMultiplier)
            {
                HitObject(raycastHit.collider);
                Disable();
                return;
            }
        }

        private void StopMovementSystems()
        {
            _moveSystem.Stop();
            _rotateSystem.Stop();
        }

        private void Slice(Collider otherCollider)
        {
            ISliceable slicable = otherCollider.GetComponent<ISliceable>();
            slicable?.Slice(_view.BladeView.Transform.position, _view.BladeView.Transform.up);
            Hit?.Invoke();
        }

        private void HitObject(Collider otherCollider)
        {
            otherCollider.GetComponent<IHittable>()?.Hit();
            Hit?.Invoke();
        }

        public void OnBackTriggerEnterReact(Collider otherCollider)
        {
            int layer = otherCollider.gameObject.layer;

            switch (layer)
            {
                case (int)Element.Death:
                    Death(); break;
                case (int)Element.Static:
                case (int)Element.Slicable:
                    //case (int)Element.ScoreMultiplier:
                    SpringBack(); break;
            }
        }

        private void SpringBack()
        {
            _moveSystem.SpringBack();
            _rotateSystem.SpringBack();

            _colorBlinkSystem.Blink();

            _soundSystem.PlayBackHitSound();
        }

        public void OnMoved()
        {
            _view.SetPosition(_model.Position);
        }

        public void OnRotated()
        {
            _view.SetRotation(_model.Rotataion);
        }

        public void Death()
        {
            Disable();
            _view.Disable();

            StopMovementSystems();
        }

        private void Disable()
        {
            Disabling.Invoke();
            UnRegisterEvents();
            StopMovementSystems();
        }

        private void UnRegisterEvents()
        {
            _view.BladeView.OnTriggerEnterEvent -= OnBladeTriggerEnterReact;
            _view.BladeView.OnRaycastHitEvent -= OnBladeRaycastHitReact;
            _view.BackView.OnTriggerEnterEvent -= OnBackTriggerEnterReact;
            _model.Moved -= OnMoved;
            _model.Rotated -= OnRotated;
        }

        private void Destroy()
        {
            Disable();
        }

        public void Dispose()
        {
            Destroy();
        }
    }
}