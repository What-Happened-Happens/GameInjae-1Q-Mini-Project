using System.Collections;
using UnityEngine;

public class MoveIconActiveController : MonoBehaviour
{

    [Header("targets")]
    public Transform _player; // �÷��̾� Ÿ�� 

    [Header("Use TargetIcon")]
    public GameObject _targetIcon_Move;

    [Tooltip("�̵� ������ Ȱ��ȭ ����")] 
    public bool isActived = true; // ���� �� �� ������ Ȱ��ȭ ���� ���� ���� // �� ���� ��, �� ����


    private void Start()
    {
        if (_player == null)
        {
            Debug.LogError("�÷��̾ �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        foreach (Transform tr in _targetIcon_Move.GetComponentsInChildren<Transform>())
        {
            if (tr.gameObject != _targetIcon_Move)
            {
                tr.gameObject.SetActive(isActived); // �ڽ� ������Ʈ Ȱ��ȭ 
            }
        } 

        StartCoroutine(DisableChilderenAfterDelay(5f)); // 0.5�� �Ŀ� ��Ȱ��ȭ 
    }

    IEnumerator DisableChilderenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _targetIcon_Move.transform.localPosition = _player.transform.position; // �÷��̾� ��ġ�� �̵� 
        foreach (Transform tr in _targetIcon_Move.GetComponentsInChildren<Transform>())
        {
            tr.gameObject.SetActive(false); // �ڽ� ������Ʈ Ȱ��ȭ 
        }
    }


}
