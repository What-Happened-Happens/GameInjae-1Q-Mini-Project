using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//IUIHelper 상속 : Image의 속성(컬러, 포지션, 스케일 등) 셋팅 할 수 있는 캡슐화 셋팅함수 인터페이스. 한번 확인 부탁
// 추후, 속성 셋팅용 인터페이스로 사용

public class SpriteManager : UIHelper
{ // - ghostCounter 랑 recordingTimeRemaining 가져가서 UI 표시 해주시면 될 거 같아요!! 
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
        int count = _paradoxManager.ghostCounter;
        RefreshGhostUI(count);
    }

    // 고스트 UI 를 보여준다. 
    private void RefreshGhostUI(int ghostCount)
    {
        Debug.Log($"개수 변환에 따라서 색깔 변경 ");
        // 생성된 고스트의 개수만큼 리스트 안에 있는 고스트 이미지의 색을 White로 변경 
        for (int i = 0; i < ghostCount; i++)
        {
            if (ghosts[i] == null) continue;

            if (i < ghostCount)
                SetImageColor(ghosts[i], Color.white);
            else
                SetImageColor(ghosts[i], Color.gray);
        }

    }


}
