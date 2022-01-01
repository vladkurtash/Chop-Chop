using System;
using System.Collections.Generic;
using System.Linq;

namespace ChopChop
{
    public interface ITickable
    {

    }

    public class Timers : IUpdatable
    {
        private List<Timer> _timers = new List<Timer>();

        public void Start(ITickable context, float time, Action onEnd)
        {
            _timers.Add(new Timer(context, time, onEnd));
        }

        public void StopAll(ITickable context)
        {
            _timers.RemoveAll(timer => timer.Context.Equals(context));
        }

        public void UpdateLocal(float deltaTime)
        {
            foreach (var timer in _timers.ToList())
            {
                timer.AccumulatedTime += deltaTime;

                if (timer.IsEnd)
                {
                    _timers.Remove(timer);
                    timer.OnEnd.Invoke();
                }
            }
        }

        public float GetAccumulatedTime(ITickable context)
        {
            return _timers.First(timer => timer.Context.Equals(context)).AccumulatedTime;
        }

        private class Timer
        {
            public float AccumulatedTime;
            public readonly float Time;
            public readonly ITickable Context;
            public readonly Action OnEnd;

            public bool IsEnd => Time <= AccumulatedTime;

            public Timer(ITickable context, float time, Action onEnd)
            {
                Time = time;
                Context = context;
                OnEnd = onEnd;
            }
        }
    }
}
