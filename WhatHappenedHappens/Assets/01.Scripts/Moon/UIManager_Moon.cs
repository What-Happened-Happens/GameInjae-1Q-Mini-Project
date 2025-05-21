using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Moon : MonoBehaviour
{
    public GameObject CardKey;
    // public GameObject Player;
    private bool isActiveCardKey = false; // 카드키가 활성화 되었는지 확인하는 변수

    [SerializeField] private Player player;

    void Start()
    {
        // CardKey의 색을 회색으로 설정
        CardKey.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }

    void Update()
    {
        if ((player != null) && !isActiveCardKey)
        {
            if (player.hasCardKey)
            {
                // CardKey.SetActive(true);
                
                StartCoroutine(ActiveCardKey());
                isActiveCardKey = true;
            }
        }
    }

    private IEnumerator ActiveCardKey()
    {
        yield return new WaitForSeconds(2.7f);
        CardKey.GetComponent<Image>().color = Color.white;
        CardKey.GetComponent<Animator>().SetTrigger("Active"); // 한번 커졌다가 작아지는 애니메이션
        
    }
}
