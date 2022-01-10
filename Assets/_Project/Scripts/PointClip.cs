using UnityEngine;

namespace ChopChop
{
    public class PointClip : MonoBehaviour
    {
        public static void Create(AudioClip clip, Vector3 position, float pitch, float volume = 1.0f)
        {
            GameObject pointObject = Instantiate(new GameObject(), position, Quaternion.identity);
            SetupObject(clip, pointObject, pitch, volume);

            Destroy(pointObject, clip.length / pitch);

            // // return pointObject.AddComponent<PointClip>();
        }

        private static void SetupObject(AudioClip clip, GameObject obj, float pitch, float volume = 1.0f)
        {
            AudioSource audioSource = obj.AddComponent<AudioSource>();
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clip, volume);
        }
    }
}