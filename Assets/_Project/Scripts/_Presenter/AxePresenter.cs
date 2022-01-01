using System;
using UnityEngine;

namespace ChopChop
{
    public class AxePresenter : IAxePresenter
    {
        private IAxeModel _model;
        private IAxeView _view;
        private IAxeMoveSystem _moveSystem;
        private IAxeRotateSystem _rotateSystem;

        public AxePresenter(IAxeModel model, IAxeView view, IAxeMoveSystem moveSystem, IAxeRotateSystem rotateSystem)
        {
            _model = model;
            _view = view;
            _moveSystem = moveSystem;
            _rotateSystem = rotateSystem;

            Setup();
        }

        public void Setup()
        {
            _view.BladeView.OnTriggerEnterEvent += OnBladeTriggerEnterReact;
            _view.BackView.OnTriggerEnterEvent += OnBackTriggerEnterReact;

            _model.Moved += OnMoved;
            _model.Rotated += OnRotated;
        }

        public void OnBladeTriggerEnterReact(Collider otherCollider)
        {
            int layer = otherCollider.gameObject.layer;

            switch (layer)
            {
                //case (int)Element.Death: /* GameOver */ break;
                    
                case (int)Element.Static: Stuck(); break;
                    
                case (int)Element.Slicable: Slice(otherCollider); break;
                    
                // case (int)Element.ScoreMultiplier: /*  CompleteLevel and multiply score */ break;
            }
        }

        private void Stuck()
        {
            _moveSystem.Stop();
            _rotateSystem.Stop();
        }

        private void Slice(Collider otherCollider)
        {
            ISliceable slicable = otherCollider.GetComponent<ISliceable>();
            slicable?.Slice(_view.BladeView.Transform.position, _view.BladeView.Transform.up);
        }

        public void OnBackTriggerEnterReact(Collider otherCollider)
        {
            int layer = otherCollider.gameObject.layer;

            switch (layer)
            {
                //case (int)Element.Death: /* GameOver */ break;
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
        }

        public void OnMoved()
        {
            _view.SetPosition(_model.Position);
        }

        public void OnRotated()
        {
            _view.SetRotation(_model.Rotataion);
        }

        public void OnDeath()
        {
            //add and enable rigidbody on blande and on handle
        }

        private void Destroy()
        {
            _view.BladeView.OnTriggerEnterEvent -= OnBladeTriggerEnterReact;
            _view.BackView.OnTriggerEnterEvent -= OnBackTriggerEnterReact;
        }

        public void Dispose()
        {
            Destroy();
        }
    }
}