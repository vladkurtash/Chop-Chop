using UnityEngine;

namespace ChopChop
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private AxeRootView axeView;
        [SerializeField] private AxeMoveSystemData axeMoveSystemData;
        [SerializeField] private AxeRotateSystemData axeRotateSystemData;
        private AxePresenter _axePresenter = null;
        private AxeInputRouter _axeInputRouter = null;
        private Timers _timers = null;

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            CrossSectionMaterialProvider.Setup();

            SetDefaultFrameRate();

            _timers = new Timers();

            float axeAngleX = Utility.Math.GetAngle0360(Vector3.up, axeView.transform.up, Vector3.right);
            AxeModel model = new AxeModel(axeView.transform.position, new Vector3(axeAngleX, 0, 0));

            AxeMoveSystemRouter moveSystem = new AxeMoveSystemRouter(model, axeMoveSystemData);
            AxeRotateSystemRouter rotateSystem = new AxeRotateSystemRouter(model, axeRotateSystemData);

            SetupAxePresenter(model, moveSystem, rotateSystem);
            SetupAxeInput(moveSystem, rotateSystem);

            DoOnEnable();
        }

        private void DoOnEnable()
        {
            _axeInputRouter.OnEnable();
            _axePresenter.Disabling += OnAxeDestroy;
        }

        private void OnDisable()
        {
            _axeInputRouter.OnDisable();
            _axePresenter.Disabling -= OnAxeDestroy;
        }

        public void OnAxeDestroy()
        {
            _axeInputRouter.OnDisable();
            //GUI stuff
            //Camera stuff (lock)
        }

        private void SetDefaultFrameRate()
        {
            Application.targetFrameRate = Config.Instance.defaultFrameRate;
        }

        private void SetupAxePresenter(IAxeModel model, IAxeMoveSystem moveSystem, IAxeRotateSystem rotateSystem)
        {
            _axePresenter = new AxePresenter(model, axeView, moveSystem, rotateSystem);
        }

        private void SetupAxeInput(IAxeMoveSystem moveSystem, IAxeRotateSystem rotateSystem)
        {
            _axeInputRouter = new AxeInputRouter(moveSystem, rotateSystem, _timers);
        }

        private void Update()
        {
            _axeInputRouter.UpdateLocal(Time.deltaTime);
            _timers.UpdateLocal(Time.deltaTime);
        }
    }
}