using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    [Header("targets")]
    public GameObject _player; // 플레이어 타겟 
    public GameObject _target; // 오브젝트 타겟 

    [Header("Use Icons")] // 나타나게 할 아이콘 
    public List<GameObject> _icons = new List<GameObject>(); // 아이콘 리스트 

    [Header("Use TargetIcon")] // 나타나게 할 아이콘 
    public GameObject _targetIcon;

    [Header("Distance Setting")]
    private float distance = 0f; // 플레이어와 타겟 사이 거리 
    private float minDistance = 2f; // 최소 거리 
    private float maxDistance = 3f; // 최대 거리 
    private float offset = 2f;

    private void Start()
    {
        foreach (var icon in _icons)
        {
            icon.SetActive(false); // 아이콘 초기화 :  비활성화 
        }

    }

    private void Update()
    {
        if (_player == null || _target == null) return;

        IconUpdate();
    }

    public void IconUpdate()
    {
        distance = Vector3.Distance(_player.transform.position, _target.transform.position); // 거리 계산 
        Debug.Log($"Update : 플레이어와 타겟의 거리:  {distance}");

        if (distance < minDistance)
        {
            Debug.Log($"Show : 플레이어와 타겟의 거리:  {distance}");
            IconShow(_targetIcon); // 아이콘 활성화

        }
        else if (distance > maxDistance)
        {
            Debug.Log($"Hide : 플레이어와 타겟의 거리:  {distance}");
            HideIcon(_targetIcon); // 아이콘 비활성화 

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

    private void UpdatePosition(GameObject iconObj)
    {
        iconObj.transform.position = _target.transform.position + Vector3.up * offset;  // 아이콘 위치 업데이트

    }
}
