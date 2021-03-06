using UnityEngine;

namespace ChopChop.Component
{
    public class ColorBlinker : MonoBehaviour
    {
        [SerializeField] private Color32 color32;
        [SerializeField] private float rate;
        [SerializeField] private int blinkCount;
        [SerializeField] private ColorBlinkSystem.Type type = ColorBlinkSystem.Type.Infinity;
        private ColorBlinkSystem _colorBlinkSystem = null;

        private void Awake()
        {
            if (type == ColorBlinkSystem.Type.Infinity)
                _colorBlinkSystem = new ColorBlinkSystem(color32, rate);
            else if (type == ColorBlinkSystem.Type.LoopCount)
                _colorBlinkSystem = new ColorBlinkSystem(color32, rate, blinkCount);

            _colorBlinkSystem.Setup(this.gameObject.GetComponent<Renderer>());
        }

        private void Update()
        {
            _colorBlinkSystem.UpdateLocal(Time.deltaTime);
        }

        public void Perform()
        {
            _colorBlinkSystem.Blink();
        }
    }
}