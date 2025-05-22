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
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �̸����� �� ��ȯ
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    // �� ��ȯ �� Ư�� ������Ʈ Ȱ��ȭ, �ð� ���� �� Ŀ���� ������ ����
    public void LoadSceneWithDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadAfterDelay(sceneName, delay));
    }

    private IEnumerator LoadAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    // [ ���� �� �ٽ� �ε� ]
    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }


    // ���� �񵿱�� �ε�Ǵ� ���� �ε��� ���� ���� �߰��ϰ� �ʹٸ� ���� Ȯ�� ����
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(AsyncLoadScene(sceneName));
    }

    private IEnumerator AsyncLoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // �ʿ��� ���: asyncLoad.progress Ȱ���Ͽ� �ε� UI ������Ʈ
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // ���� �� �ٽ� �ε�
    // SceneController.Instance.ReloadCurrentScene();

    // 2�� �ڿ� �� ��ȯ
    // SceneController.Instance.LoadSceneWithDelay("GameScene", 2f);

    // ��� �� ��ȯ
    //SceneController.Instance.LoadScene("MenuScene");
}
