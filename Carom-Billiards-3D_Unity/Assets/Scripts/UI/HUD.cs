using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CaromBilliard
{
    public class HUD : MonoBehaviour, IServiceLocator
    {
        void IServiceLocator.ProvideService() { }

        private CommandInvoker invoker;
        private IngameScoreSystem scoreSystem;
        private PlayerController playerController;
        private BallsManager ballsManager;
        private ReplaySystem replaySystem;

        void IServiceLocator.GetService()
        {
            invoker = ServiceLocator.GetService<CommandInvoker>();
            scoreSystem = ServiceLocator.GetService<IngameScoreSystem>();
            playerController = ServiceLocator.GetService<PlayerController>();
            ballsManager = ServiceLocator.GetService<BallsManager>();
            replaySystem = ServiceLocator.GetService<ReplaySystem>();
        }

        public Button replayButton;
        public LineRenderer line;
        public GameObject isReplayDisplay, forceSliderElement, scoreElement;

        private TextMeshProUGUI sliderTutorialText;
        private Slider forceSlider;

        private void Start()
        {
            if (forceSliderElement != null)
            {
                forceSlider = forceSliderElement.GetComponentInChildren<Slider>();
                sliderTutorialText = forceSliderElement.GetComponentInChildren<TextMeshProUGUI>();
            }

            RegisterEvents();
        }

        void RegisterEvents()
        {
            invoker.OnHasCommands += SetReplayButtonActive;
            playerController.OnChargeForce += SetSlider;
            playerController.OnApplyForce += SetSlider;
            playerController.OnAiming += SetLine;
            ballsManager.OnMoving += SetReplayButton;
            ballsManager.OnMoving += SetLine;
            replaySystem.OnReplayStart += ShowReplayOverlay;
            replaySystem.OnReplayStop += HideReplayOverlay;
            scoreSystem.OnGameOver += GameOver;
        }

        void SetSlider(float value)
        {
            if (forceSlider != null)
                forceSlider.value = value;
            if (sliderTutorialText != null)
                sliderTutorialText.text = "Hold Space";
        }

        void SetSlider(float value, float minValue, float maxValue)
        {
            if (forceSlider != null)
            {
                forceSlider.value = value;
                forceSlider.minValue = minValue;
                forceSlider.maxValue = maxValue;
            }
            if (sliderTutorialText != null)
                sliderTutorialText.text = "Release Space";
        }

        void SetReplayButtonActive(bool canReplay)
        {
            if (replayButton != null)
                replayButton.gameObject.SetActive(canReplay);
        }

        void SetReplayButton(bool isMoving)
        {
            if (replayButton != null)
                replayButton.interactable = !isMoving;
        }

        void SetLine(bool isMoving)
        {
            if (line != null)
                line.enabled = !isMoving;
        }

        void SetLine(params Vector3[] points)
        {
            if (line != null)
            {
                line.positionCount = points.Length;
                line.SetPositions(points);
            }
        }

        void ShowReplayOverlay()
        {
            SetReplayOverlay(true);
        }

        void HideReplayOverlay()
        {
            SetReplayOverlay(false);
        }

        void SetReplayOverlay(bool isReplay)
        {
            if (forceSlider != null)
                forceSlider.gameObject.SetActive(!isReplay);
            if (isReplayDisplay != null)
                isReplayDisplay.SetActive(isReplay);
            if (replayButton != null)
                replayButton.gameObject.SetActive(!isReplay);
            if (forceSliderElement != null)
                forceSliderElement.SetActive(!isReplay);
            if (scoreElement != null)
                scoreElement.SetActive(!isReplay);
        }

        void GameOver()
        {
            gameObject.SetActive(false);
        }
    }
}
