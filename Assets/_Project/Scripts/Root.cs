using UnityEngine;
using ChopChop.GUI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.EventSystems;

namespace ChopChop
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private AxeRootView axeView;
        [SerializeField] private AxeMoveSystemData axeMoveSystemData;
        [SerializeField] private AxeRotateSystemData axeRotateSystemData;
        [SerializeField] private CameraPresenter cameraPresenter;

        [SerializeField] private MainMenuWindowPresenter mainMenuWindow;
        [SerializeField] private InGameWindowPresenter inGameWindow;
        [SerializeField] private GameCompleteWindowPresenter gameCompleteWindow;
        [SerializeField] private GameOverWindowPresenter gameOverWindow;

        [SerializeField] private OnScreenButton axeJumpArea;
        
        private AxePresenter _axePresenter = null;
        private AxeInputRouter _axeInputRouter = null;
        private Timers _timers = null;
        private HitSeries _hitSeries = null;

        private Updater _updater = null;

        private void Start()
        {
            Setup();
            mainMenuWindow.Show(OnStartClick);
        }

        private void OnStartClick()
        {
            inGameWindow.Show(LoadNextLevel);
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

            _hitSeries = new HitSeries(AudioConfig.Instance.maxHitSeries, _timers);
            AudioHitSeries audioHitSeries = new AudioHitSeries(AudioConfig.Instance.defaultHitSoundPitch, AudioConfig.Instance.maxHitSoundPitch, _hitSeries);
            AudioPlayer.Instance.Setup(audioHitSeries);

            AxeSoundSystem axeSoundSystem = new AxeSoundSystem(AxeSounds.Instance.flip, AxeSounds.Instance.backHit, model);

            SetupAxePresenter(model, moveSystem, rotateSystem, axeSoundSystem);
            SetupAxeInput(moveSystem, rotateSystem, axeSoundSystem);

            DoOnEnable();
        }

        private void DoOnEnable()
        {
            _axeInputRouter.OnEnable();
            _axePresenter.Disabling += OnAxeDisable;
            _axePresenter.Destroying += OnAxeDestroying;
            _axePresenter.Hit += OnObjectHit;
        }

        private void OnDisable()
        {
            _axeInputRouter.OnDisable();
            _axePresenter.Disabling -= OnAxeDisable;
            _axePresenter.Destroying -= OnAxeDestroying;
            _axePresenter.Hit -= OnObjectHit;
        }

        public void OnAxeDisable()
        {
            _axeInputRouter.OnDisable();
        }

        public void OnAxeDestroying()
        {
            gameOverWindow.Show(LoadNextLevel);
            _axeInputRouter.OnDisable();
            cameraPresenter.SetState(CameraPresenter.State.None);
        }

        public void OnLevelComplete()
        {
            gameCompleteWindow.Show(LoadNextLevel);
            cameraPresenter.SetState(CameraPresenter.State.Align);
        }

        private void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void SetDefaultFrameRate()
        {
            Application.targetFrameRate = Config.Instance.defaultFrameRate;
        }

        private void SetupAxePresenter(IAxeModel model, IAxeMoveSystem moveSystem, IAxeRotateSystem rotateSystem, AxeSoundSystem soundSystem)
        {
            ColorBlinkSystem colorBlinkSystem = new ColorBlinkSystem(AxeBlinkingConfig.Instance.targetColor, AxeBlinkingConfig.Instance.blinkingRate, AxeBlinkingConfig.Instance.blinkCount);
            _updater.AddUpdatable(colorBlinkSystem);
            _axePresenter = new AxePresenter(model, axeView, moveSystem, rotateSystem, colorBlinkSystem, soundSystem);
        }

        private void OnObjectHit()
        {
            _hitSeries.IncreaseBy1();
        }

        private void SetupAxeInput(IAxeMoveSystem moveSystem, IAxeRotateSystem rotateSystem, IAxeSoundSystem soundSystem)
        {
            _axeInputRouter = new AxeInputRouter(moveSystem, rotateSystem, _timers, soundSystem);
        }

        private void Update()
        {
            _axeInputRouter.UpdateLocal(Time.deltaTime);
            _timers.UpdateLocal(Time.deltaTime);
            _updater.UpdateLocal(Time.deltaTime);
        }
    }
}