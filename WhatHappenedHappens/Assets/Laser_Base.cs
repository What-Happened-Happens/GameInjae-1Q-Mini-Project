using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Base : MonoBehaviour
{
    // Start is called before the first frame update
    public TrueFalse trigger;
    public GameObject laser;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger.IsTrue)
        {
            laser.SetActive(true); 
        }
        else
        {
            laser.SetActive(false);
        }
    }
}
