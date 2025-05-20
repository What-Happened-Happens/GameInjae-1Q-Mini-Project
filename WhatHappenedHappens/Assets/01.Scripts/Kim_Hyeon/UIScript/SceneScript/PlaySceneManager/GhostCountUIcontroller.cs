using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostCountUIcontroller : UIHelper
{
    // - ghostCounter �� recordingTimeRemaining �������� UI ǥ�� ���ֽø� �� �� ���ƿ�!! 
    ParadoxManager _paradoxManager;

    [Header("ghostImageList")]
    public List<Image> ghosts = new List<Image>(); // ��Ʈ ��������Ʈ �̹��� ����Ʈ 

    private void Start()
    {
        // if (_getItemUI == null)  _getItemUI = gameObject.AddComponent<GetItemUI>();
        _paradoxManager = FindObjectOfType<ParadoxManager>();
        if (_paradoxManager == null)
            Debug.LogError("ParadoxManager�� ã�� �� �����ϴ�!");

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
        int count = _paradoxManager.ghostCounter;
        RefreshGhostUI(count);
    }

    // ��Ʈ UI Color Change 
    private void RefreshGhostUI(int ghostCount)
    {
        if (ghosts == null) return;
        Debug.LogWarning($"UI �̹����� ����ֽ��ϴ�.");

        // ������ ��Ʈ�� ������ŭ ����Ʈ �ȿ� �ִ� ��Ʈ �̹����� ���� white�� ���� 
        for (int i = 0; i < ghosts.Count; i++)
        {
            if (ghosts[i] == null) continue;

            if (i <= ghostCount)
            {
                SetImageColor(ghosts[i], Color.white);
            }
            else if (i >= ghostCount)
            {
                SetImageColor(ghosts[i], Color.gray);
            }
            Debug.Log($"�� ���� : {ghosts[i].color}");

        }

    }
}
