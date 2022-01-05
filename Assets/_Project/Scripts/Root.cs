using UnityEngine;

namespace ChopChop
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private AxeRootView axeView;
        [SerializeField] private AxeMoveSystemData axeMoveSystemData;
        [SerializeField] private AxeRotateSystemData axeRotateSystemData;
        [SerializeField] private CameraFollow cameraFollow;
        private AxePresenter _axePresenter = null;
        private AxeInputRouter _axeInputRouter = null;
        private Timers _timers = null;

        private Updater _updater = null;

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            _updater = new Updater();

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
            _axePresenter.Disabling += OnAxeDisable;
        }

        private void OnDisable()
        {
            _axeInputRouter.OnDisable();
            _axePresenter.Disabling -= OnAxeDisable;
        }

        public void OnAxeDisable()
        {
            _axeInputRouter.OnDisable();
            //todo GUI stuff - GameOver Menu
            cameraFollow.Stop();
        }

        private void SetDefaultFrameRate()
        {
            Application.targetFrameRate = Config.Instance.defaultFrameRate;
        }

        private void SetupAxePresenter(IAxeModel model, IAxeMoveSystem moveSystem, IAxeRotateSystem rotateSystem)
        {
            ColorBlinkSystem colorBlinkSystem = new ColorBlinkSystem();
            _updater.AddUpdatable(colorBlinkSystem);
            _axePresenter = new AxePresenter(model, axeView, moveSystem, rotateSystem, colorBlinkSystem);
        }

        private void SetupAxeInput(IAxeMoveSystem moveSystem, IAxeRotateSystem rotateSystem)
        {
            _axeInputRouter = new AxeInputRouter(moveSystem, rotateSystem, _timers);
        }

        private void Update()
        {
            _axeInputRouter.UpdateLocal(Time.deltaTime);
            _timers.UpdateLocal(Time.deltaTime);
            _updater.UpdateLocal(Time.deltaTime);
        }
    }
}