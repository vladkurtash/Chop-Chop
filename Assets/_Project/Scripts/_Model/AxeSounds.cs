using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "AxeSounds", menuName = "SingletonScriptableObject/AxeSounds")]
    public class AxeSounds : SingletonScriptableObject<AxeSounds>
    {
        [SerializeField] public AudioClip[] flip;
        [SerializeField] public AudioClip[] backHit;
    }
}