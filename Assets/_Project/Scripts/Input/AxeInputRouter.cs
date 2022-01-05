using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChopChop
{
    public class AxeInputRouter : IUpdatable, ITickable
    {
        private readonly AxeInput _input = null;
        private readonly IAxeMoveSystem _moveSystem = null;
        private readonly IAxeRotateSystem _rotateSystem = null;
        private readonly Timers _timers = null;
        private bool _coolDown = true;
        private bool _disabled = true;

        public AxeInputRouter(IAxeMoveSystem moveSystem, IAxeRotateSystem rotateSystem, Timers timers)
        {
            _input = new AxeInput();
            _moveSystem = moveSystem;
            _rotateSystem = rotateSystem;
            _timers = timers;

            _coolDown = false;
        }

        public void OnEnable()
        {
            _input.Enable();
            _input.Axe.Jump.performed += OnJump;
        }

        public void OnDisable()
        {
            _input.Disable();
            _input.Axe.Jump.performed -= OnJump;
            _disabled = false;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (_coolDown)
                return;

            _moveSystem.Jump();
            _rotateSystem.Jump();

            StartCoolDown();
        }

        private void StartCoolDown()
        {
            _coolDown = true;
            _timers.Start(this, Config.Instance.playerInputCoolDown, () => _coolDown = false);
        }

        public void UpdateLocal(float deltaTime)
        {
            if (!_disabled)
                return;
                
            _rotateSystem.UpdateLocal(deltaTime);
            _moveSystem.UpdateLocal(deltaTime);
        }
    }
}