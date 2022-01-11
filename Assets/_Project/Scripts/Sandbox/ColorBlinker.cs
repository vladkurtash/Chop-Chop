using UnityEngine;

namespace ChopChop
{
    public class ColorBlinker : MonoBehaviour
    {
        [SerializeField] private Color32 color32;
        [SerializeField] private float rate;
        [SerializeField] private int blinkCount;
        private ColorBlinkSystem _colorBlinkSystem = null;

        private void Awake()
        {
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