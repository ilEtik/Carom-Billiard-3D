using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CaromBilliard
{
    public class HUD : MonoBehaviour
    {
        public Button replayButton;
        public LineRenderer line;
        public GameObject replayObject, sliderObject;
        public TextMeshProUGUI sliderText;

        private Slider slider;

        private void Start()
        {
            if (sliderObject != null)
                slider = sliderObject.GetComponentInChildren<Slider>();
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
            ReplaySystem.OnReplayStart += SetReplayOverlay;
            ReplaySystem.OnReplayStop += SetReplayOverlay;
        }

        void SetSlider(float value)
        {
            if (slider != null)
                slider.value = value;
            if (sliderText != null)
                sliderText.text = "Hold Space";
        }

        void SetSlider(float value, float minValue, float maxValue)
        {
            if (slider != null)
            {
                slider.value = value;
                slider.minValue = minValue;
                slider.maxValue = maxValue;
            }
            if(sliderText != null)
                sliderText.text = "Release Space";
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

        void SetReplayOverlay(bool isShowing)
        {
            if (slider != null)
                slider.gameObject.SetActive(!isShowing);
            if (replayObject != null)
                replayObject.SetActive(isShowing);
            if (replayButton != null)
                replayButton.gameObject.SetActive(!isShowing);
            if (sliderObject != null)
                sliderObject.SetActive(!isShowing);
        }
    }
}
