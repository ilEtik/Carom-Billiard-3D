using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CaromBilliard
{
    /// <summary>
    /// Class for loading a new scene or exit the game.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        /// <summary>
        /// Loads a new scene.
        /// </summary>
        /// <param name="sceneInd"> The scene that should be loaded by it's index. </param>
        public void LoadScene(int sceneInd)
        {
            SceneManager.LoadScene(sceneInd, LoadSceneMode.Single);
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        }
    }
}
