using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGravityState : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Sprites")]
    /*public Sprite[] sprites = new Sprite[3];*/
    public Sprite ReverseSprite;
    public Sprite HalfSprite;
    public Sprite DoubleSprite;

    SpriteRenderer sr;

    public ReverseGravity_Adjust gravityZone;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gravityZone.GetCurrGravityState())
        {
            case ReverseGravity_Adjust.GravityState.Normal:

                break;
            case ReverseGravity_Adjust.GravityState.Reversed:
                sr.sprite = ReverseSprite;
                /*Destroy(mode);
                mode = Instantiate(gravityModes[0]);*/
                break;
            case ReverseGravity_Adjust.GravityState.Half:
                sr.sprite = HalfSprite;
                /*Destroy(mode);
                mode = Instantiate(gravityModes[1]);*/
                break;
            case ReverseGravity_Adjust.GravityState.Double:
                sr.sprite = DoubleSprite;
                /*Destroy(mode);
                mode = Instantiate(gravityModes[2]);*/
                break;
        }
    }
}
