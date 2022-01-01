namespace ChopChop
{
    public class AxeRotateSystemRouter : IAxeRotateSystem
    {
        private IRotate _rotateData;
        private AxeRotateSystem _system;

        public AxeRotateSystemRouter(IRotate rotateData, AxeRotateSystemData systemData)
        {
            _rotateData = rotateData;
            _system = new AxeRotateSystem(rotateData, systemData);
        }

        public void Stop()
        {
            _system.SetState(AxeRotateSystem.SystemState.None);
        }

        public void UpdateLocal(float deltaTime)
        {
            _system.UpdateLocal(deltaTime);
            _rotateData.AddRotation(_system.RotationDelta);
        }

        public void SpringBack()
        {
            _system.SetState(AxeRotateSystem.SystemState.SpringBack);
        }

        public void Jump()
        {
            _system.SetState(AxeRotateSystem.SystemState.Jump);
        }
    }
}