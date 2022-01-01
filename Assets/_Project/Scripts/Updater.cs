using System;
using System.Collections.Generic;

namespace ChopChop
{
    public class Updater : IUpdatable
    {
        private List<IUpdatable> _updatables = new List<IUpdatable>();

        public event Action<IUpdatable> Destroying;

        public void AddUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
            //updatable.Destroying += DeleteUpdatable;
        }

        private void DeleteUpdatable(IUpdatable updatable)
        {
            if (!_updatables.Contains(updatable)) 
                return;

            _updatables.Remove(updatable);
        }

        public void UpdateLocal(float deltaTime)
        {
            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i]?.UpdateLocal(deltaTime);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}