using UnityEngine;

namespace ChopChop
{
    public class AudioHitSeries
    {
        private readonly float _maxPitch = 0.0f;
        private readonly float _minPitch = 0.0f;
        private readonly HitSeries _hitSeries = null;

        public AudioHitSeries(float minPitch, float maxPitch, HitSeries hitSeries)
        {
            _hitSeries = hitSeries;
            _minPitch = minPitch;
            _maxPitch = maxPitch;
        }

        public float Pitch => Mathf.Lerp(_minPitch, _maxPitch, _hitSeries.Percentage);
    }
}