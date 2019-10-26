using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CaromBilliard
{
    public class HUD : MonoBehaviour
    {
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
            PlayerController.OnChargeForce += SetSlider;
            PlayerController.OnApplyForce += SetSlider;
            PlayerController.OnAiming += SetLine;
            BallsManager.Instance.OnMoving += SetReplayButton;
            BallsManager.Instance.OnMoving += SetLine;
            IngameScoreSystem.Instance.OnGameOver += () => Destroy(gameObject);
            ReplaySystem.OnReplayStart += ShowReplayOverlay;
            ReplaySystem.OnReplayStop += HideReplayOverlay;
            CommandInvoker.OnHasCommands += (hasCommand) => replayButton.gameObject.SetActive(hasCommand);
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
    }
}
