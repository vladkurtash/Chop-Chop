using System.Collections.Generic;
using UnityEngine;

namespace ChopChop
{
    public class AxeRotationVariable : ObservableVariable<Vector3>
    {
        public AxeRotationVariable(Vector3 value) : base(value)
        { }

        new public Vector3 Value
        {
            get => _value;
            set
            {
                float angleX = Utility.Math.Clamp(value.x, 360.0f);
                _value = new Vector3(angleX, value.y, value.z);
                OnValueChanged();
            }
        }
    }
}