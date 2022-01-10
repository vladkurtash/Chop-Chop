using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "SingletonScriptableObject/AudioConfig")]
    public class AudioConfig : SingletonScriptableObject<AudioConfig>
    {
        [SerializeField] public float defaultHitSoundPitch = 2.0f;
        [SerializeField] public float maxHitSoundPitch = 4.0f;
        [SerializeField] public int maxHitSeries = 15;
        [SerializeField] public float hitSeriesDuration = 0.25f;
    }
}