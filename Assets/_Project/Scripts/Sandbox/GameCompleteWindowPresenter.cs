using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChopChop.GUI
{
    public class GameCompleteWindowPresenter : MonoBehaviour
    {
        [SerializeField] private Button continueButton;

        public void Show(Action onContinueClick)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() => onContinueClick());

            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}