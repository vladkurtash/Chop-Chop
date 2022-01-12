using UnityEngine;

namespace ChopChop
{
    public class ScoreMultiplierObject : MonoBehaviour
    {
        private Root _init;
        [SerializeField] private GameObject confetti;

        private void Awake()
        {
            _init = FindObjectOfType(typeof(Root)) as Root;
        }

        public void CompleteLevel()
        {
            _init.OnLevelComplete();
            SpawnConfetti();
        }

        private void SpawnConfetti()
        {
            Vector3 position = this.gameObject.transform.position;
            
            float spawnPositionXOffset = transform.localScale.x / 2;
            float spawnPositionY = position.y + (transform.localScale.y / 2);

            Vector3 spawnPositionLeft = new Vector3(position.x - spawnPositionXOffset, spawnPositionY, position.z);
            Vector3 spawnPositionRight = new Vector3(position.x + spawnPositionXOffset, spawnPositionY, position.z);
            
            SpawnObject(confetti, spawnPositionLeft, Quaternion.identity);
            SpawnObject(confetti, spawnPositionRight, Quaternion.identity);
        }

        private void SpawnObject(GameObject obj, Vector3 position, Quaternion rotation)
        {
            Instantiate(obj, position, rotation);
        }
    }
}