using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChopChop.GUI
{
    public class MainMenuWindowPresenter : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        
        public void Show(Action onStartClick)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() => onStartClick());
            startButton.onClick.AddListener(() => Hide());

            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}