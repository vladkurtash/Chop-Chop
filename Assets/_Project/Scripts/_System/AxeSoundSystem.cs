using UnityEngine;

namespace ChopChop
{
    public class AxeSoundSystem : IAxeSoundSystem
    {
        private readonly AudioClip[] _clips = null;
        private readonly IMove _moveData = null;
        public AxeSoundSystem(AudioClip[] clips, IMove moveData)
        {
            _clips = clips;
            _moveData = moveData;

            if (_clips.Length < 1)
            {
                Debug.LogError($"{typeof(AxeSoundSystem)}: audioClips Length is less than one.");
                return;
            }
        }

        public void PlayFlipSound()
        {
            int randomIndex = Random.Range(0, _clips.Length);
            AudioPlayer.Instance.PlayClipAtPoint(_clips[randomIndex], _moveData.Position);
        }
    }

    public interface IAxeSoundSystem
    {
        void PlayFlipSound();
    }
}