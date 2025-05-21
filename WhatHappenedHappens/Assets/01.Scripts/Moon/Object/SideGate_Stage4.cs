using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideGate_Stage4 : MonoBehaviour
{
    [Header("SideGate Triggers")]
    public GameObject Rever1;
    public GameObject Rever2;
    public GameObject CircuitBreaker;

    [Header("Components")]
    private Animator animator;
    private Collider2D gateCollider;

    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        gateCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(!isOpen &&
            ((Rever1.GetComponent<Lever>().IsTrue && Rever2.GetComponent<Lever>().IsTrue) 
            || CircuitBreaker.GetComponent<CircuitBreaker>().IsTrue))
        {
            isOpen = true;
            animator.SetTrigger("Open");
            gateCollider.isTrigger = true; // ��� �����ϰ� 
        }
        else if (isOpen &&
            (!(Rever1.GetComponent<Lever>().IsTrue && Rever2.GetComponent<Lever>().IsTrue)
            && !CircuitBreaker.GetComponent<CircuitBreaker>().IsTrue))
        {
            isOpen = false;
            animator.SetTrigger("Close");
            gateCollider.isTrigger = false; // ��� �Ұ����ϰ�
        }

    }
}
