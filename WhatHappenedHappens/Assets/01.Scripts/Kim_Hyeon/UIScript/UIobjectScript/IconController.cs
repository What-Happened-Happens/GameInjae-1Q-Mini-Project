using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    [Header("targets")]
    public GameObject _player; // �÷��̾� Ÿ�� 
    public GameObject _target; // ������Ʈ Ÿ�� 

    [Header("Use Icons")] // ��Ÿ���� �� ������ 
    public List<GameObject> _icons = new List<GameObject>(); // ������ ����Ʈ 

    [Header("Use TargetIcon")] // ��Ÿ���� �� ������ 
    public GameObject _targetIcon;

    [Header("Distance Setting")]
    private float distance = 0f; // �÷��̾�� Ÿ�� ���� �Ÿ� 
    private float minDistance = 2f; // �ּ� �Ÿ� 
    private float maxDistance = 3f; // �ִ� �Ÿ� 
    private float offset = 2f;

    private void Start()
    {
        foreach (var icon in _icons)
        {
            icon.SetActive(false); // ������ �ʱ�ȭ :  ��Ȱ��ȭ 
        }

    }

    private void Update()
    {
        if (_player == null || _target == null) return;

        IconUpdate();
    }

    public void IconUpdate()
    {
        distance = Vector3.Distance(_player.transform.position, _target.transform.position); // �Ÿ� ��� 
        Debug.Log($"Update : �÷��̾�� Ÿ���� �Ÿ�:  {distance}");

        if (distance < minDistance)
        {
            Debug.Log($"Show : �÷��̾�� Ÿ���� �Ÿ�:  {distance}");
            IconShow(_targetIcon); // ������ Ȱ��ȭ

        }
        else if (distance > maxDistance)
        {
            Debug.Log($"Hide : �÷��̾�� Ÿ���� �Ÿ�:  {distance}");
            HideIcon(_targetIcon); // ������ ��Ȱ��ȭ 

        }
        else return;

    }

    private void IconShow(GameObject iconObj)
    {
        if (!iconObj.activeSelf)
            iconObj.SetActive(true);
        UpdatePosition(iconObj);
    }

    // �⺻ ������ ����
    private void HideIcon(GameObject iconObj)
    {
        if (iconObj != null && iconObj.activeSelf)
            iconObj.SetActive(false);
    }

    private void UpdatePosition(GameObject iconObj)
    {
        iconObj.transform.position = _target.transform.position + Vector3.up * offset;  // ������ ��ġ ������Ʈ

    }
}
