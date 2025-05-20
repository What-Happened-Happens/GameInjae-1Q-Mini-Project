using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGravityState : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite[] sprites = new Sprite[3];
    SpriteRenderer sr;
    public ReverseGravity gravityZone;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gravityZone.GetCurrGravityState())
        {
            case ReverseGravity.GravityState.Normal:
                break;
            case ReverseGravity.GravityState.Reversed:
                sr.sprite = sprites[0];
                /*Destroy(mode);
                mode = Instantiate(gravityModes[0]);*/
                break;
            case ReverseGravity.GravityState.Weakened:
                sr.sprite = sprites[1];
                /*Destroy(mode);
                mode = Instantiate(gravityModes[1]);*/
                break;
            case ReverseGravity.GravityState.Reinforced:
                sr.sprite = sprites[2];
                /*Destroy(mode);
                mode = Instantiate(gravityModes[2]);*/
                break;
        }
    }
}
