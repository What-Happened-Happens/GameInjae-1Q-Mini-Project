using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Manager : MonoBehaviour
{
    void Start()
    {
        // 첫번째 클립 재생 
        SoundManager.Instance.PlayBGM(SoundManager.Instance.bgmClips[0]);
    }

    void Update()
    {
        
    }
}
