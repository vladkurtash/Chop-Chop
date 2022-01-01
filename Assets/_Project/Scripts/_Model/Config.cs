using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config")]
    public class Config : SingletonScriptableObject<Config>
    {
        public float playerInputCoolDown;
        public int defaultFrameRate = 60;
        public Vector3 cutOffPartImpulseForce = new Vector3(3.0f, 0.0f, 0.0f);
    }
}