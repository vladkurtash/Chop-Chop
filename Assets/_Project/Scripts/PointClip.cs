using UnityEngine;

namespace ChopChop
{
    //TODO: Create ObjectPooler instead of Instantiation
    public class PointClip : MonoBehaviour
    {
        public static PointClip Create(AudioClip clip, Vector3 position, float pitch, float volume = 1.0f)
        {
            GameObject pointObject = new GameObject();
            PointClip pointClip = pointObject.AddComponent<PointClip>();

            pointClip._pitch = 1;
            pointClip._clip = clip;
            pointClip._volume = volume;

            return pointClip;
        }

        private float _pitch = 0.0f;
        private AudioClip _clip = null;
        private float _volume = 1.0f;
        
        private void Start()
        {
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.pitch = _pitch;
            audioSource.PlayOneShot(_clip, _volume);

            Destroy(this.gameObject, _clip.length / _pitch);
        }
    }
}