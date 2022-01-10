using UnityEngine;

namespace ChopChop
{
    public class HitSeries : ITickable
    {
        private readonly int _maxSeries = 0;
        private int _series = 0;
        private readonly Timers _timers = null;

        public HitSeries(int maxSeries, Timers timers)
        {
            _maxSeries = maxSeries;
            _timers = timers;
        }

        /// <summary>
        /// Returns a percentage in the range [0 to 100] percent
        /// </summary>
        public float Percentage => (float)_series / (float)_maxSeries;

        public void Reset()
        {
            _series = 0;
        }

        public void IncreaseBy1()
        {
            _series = Mathf.Clamp(++_series, 0, _maxSeries);
            StartTimer();
        }
        
        private void StartTimer()
        {
            _timers.StopAll(this);
            _timers.Start(this, AudioConfig.Instance.hitSeriesDuration, () => Reset());
        }
    }
}