using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff_Platform_Effect : MonoBehaviour
{
    // Start is called before the first frame update
    public Material dissolveMaterial;
    private Material instanceMaterial;
    Material baseMaterial;
    SpriteRenderer sr;
    public bool isDissolving = false;
    float dissolvingTime;
    float dissolveValue;
    void Start()
    {
        /*instanceMaterial = new Material(dissolveMaterial);*/
        sr = gameObject.GetComponent<SpriteRenderer>();
        dissolvingTime = 1.5f;
        dissolveValue = 1f;
        /*sr.material = instanceMaterial;*/
        baseMaterial = sr.material;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TriggerDestroy()
    {
        if(gameObject.activeSelf)
        {
            StartCoroutine(DissolveEffect());
            Debug.Log("Destroy");
        }
    }

    public void TriggerCreate()
    {
        StartCoroutine(AssembleEffect());
        Debug.Log("Create");
    }
    IEnumerator DissolveEffect()
    {
        sr.material = dissolveMaterial;
        while (dissolveValue >= 0f)
        { 
            dissolveValue -= Time.deltaTime/dissolvingTime; 
            sr.material.SetFloat("_Dissolve", dissolveValue);
            yield return /*new WaitForSeconds(dissolvingTime)*/null;
        }
        gameObject.SetActive(false);
    }
    IEnumerator AssembleEffect()
    {
        if (dissolveValue <= 1f)
        { 
            dissolveValue += Time.deltaTime; 
        }
        sr.material.SetFloat("_Dissolve", dissolveValue);
        yield return new WaitForSeconds (dissolvingTime) ;
    }
}
