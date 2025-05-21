using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGate : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // 문 닫는 애니메이션 재생
        StartCoroutine(CloseGate());
    }

    // 일정 시간 후에 문 닫기
    void Update()
    {
        
    }

    private IEnumerator CloseGate()
    {
        // 2초 후에 문 닫기 : 페이드 아웃에 맞춰서 시간 재설정 필요 
        yield return new WaitForSeconds(1f); 
        animator.SetTrigger("Close");
       
    }
}
