using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    public GameObject Target_wormholeObject;    // �̵��� �ݴ��� ��Ȧ 
    private Vector3 wormholePosition;           // ��Ȧ�� ��ġ
    public GameObject player;                   // �÷��̾� ������Ʈ

    private bool isPlayerInWormhole = false;

    void Start()
    {
        wormholePosition = Target_wormholeObject.transform.position;
    }

    void Update()
    {
        if (isPlayerInWormhole && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("[Wormhole] �ݴ��� ��Ȧ ��ġ�� �̵�");
            MoveToWormhole(Target_wormholeObject.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInWormhole = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInWormhole = false;
        }
    }

    void MoveToWormhole(Vector3 wormholePosition)
    {
        player.transform.position = wormholePosition;
    }
}
