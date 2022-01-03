using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config")]
    public class Config : SingletonScriptableObject<Config>
    {
        public float playerInputCoolDown;
        public int defaultFrameRate = 60;
        public Vector3 cutOffPartDefaultImpulseForce = new Vector3(3.0f, 0.0f, 0.0f);
        public Vector3 axeDeathDefaultImpulseForce = new Vector3(0.0f, 75.0f, -75.0f);
    }
}