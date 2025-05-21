using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Moon : MonoBehaviour
{
    public GameObject CardKey;
    // public GameObject Player;
    private bool isActiveCardKey = false; // ī��Ű�� Ȱ��ȭ �Ǿ����� Ȯ���ϴ� ����

    [SerializeField] private Player player;

    void Start()
    {
        // CardKey�� ���� ȸ������ ����
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
        CardKey.GetComponent<Animator>().SetTrigger("Active"); // �ѹ� Ŀ���ٰ� �۾����� �ִϸ��̼�
        
    }
}
