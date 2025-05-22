using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("targets")]
    public GameObject _player; // 플레이어 타겟 
    public GameObject _target; // 오브젝트 타겟 

    [Header("Use Icons")] // 나타나게 할 아이콘 
    public List<GameObject> _icons = new List<GameObject>(); // 아이콘 리스트 

    [Header("Use TargetIcon")] // 나타나게 할 아이콘 
    public GameObject _targetIcon;
    public GameObject _targetIcon_Move;

    [Header("Distance Setting")]
    private float distance = 0f; // 플레이어와 타겟 사이 거리 
    private float minDistance = 2f; // 최소 거리 
    private float maxDistance = 3f; // 최대 거리 
    private float offset = 2f;
    private bool isStageClear = true; // 스테이지 클리어 여부 > Test 

    private void Awake()
    {
        foreach (var icon in _icons)
        {
            if (icon.name == _targetIcon_Move.name)
            {
                icon.SetActive(true); // 이동 아이콘은 활성화 
            }// 이동 아이콘은 제외
            icon.gameObject.SetActive(false); // 아이콘 비활성화 
        }

    }

    private void Update()
    {
        if (_player == null || _target == null) return;
        distance = Vector3.Distance(_player.transform.position, _target.transform.position); // 거리 계산 

        Debug.Log($"Update : 플레이어와 타겟의 거리:  {distance}");
        IconUpdate();
    }

    public void IconUpdate()
    {

        if (distance < minDistance)
        {
            Debug.Log($"Show : 플레이어와 타겟의 거리:  {distance}");
            IconShow(_targetIcon); // 아이콘 활성화
            MoveIconAll(_targetIcon_Move, true); // 이동 아이콘 활성화 

        }
        else if (distance > maxDistance)
        {
            Debug.Log($"Hide : 플레이어와 타겟의 거리:  {distance}");
            HideIcon(_targetIcon); // 아이콘 비활성화 

        }
        else if (isStageClear)
        {
            MoveIconAll(_targetIcon_Move, false); // 이동 아이콘 비활성화 
        }
        else return; 

    }

    private void IconShow(GameObject iconObj)
    {
        if (!iconObj.activeSelf)
            iconObj.SetActive(true);
        UpdatePosition(iconObj);
    }

    // 기본 아이콘 끄기
    private void HideIcon(GameObject iconObj)
    {
        if (iconObj != null && iconObj.activeSelf)
            iconObj.SetActive(false);
    }
    private void MoveIconAll(GameObject iconObj, bool isActive) // isActive : 이동 아이콘 활성화 여부 // 숨기고 싶을 땐 따로 시간에 맞춰서 false 로 바꾸기 
    {
        iconObj.SetActive(isActive); // 이동 아이콘 활성화 
        foreach (GameObject child in iconObj.GetComponentsInChildren<GameObject>())
        {
            child.SetActive(isActive); // 자식 오브젝트 포함 
            child.transform.position = _player.transform.position + Vector3.up;  // 아이콘 위치 업데이트
        }
    }

    // 이동 아이콘 끄기
  

    private void UpdatePosition(GameObject iconObj)
    {
        
            iconObj.transform.position = _target.transform.position + Vector3.up * offset;  // 아이콘 위치 업데이트

    }

}
