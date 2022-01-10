using UnityEngine;

namespace ChopChop
{
    public class AudioPlayer
    {
        private static readonly AudioPlayer _instance = new AudioPlayer();
        private AudioHitSeries _audioHitSeries = null;

        static AudioPlayer()
        { }

        private AudioPlayer()
        { }

        public static AudioPlayer Instance => _instance;

        public void Setup(AudioHitSeries audioHitSeries)
        {
            _audioHitSeries = audioHitSeries;
        }

        public void PlaySeriesClipAtPoint(AudioClip clip, Vector3 position, float volume = 1.0f)
        {
            PlayClipAtPoint(clip, position, _audioHitSeries.Pitch, volume);
        }

        public void PlayClipAtPoint(AudioClip clip, Vector3 position, float pitch = 1.0f, float volume = 1.0f)
        {
            PointClip.Create(clip, position, pitch, volume);
        }
    }
}