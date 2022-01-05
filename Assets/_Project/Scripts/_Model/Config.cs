using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config")]
    public class Config : SingletonScriptableObject<Config>
    {
        [SerializeField] public float playerInputCoolDown;
        [SerializeField] public int defaultFrameRate = 60;
        [SerializeField] public Vector3 cutOffPartDefaultImpulseForce = new Vector3(3.0f, 0.0f, 0.0f);
        [SerializeField] public Vector3 axeDeathDefaultImpulseForce = new Vector3(0.0f, 75.0f, -75.0f);
        
        [Header("AxeBlinking")]
        [SerializeField] public Color32 colorEnd = new Color32(255, 255, 255, 255);
        [SerializeField] public float blinkingRate = 0.3f;
        [SerializeField] public int blinkCount = 1;
    }
}