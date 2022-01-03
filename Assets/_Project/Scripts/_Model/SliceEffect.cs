using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "SliceEffect", menuName = "SliceEffect")]
    public class SliceEffect : SingletonScriptableObject<SliceEffect>
    {
        public GameObject effect;
    }
}