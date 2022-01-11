using UnityEngine;

namespace ChopChop
{
    public class AxeSoundSystem : IAxeSoundSystem
    {
        private readonly AudioClip[] _flipClips = null;
        private readonly AudioClip[] _backHitClips = null;
        private readonly IMove _moveData = null;
        public AxeSoundSystem(AudioClip[] flipClips, AudioClip[] backHitClips, IMove moveData)
        {
            _flipClips = flipClips;
            _backHitClips = backHitClips;
            _moveData = moveData;

            if (_flipClips.Length < 1)
            {
                Debug.LogError($"{typeof(AxeSoundSystem)}: audioClips Length is less than one.");
                return;
            }
        }

        public void PlayFlipSound()
        {
            PlayRandomClip(_flipClips);
        }

        public void PlayBackHitSound()
        {
            PlayRandomClip(_backHitClips);
        }

        private void PlayRandomClip(AudioClip[] clips)
        {
            int randomIndex = Random.Range(0, clips.Length);
            AudioPlayer.Instance.PlayClipAtPoint(clips[randomIndex], _moveData.Position);
        }
    }

    public interface IAxeSoundSystem
    {
        void PlayFlipSound();
        void PlayBackHitSound();
    }
}