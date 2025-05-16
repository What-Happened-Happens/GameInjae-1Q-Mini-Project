using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostUI : UIHelper
{
    // - ghostCounter �� recordingTimeRemaining �������� UI ǥ�� ���ֽø� �� �� ���ƿ�!! 
    // [SerializeField] private GetItemUI _getItemUI;
    ParadoxManager _paradoxManager;

    [Header("ghostImageList")]
    public List<Image> ghosts = new List<Image>(); // ��Ʈ ��������Ʈ �̹��� ����Ʈ 

    private void Start()
    {
        // if (_getItemUI == null)  _getItemUI = gameObject.AddComponent<GetItemUI>();
        if (_paradoxManager == null) _paradoxManager = gameObject.AddComponent<ParadoxManager>();

        if (ghosts == null || ghosts.Count == 0)
            Debug.LogWarning("Ghost UI Image ����Ʈ�� ����ֽ��ϴ�. �ν����Ϳ��� �Ҵ����ּ���.");

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

    // ��Ʈ UI Color Change 
    private void RefreshGhostUI(int ghostCount)
    {
        if (ghosts == null || ghosts.Count == 0) return;
        Debug.LogWarning($"UI �̹����� ����ֽ��ϴ�.");


        // ������ ��Ʈ�� ������ŭ ����Ʈ �ȿ� �ִ� ��Ʈ �̹����� ���� white�� ���� 
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
            Debug.Log($"�� ���� : {ghosts[i].color}");
        }

    }
}
