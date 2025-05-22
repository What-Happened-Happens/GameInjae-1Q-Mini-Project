using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 이름으로 씬 전환
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    // 씬 전환 후 특정 오브젝트 활성화, 시간 지연 등 커스텀 로직도 가능
    public void LoadSceneWithDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadAfterDelay(sceneName, delay));
    }

    private IEnumerator LoadAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    // [ 현재 씬 다시 로드 ]
    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }


    // 씬이 비동기로 로드되는 동안 로딩바 연출 등을 추가하고 싶다면 여기 확장 가능
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(AsyncLoadScene(sceneName));
    }

    private IEnumerator AsyncLoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 필요한 경우: asyncLoad.progress 활용하여 로딩 UI 업데이트
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // 현재 씬 다시 로드
    // SceneController.Instance.ReloadCurrentScene();

    // 2초 뒤에 씬 전환
    // SceneController.Instance.LoadSceneWithDelay("GameScene", 2f);

    // 즉시 씬 전환
    //SceneController.Instance.LoadScene("MenuScene");
}
