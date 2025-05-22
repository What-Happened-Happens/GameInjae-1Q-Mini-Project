using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("targets")]
    public GameObject _player; // 플레이어 타겟 
    public GameObject _target; // 오브젝트 타겟 

    [Header("Icons")]
    public List<GameObject> _icons = new List<GameObject>(); // 아이콘 리스트 

    [Header("Distance Setting")]
    private float distance = 0f; // 플레이어와 타겟 사이 거리 
    private float minDistance = 2f; // 최소 거리 
    private float maxDistance = 3f; // 최대 거리 
    private float offset = 2f;
    private GameObject currentIcon; // 현재 아이콘

    [Header("Use TargetIcon")]
    public GameObject _targetIcon; // 나타나게 할 아이콘 


    private void Awake()
    {
        foreach (var icon in _icons)
        {
            icon.gameObject.SetActive(false); // 아이콘 비활성화 
        }
    }

    private void Update()
    {
        distance = Vector2.Distance(_player.transform.position, _target.transform.position); // 거리 계산 

        Debug.Log($"Update : 플레이어와 타겟의 거리:  {distance}");
        IconUpdate();
    }

    public void IconUpdate()
    {
        if (distance < minDistance )
        {
            Debug.Log($"Show : 플레이어와 타겟의 거리:  {distance}"); 
            IconShow(_targetIcon); // 아이콘 활성화 
        }
        else if(distance > maxDistance) 
        {
            Debug.Log($"Hide : 플레이어와 타겟의 거리:  {distance}");
            HideIcon(); // 아이콘 비활성화 
        }

    }

    public void IconShow(GameObject iconObj)
    {
        if (currentIcon == iconObj && iconObj.activeSelf)
        {
            UpdatePosition(iconObj); // 아이콘 위치 업데이트 
            return; // 이미 활성화된 아이콘이면 아무것도 하지 않음
        }

        foreach (var go in _icons)
        {
            go.SetActive(false); // 모든 아이콘 비활성화 
        }

        iconObj.SetActive(true); // 선택한 아이콘 활성화 
        currentIcon = iconObj; // 현재 아이콘 설정 

        UpdatePosition(iconObj); // 아이콘 위치 업데이트 
    }

    private void HideIcon()
    {
        if (currentIcon != null)
        {
            currentIcon.SetActive(false); // 현재 아이콘 비활성화 
            currentIcon = null; // 현재 아이콘 초기화 
        }
    }

    private void UpdatePosition(GameObject iconObj)
    {
        Vector3 IconPos = _target.transform.position + Vector3.up * offset; // 아이콘 위치 설정 

        iconObj.transform.position = IconPos; // 아이콘 위치 업데이트
    }

}
