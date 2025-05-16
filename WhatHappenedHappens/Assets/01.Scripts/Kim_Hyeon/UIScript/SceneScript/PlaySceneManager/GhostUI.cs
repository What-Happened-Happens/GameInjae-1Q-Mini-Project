using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostUI : UIHelper
{
    // - ghostCounter 랑 recordingTimeRemaining 가져가서 UI 표시 해주시면 될 거 같아요!! 
    // [SerializeField] private GetItemUI _getItemUI;
    ParadoxManager _paradoxManager;

    [Header("ghostImageList")]
    public List<Image> ghosts = new List<Image>(); // 고스트 스프라이트 이미지 리스트 

    private void Start()
    {
        // if (_getItemUI == null)  _getItemUI = gameObject.AddComponent<GetItemUI>();
        if (_paradoxManager == null) _paradoxManager = gameObject.AddComponent<ParadoxManager>();

        if (ghosts == null || ghosts.Count == 0)
            Debug.LogWarning("Ghost UI Image 리스트가 비어있습니다. 인스펙터에서 할당해주세요.");

        foreach (var img in ghosts)
        {
            if (img != null)
                img.gameObject.SetActive(true);
        }

    }
    private void Update()
    {
        int count = 1;
        RefreshGhostUI(count);
    }

    // 고스트 UI Color Change 
    private void RefreshGhostUI(int ghostCount)
    {
        if (ghosts == null || ghosts.Count == 0) return;
        Debug.LogWarning($"UI 이미지가 비어있습니다.");


        // 생성된 고스트의 개수만큼 리스트 안에 있는 고스트 이미지의 색을 white로 변경 
        for (int i = 0; i < ghosts.Count; i++)
        {
            if (ghosts[i] == null) continue;

            if (i < ghostCount)
            {              
                SetImageColor(ghosts[i], Color.white);
            }
            else
            {
              SetImageColor(ghosts[i], Color.gray);
            }
            Debug.Log($"색 변경 : {ghosts[i].color}");
        }

    }
}
