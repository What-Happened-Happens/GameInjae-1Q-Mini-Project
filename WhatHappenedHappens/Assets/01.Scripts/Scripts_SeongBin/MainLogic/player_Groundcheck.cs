using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class player_Groundcheck : MonoBehaviour
    {
        public bool isGroundCheck;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
            isGroundCheck = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ground")
            isGroundCheck = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ground")
            isGroundCheck = false;
    }
    
}
    
