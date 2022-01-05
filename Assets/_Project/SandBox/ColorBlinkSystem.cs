using UnityEngine;
using System.Collections;

namespace ChopChop
{
    public class ColorBlinkSystem : IUpdatable
    {
        private bool _blinking = false;
        MaterialPropertyBlock _materialPropertyBlock = null;

        private Renderer[] _renderers = null;
        private int _blinkCount = 1;
        private float _rate = 0.3f;
        private Color32 _colorStart = new Color32(0, 0, 0, 255);
        private Color32 _colorEnd = new Color32(0, 0, 0, 255);
        float _totalDeltaTime = 0.0f;

        public void Setup(params Renderer[] renderers)
        {
            _renderers = renderers;
            _materialPropertyBlock = new MaterialPropertyBlock();
            _colorEnd = Config.Instance.colorEnd;
            _rate = Config.Instance.blinkingRate;
            _blinkCount = Config.Instance.blinkCount;

            EnableMaterialsEmission();
        }
        
        /// <summary>
        /// Need to be enabled to display color changes 
        /// </summary>
        private void EnableMaterialsEmission()
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.EnableKeyword("_EMISSION");
            }
        }

        public void Blink()
        {
            _totalDeltaTime = 0.0f;
            SetSystemState();
        }

        public void UpdateLocal(float deltaTime)
        {
            if (!_blinking)
                return;

            DoBlinking(deltaTime);
            SetSystemState();
        }

        private void DoBlinking(float deltaTime)
        {
            _totalDeltaTime += deltaTime / _rate;
            //Need to multiply by 2 because one blink is two cycles from 0 to 1 (from 0 to 1 & from 1 to 2)
            _totalDeltaTime = Mathf.Clamp(_totalDeltaTime, 0.0f, _blinkCount * 2);

            SmoothTransition(_colorStart, _colorEnd, _totalDeltaTime);
        }

        private void SetSystemState()
        {
            //Need to multiply by 2 because one blink is two cycles from 0 to 1 (from 0 to 1 & from 1 to 2)
            if ((_totalDeltaTime) > _blinkCount * 2)
            {
                _blinking = false;
                return;
            }

            _blinking = true;
        }

        private void SmoothTransition(Color32 from, Color32 to, float value)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].GetPropertyBlock(_materialPropertyBlock);
                _materialPropertyBlock.SetColor("_EmissionColor", Color32.Lerp(from, to, Mathf.PingPong(value, 1.0f)));
                _renderers[i].SetPropertyBlock(_materialPropertyBlock);
            }
        }
    }
}