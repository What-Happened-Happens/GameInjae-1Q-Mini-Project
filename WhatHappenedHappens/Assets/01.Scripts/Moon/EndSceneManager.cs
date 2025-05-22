using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    void Start()
    {
        // 두번째 클립 재생
        SoundManager.Instance.PlayBGM(SoundManager.Instance.bgmClips[2]);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneController.Instance.LoadScene("StartScene");
        }
    }
}
