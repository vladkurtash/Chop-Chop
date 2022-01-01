using System;
using UnityEngine;

namespace ChopChop
{
    public class ObservableVariable<T>
    {
        protected T _value;

        public ObservableVariable(T value)
        {
            _value = value;
        }

        public event Action ValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke();
            }
        }

        protected void OnValueChanged()
        {
            ValueChanged?.Invoke();
        }
    }
}