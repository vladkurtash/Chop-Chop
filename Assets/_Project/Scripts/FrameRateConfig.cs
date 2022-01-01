using UnityEngine;

namespace ChopChop
{
    public class FrameRateConfig : MonoBehaviour
    {
        public void SetFrameRateTo30()
        {
            Application.targetFrameRate = 30;
        }

        public void SetFrameRateTo60()
        {
            Application.targetFrameRate = 60;
        }
    }
}