using UnityEngine;

namespace ChopChop
{
    [CreateAssetMenu(fileName = "AxeBlinkingConfig", menuName = "SingletonScriptableObject/AxeBlinkingConfig")]
    public class AxeBlinkingConfig : SingletonScriptableObject<AxeBlinkingConfig>
    {
        [SerializeField] public Color32 targetColor = new Color32(255, 255, 255, 255);
        [SerializeField] public float blinkingRate = 0.3f;
        [SerializeField] public int blinkCount = 1;
    }
}