namespace ChopChop
{
    public abstract class ARootView : AView
    {
        protected virtual void Awake()
        {
            Setup();
        }

        protected abstract void Setup();
    }
}