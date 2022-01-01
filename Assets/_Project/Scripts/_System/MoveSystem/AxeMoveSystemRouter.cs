namespace ChopChop
{
    public class AxeMoveSystemRouter : IAxeMoveSystem
    {
        private readonly IMove _moveData = null;
        private readonly AxeMoveSystem _moveSystem = null;

        public AxeMoveSystemRouter(IMove moveData, AxeMoveSystemData systemData)
        {
            _moveData = moveData;
            _moveSystem = new AxeMoveSystem(systemData);
        }

        public void Stop()
        {
            _moveSystem.SetState(AxeMoveSystem.SystemState.None);
        }

        public void Jump()
        {
            _moveSystem.SetState(AxeMoveSystem.SystemState.Jump);
        }

        public void SpringBack()
        {
            _moveSystem.SetState(AxeMoveSystem.SystemState.SpringBack);
        }

        public void UpdateLocal(float deltaTime)
        {
            _moveSystem.UpdateLocal(deltaTime);
            _moveData.AddPosition(_moveSystem.PositionDelta);
        }
    }
}