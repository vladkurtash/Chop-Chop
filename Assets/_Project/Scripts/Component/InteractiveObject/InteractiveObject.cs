using UnityEngine;
using UnityEngine.Events;

namespace ChopChop
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class InteractiveObject : MonoBehaviour
    {
        [SerializeField] public UnityEvent Response;

        public void Raise()
        {
            Response?.Invoke();
        }
    }

    public class SoundableObject : InteractiveObject, ISoundable
    {
        [SerializeField] private AudioClip clip;

        public virtual void MakeSound()
        {
            AudioPlayer.Instance.PlaySeriesClipAtPoint(clip, this.gameObject.transform.position);
        }
    }
}