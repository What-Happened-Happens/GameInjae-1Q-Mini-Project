using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGate : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // �� �ݴ� �ִϸ��̼� ���
        StartCoroutine(CloseGate());
    }

    // ���� �ð� �Ŀ� �� �ݱ�
    void Update()
    {
        
    }

    private IEnumerator CloseGate()
    {
        // 2�� �Ŀ� �� �ݱ� : ���̵� �ƿ��� ���缭 �ð� �缳�� �ʿ� 
        yield return new WaitForSeconds(1f); 
        animator.SetTrigger("Close");
       
    }
}
