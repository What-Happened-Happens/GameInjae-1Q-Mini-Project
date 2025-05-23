using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostCountUIcontroller : UIHelper
{
    // - ghostCounter 랑 recordingTimeRemaining 가져가서 UI 표시 해주시면 될 거 같아요!! 
    ParadoxManager _paradoxManager;

    [Header("ghostImageList")]
    public List<Image> ghosts = new List<Image>(); // 고스트 스프라이트 이미지 리스트 

    private void Start()
    {
        _paradoxManager = FindObjectOfType<ParadoxManager>();
        if (_paradoxManager == null)
            Debug.LogError("ParadoxManager를 찾을 수 없습니다!");

        if (ghosts == null || ghosts.Count == 0)
            Debug.LogWarning("Ghost UI Image 리스트가 비어있습니다. 인스펙터에서 할당해주세요.");
        /*
        foreach (var img in ghosts)
        {
            if (img != null)
                img.gameObject.SetActive(true);
        }
        */

    }
    private void Update()
    {
        int count = _paradoxManager.ghostCounter;
        RefreshGhostUI(count);
    }

    // 고스트 UI Color Change 
    private void RefreshGhostUI(int ghostCount)
    {
        if (ghosts == null || ghosts.Count == 0) return;

        // 생성된 고스트의 개수만큼 리스트 안에 있는 고스트 이미지 활성화
        for (int i = 0; i < ghosts.Count; i++)
        {
            var img = ghosts[i];
            if (img == null) continue;

            if (i < ghostCount)
            {
                ghosts[i].gameObject.SetActive(true);
            }
            else 
            {
                ghosts[i].gameObject.SetActive(false);
            }

        }

    }
}
