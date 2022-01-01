namespace ChopChop
{
    public interface ISystem
    {
        //void Pause();
        //void Unpause();
        void Stop();
        void UpdateLocal(float deltaTime);
    }

    public interface IMoveSystem : ISystem
    { }

    public interface IRotateSystem : ISystem
    { }

    public interface IAxeMoveSystem : IMoveSystem
    {
        void Jump();
        void SpringBack();
    }

    public interface IAxeRotateSystem : IRotateSystem
    {
        void Jump();
        void SpringBack();
    }
}