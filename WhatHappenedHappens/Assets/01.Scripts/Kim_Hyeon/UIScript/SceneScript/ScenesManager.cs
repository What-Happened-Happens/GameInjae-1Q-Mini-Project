using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Kim_Hyeon
{
    public class ScenesManager : MonoBehaviour
    {
        [SerializeField] private string mainSceneName = "MainScene";
        [SerializeField] private string uiSceneName = "UIScene";

        private void Start()
        {
            // MainScene 과 UI Scene 함꼐 출력 -> MainScene Camera 에 적용 
            // Build Setting 에서 UIScene 추가 필요.
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