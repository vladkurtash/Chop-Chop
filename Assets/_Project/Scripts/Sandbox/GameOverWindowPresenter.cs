using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChopChop.GUI
{
    public class GameOverWindowPresenter : MonoBehaviour
    {
        [SerializeField] private Button restartButton;

        public void Show(Action onRestartClick)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(() => onRestartClick());

            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}