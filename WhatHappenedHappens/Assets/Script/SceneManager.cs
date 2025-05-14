using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Scenes
{
    public class ScenesManager : MonoBehaviour
    {
        [SerializeField] private string mainSceneName = "MainScene";
        [SerializeField] private string uiSceneName = "UIScene";

        private void Start()
        {
            LoadSceneAdditively(mainSceneName);
            LoadSceneAdditively(uiSceneName);
        }

        private void LoadSceneAdditively(string sceneName)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }

    }
}