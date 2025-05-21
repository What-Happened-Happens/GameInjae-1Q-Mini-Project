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
            Debug.Log("버튼" + buttonLeft.IsTrue);
            if (buttonLeft.IsTrue)
            {
 
                Debug.Log("자식 스크립트에서 isTrue가 true입니다!");
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
            Debug.Log("레버  : " + lever.IsTrue);
            if (lever.IsTrue)
            {
                // 컨베이어 벨트 움직이기?!
            }
            else
            {

            }

        }
        else
            return;
    }


}
