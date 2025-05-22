using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff_Platform_Effect : MonoBehaviour
{
    // Start is called before the first frame update
    public Material dissolveMaterial;
    float elapsedTime;
    public bool isDissolving = false;
    float dissolveTime;
    public OnOff_Platform_Effect() // 持失切
    { 

    }   
    ~OnOff_Platform_Effect () // 社瑚切
    {
        
    } 
    void Start()
    {
        elapsedTime = 0f;
        dissolveTime = 1f;
        gameObject.GetComponent<SpriteRenderer>().material = dissolveMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDissolving)
        { 
            dissolveTime -= Time.deltaTime; 
            dissolveMaterial.SetFloat("_Dissolve", dissolveTime);
        }
        
        if (dissolveTime < 0f)
        {
            Destroy(gameObject);
        }
    }
}
