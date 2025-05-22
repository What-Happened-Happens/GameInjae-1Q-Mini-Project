using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("targets")]
    public GameObject _player; // �÷��̾� Ÿ�� 
    public GameObject _target; // ������Ʈ Ÿ�� 

    [Header("Icons")]
    public List<GameObject> _icons = new List<GameObject>(); // ������ ����Ʈ 

    [Header("Distance Setting")]
    private float distance = 0f; // �÷��̾�� Ÿ�� ���� �Ÿ� 
    private float minDistance = 2f; // �ּ� �Ÿ� 
    private float maxDistance = 3f; // �ִ� �Ÿ� 
    private float offset = 2f;
    private GameObject currentIcon; // ���� ������

    [Header("Use TargetIcon")]
    public GameObject _targetIcon; // ��Ÿ���� �� ������ 


    private void Awake()
    {
        foreach (var icon in _icons)
        {
            icon.gameObject.SetActive(false); // ������ ��Ȱ��ȭ 
        }
    }

    private void Update()
    {
        distance = Vector2.Distance(_player.transform.position, _target.transform.position); // �Ÿ� ��� 

        Debug.Log($"Update : �÷��̾�� Ÿ���� �Ÿ�:  {distance}");
        IconUpdate();
    }

    public void IconUpdate()
    {
        if (distance < minDistance )
        {
            Debug.Log($"Show : �÷��̾�� Ÿ���� �Ÿ�:  {distance}"); 
            IconShow(_targetIcon); // ������ Ȱ��ȭ 
        }
        else if(distance > maxDistance) 
        {
            Debug.Log($"Hide : �÷��̾�� Ÿ���� �Ÿ�:  {distance}");
            HideIcon(); // ������ ��Ȱ��ȭ 
        }

    }

    public void IconShow(GameObject iconObj)
    {
        if (currentIcon == iconObj && iconObj.activeSelf)
        {
            UpdatePosition(iconObj); // ������ ��ġ ������Ʈ 
            return; // �̹� Ȱ��ȭ�� �������̸� �ƹ��͵� ���� ����
        }

        foreach (var go in _icons)
        {
            go.SetActive(false); // ��� ������ ��Ȱ��ȭ 
        }

        iconObj.SetActive(true); // ������ ������ Ȱ��ȭ 
        currentIcon = iconObj; // ���� ������ ���� 

        UpdatePosition(iconObj); // ������ ��ġ ������Ʈ 
    }

    private void HideIcon()
    {
        if (currentIcon != null)
        {
            currentIcon.SetActive(false); // ���� ������ ��Ȱ��ȭ 
            currentIcon = null; // ���� ������ �ʱ�ȭ 
        }
    }

    private void UpdatePosition(GameObject iconObj)
    {
        Vector3 IconPos = _target.transform.position + Vector3.up * offset; // ������ ��ġ ���� 

        iconObj.transform.position = IconPos; // ������ ��ġ ������Ʈ
    }

}
