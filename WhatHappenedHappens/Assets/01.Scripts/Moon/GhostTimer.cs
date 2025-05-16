using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostTimer : MonoBehaviour
{
    public float TotalTime;
    public float ElapsedTime;

    public TextMeshPro textMesh; // �Ӹ� �� �ؽ�Ʈ ������Ʈ ����

    public float RemainingTime => Mathf.Max(0f, TotalTime - ElapsedTime);

    private void Update()
    {
        if (textMesh != null)
        {
            textMesh.text = $"{RemainingTime:F1}s";
        }
    }
}
