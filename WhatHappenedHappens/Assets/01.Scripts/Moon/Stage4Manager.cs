using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject FinishGate;

    public GameObject Player;

    private bool isFinish = false;


    void Start()
    {
        
    }

    void Update()
    {
       
        if (FinishGate.GetComponent<FinishGate>().isOpened && !isFinish)
        {
            isFinish = true;
            StartCoroutine(WaitAndLoadNextScene());
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneController.Instance.LoadScene("EndingScene");
        }

        if (Player.GetComponent<Player>().isDead)
        {
            StartCoroutine(WaitAndLoadStartScene());
        }
    }

    IEnumerator WaitAndLoadNextScene()
    {
        yield return new WaitForSeconds(4f);
        // ¥Ÿ¿Ω æ¿ ∑ŒµÂ
        SceneController.Instance.LoadScene("EndingScene");
    }

    IEnumerator WaitAndLoadStartScene()
    {
        yield return new WaitForSeconds(2f);
        SceneController.Instance.LoadScene("StartScene");
    }

}
