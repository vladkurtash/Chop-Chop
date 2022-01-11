using UnityEngine;

namespace ChopChop
{
    public class TransformScaler : MonoBehaviour
    {
        [SerializeField] private Vector3 scale;
        [SerializeField] private float rate;
        [SerializeField] private float scaleCount;
        private Vector3 _startScale = Vector3.zero;
        private float _totalDeltaTime = 0.0f;
        private bool _perfoming = false;

        private void Awake()
        {
            _startScale = gameObject.transform.localScale;
            _totalDeltaTime = 0.0f;
        }

        private void Update()
        {
            if (!_perfoming)
                return;

            _totalDeltaTime += Time.deltaTime / rate;

            DoScaling();
            SetSystemState();
        }

        private void DoScaling()
        {
            _totalDeltaTime = Mathf.Clamp(_totalDeltaTime, 0.0f, scaleCount * 2);

            gameObject.transform.localScale = Vector3.Lerp(_startScale, _startScale + scale, Mathf.PingPong(_totalDeltaTime, 1.0f));
        }

        public void Perform()
        {
            _perfoming = true;
        }

        private void SetSystemState()
        {
            //Need to multiply by 2 because one blink is two cycles from 0 to 1 (from 0 to 1 & from 1 to 2)
            if ((_totalDeltaTime) > scaleCount * 2)
            {
                _perfoming = false;
                return;
            }

            _perfoming = true;
        }
    }
}