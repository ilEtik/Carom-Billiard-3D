using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CaromBilliard
{
    /// <summary>
    /// Controlls the head up display.
    /// </summary>
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

        public LineRenderer line;
        public GameObject replayTutorialElement, isReplayDisplay, forceSliderElement, scoreElement;

        private TextMeshProUGUI sliderTutorialText;
        private Slider forceSlider;
        private Image replayTutorialImage;

        private void Start()
        {
            SetCursorDisplayMode(false);

            if (forceSliderElement != null)
            {
                forceSlider = forceSliderElement.GetComponentInChildren<Slider>();
                sliderTutorialText = forceSliderElement.GetComponentInChildren<TextMeshProUGUI>();
            }
            if(replayTutorialElement != null)
                replayTutorialImage = GetComponentInChildren<Image>();

            RegisterEvents();
        }

        /// <summary>
        /// Register all events so that the HUD can display everything.
        /// </summary>
        void RegisterEvents()
        {
            invoker.OnHasCommands += SetReplayTutorialActive;
            playerController.OnChargeForce += SetSlider;
            playerController.OnApplyForce += SetSlider;
            playerController.OnAiming += SetLine;
            ballsManager.OnMoving += SetReplayTutorial;
            ballsManager.OnMoving += SetLine;
            replaySystem.OnReplayStart += ShowReplayOverlay;
            replaySystem.OnReplayStop += HideReplayOverlay;
            scoreSystem.OnGameOver += () => SetCursorDisplayMode(true);
            scoreSystem.OnGameOver += GameOver;
        }

        /// <summary>
        /// Sets the cursor visibilty and lockstate.
        /// </summary>
        /// <param name="showCursor"> Is the cursor is shown. </param>
        void SetCursorDisplayMode(bool showCursor)
        {
            Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = showCursor;
        }

        /// <summary>
        /// Sets the value of the slider when the player ball was shooted.
        /// </summary>
        /// <param name="value"> Force of the player ball. </param>
        void SetSlider(float value)
        {
            if (forceSlider != null)
                forceSlider.value = value;
            if (sliderTutorialText != null)
                sliderTutorialText.text = "Hold Space";
        }

        /// <summary>
        /// Sets the value of the slider while the player is charging the force.
        /// </summary>
        /// <param name="value"> The force value that the slider should display. </param>
        /// <param name="minValue"> The minimum value of the slider. </param>
        /// <param name="maxValue"> The maximum value of the slider. </param>
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

        /// <summary>
        /// Set the active mode of the replay button gameobject.
        /// </summary>
        /// <param name="canReplay"> Are the balls moving? </param>
        void SetReplayTutorialActive(bool canReplay)
        {
            if (replayTutorialElement != null)
                replayTutorialElement.SetActive(canReplay);
        }

        /// <summary>
        /// Sets the alpha of the replay image background.
        /// </summary>
        /// <param name="isMoving"> Are the balls moving? </param>
        void SetReplayTutorial(bool isMoving)
        {
            if(replayTutorialImage != null)
            {
                Color col = replayTutorialImage.color;
                col.a = isMoving ? .2f : 1f;
                replayTutorialImage.color = col;
            }
        }

        /// <summary>
        /// Show/Hide the line for displaying the players shooting direction.
        /// </summary>
        /// <param name="isMoving"> Are the balls moving? </param>
        void SetLine(bool isMoving)
        {
            if (line != null)
                line.enabled = !isMoving;
        }

        /// <summary>
        /// Sets the positions of the line for displaying the players shooting direction.
        /// </summary>
        /// <param name="points"> The positions of the line. </param>
        void SetLine(params Vector3[] points)
        {
            if (line != null)
            {
                line.positionCount = points.Length;
                line.SetPositions(points);
            }
        }

        /// <summary>
        /// Display the replay overlay.
        /// </summary>
        void ShowReplayOverlay()
        {
            SetReplayOverlay(true);
        }

        /// <summary>
        /// Hide the replay overlay.
        /// </summary>
        void HideReplayOverlay()
        {
            SetReplayOverlay(false);
        }

        /// <summary>
        /// Sets the replay overlay.
        /// </summary>
        /// <param name="isReplay"> Is currently a replay playing? </param>
        void SetReplayOverlay(bool isReplay)
        {
            if (forceSlider != null)
                forceSlider.gameObject.SetActive(!isReplay);
            if (isReplayDisplay != null)
                isReplayDisplay.SetActive(isReplay);
            if (replayTutorialElement != null)
                replayTutorialElement.gameObject.SetActive(!isReplay);
            if (forceSliderElement != null)
                forceSliderElement.SetActive(!isReplay);
            if (scoreElement != null)
                scoreElement.SetActive(!isReplay);
        }

        /// <summary>
        /// Called when the game is over.
        /// </summary>
        void GameOver()
        {
            gameObject.SetActive(false);
        }
    }
}
