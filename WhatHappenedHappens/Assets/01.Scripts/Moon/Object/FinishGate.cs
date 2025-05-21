using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGate : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private bool isOpened = false;
    private Player player;

    private Animator animator; // 문 여는 애니메이션
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isPlayerNearby && !isOpened && player != null && Input.GetKeyDown(KeyCode.F))
        {
            if (player.hasCardKey)
            {
                // Debug.Log("Finish Gate Opened!");

                animator.SetTrigger("Open");
                audioSource.Play();
                isOpened = true;
            }
            else
            {
                Debug.Log("Need Card Key to Open the Gate!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = collision.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
        }
    }
}
