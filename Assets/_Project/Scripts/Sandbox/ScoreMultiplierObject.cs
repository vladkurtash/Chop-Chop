using UnityEngine;

namespace ChopChop
{
    public class ScoreMultiplierObject : MonoBehaviour
    {
        private Root _init;

        private void Awake()
        {
            _init = FindObjectOfType(typeof(Root)) as Root;
        }

        public void CompleteLevel()
        {
            _init.OnLevelComplete();
        }
    }
}