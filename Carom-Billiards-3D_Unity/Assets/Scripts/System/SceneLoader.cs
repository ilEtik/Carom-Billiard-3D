using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CaromBilliard
{
    public class SceneLoader : MonoBehaviour
    {
        private int curScene;

        public void LoadScene(int sceneInd)
        {
            SceneManager.LoadScene(sceneInd, LoadSceneMode.Single);
        }

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
