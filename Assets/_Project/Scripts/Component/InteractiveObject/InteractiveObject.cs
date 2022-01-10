using UnityEngine;

namespace ChopChop
{
    public class InteractiveObject : MonoBehaviour, ISoundable
    {
        [SerializeField] private AudioClip clip;

        public virtual void MakeSound()
        {
            AudioPlayer.Instance.PlaySeriesClipAtPoint(clip, this.gameObject.transform.position);
        }
    }
}