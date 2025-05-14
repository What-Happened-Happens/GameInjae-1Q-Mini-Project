using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button;
    Button_KangJin bk;
    bool ButtonOn = false;
    void Start()
    {
        bk = button.GetComponent<Button_KangJin>();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonOn = bk.ButtonOn;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(ButtonOn)
        { 
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = -1f; 
        }
        else
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f; 
    }
}
