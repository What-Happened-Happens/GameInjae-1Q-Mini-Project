using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGravityState_Adjust : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite[] sprites = new Sprite[3];
    public GameObject[] gravitySteps = new GameObject[3];
    GameObject currentGravitySteps;
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
                break;
            case ReverseGravity.GravityState.Weakened:
                sr.sprite = sprites[1];
                break;
            case ReverseGravity.GravityState.Reinforced:
                sr.sprite = sprites[2];
                break;
        }
    }
}
