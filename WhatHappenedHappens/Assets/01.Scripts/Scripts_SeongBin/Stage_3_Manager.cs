using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_3_Manager : MonoBehaviour
{
    public TrueFalse buttonLeft;
    public MovingPlatform vanishingFlatForm;
    public Lever lever;
    public GameObject conveyor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buttonLeftResponse();
        LeverResponse();
    }

    void buttonLeftResponse()
    {
        if (buttonLeft != null)
        {
            Debug.Log("��ư" + buttonLeft.IsTrue);
            if (buttonLeft.IsTrue)
            {
 
                Debug.Log("�ڽ� ��ũ��Ʈ���� isTrue�� true�Դϴ�!");
                vanishingFlatForm.SetActive(true);

            }
            else
            {
                vanishingFlatForm.SetActive(false);
            }
        }
        else
            return;
    }

    void LeverResponse()
    {
        if (lever != null)
        {
            Debug.Log("����  : " + lever.IsTrue);
            if (lever.IsTrue)
            {
                // �����̾� ��Ʈ �����̱�?!
            }
            else
            {

            }

        }
        else
            return;
    }


}
