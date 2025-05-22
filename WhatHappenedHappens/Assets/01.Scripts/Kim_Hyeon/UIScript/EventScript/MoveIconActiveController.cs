using System.Collections;
using UnityEngine;

public class MoveIconActiveController : MonoBehaviour
{

    [Header("targets")]
    public Transform _player; // 플레이어 타겟 

    [Header("Use TargetIcon")]
    public GameObject _targetIcon_Move;

    [Tooltip("이동 아이콘 활성화 여부")] 
    public bool isActived = true; // 연결 시 이 변수로 활성화 여부 조정 가능 // 씬 연결 시, 씬 시작


    private void Start()
    {
        if (_player == null)
        {
            Debug.LogError("플레이어가 할당되지 않았습니다.");
            return;
        }

        foreach (Transform tr in _targetIcon_Move.GetComponentsInChildren<Transform>())
        {
            if (tr.gameObject != _targetIcon_Move)
            {
                tr.gameObject.SetActive(isActived); // 자식 오브젝트 활성화 
            }
        } 

        StartCoroutine(DisableChilderenAfterDelay(5f)); // 0.5초 후에 비활성화 
    }

    IEnumerator DisableChilderenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _targetIcon_Move.transform.localPosition = _player.transform.position; // 플레이어 위치로 이동 
        foreach (Transform tr in _targetIcon_Move.GetComponentsInChildren<Transform>())
        {
            tr.gameObject.SetActive(false); // 자식 오브젝트 활성화 
        }
    }


}
