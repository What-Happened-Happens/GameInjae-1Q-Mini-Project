using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    public GameObject Target_wormholeObject;    // 이동할 반대편 웜홀 
    private Vector3 wormholePosition;           // 웜홀의 위치
    public GameObject player;                   // 플레이어 오브젝트

    private bool isPlayerInWormhole = false;

    void Start()
    {
        wormholePosition = Target_wormholeObject.transform.position;
    }

    void Update()
    {
        if (isPlayerInWormhole && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("[Wormhole] 반대편 웜홀 위치로 이동");
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
