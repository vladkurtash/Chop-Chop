using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "SliceEffect", menuName = "SingletonScriptableObject/SliceEffect")]
    public class SliceEffect : SingletonScriptableObject<SliceEffect>
    {
        public GameObject effect;
    }
}