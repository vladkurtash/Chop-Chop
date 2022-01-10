using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "Config", menuName = "SingletonScriptableObject/Config")]
    public class Config : SingletonScriptableObject<Config>
    {
        [SerializeField] public float playerInputCoolDown;
        [SerializeField] public int defaultFrameRate = 60;
        [SerializeField] public Vector3 cutOffPartDefaultImpulseForce = new Vector3(3.0f, 0.0f, 0.0f);
        [SerializeField] public Vector3 axeDeathDefaultImpulseForce = new Vector3(0.0f, 75.0f, -75.0f);
    }
}