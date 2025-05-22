using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityParticle : MonoBehaviour
{
    public ReverseGravity_Adjust gravityZone;

    //파티클을 받을 프리팹
    public GameObject ReverseParticlePrefab;
    public GameObject HalfParticlePrefab;
    public GameObject DoubledParticlePrefab;

    GameObject reverseparticle;
    GameObject halfparticle;
    GameObject doubleparticle;

    public float particleScale = 1.65f;
    // Start is called before the first frame update
    void Start()
    {
        reverseparticle = Instantiate(ReverseParticlePrefab, transform.position, transform.rotation);
        halfparticle = Instantiate(HalfParticlePrefab, transform.position, transform.rotation);
        doubleparticle = Instantiate(DoubledParticlePrefab, transform.position, transform.rotation);
        
        reverseparticle.transform.localScale = new Vector3(particleScale, particleScale, particleScale);
        halfparticle.transform.localScale = new Vector3(particleScale, particleScale, particleScale);
        doubleparticle.transform.localScale = new Vector3(particleScale, particleScale, particleScale);
    }

    // Update is called once per frame
    void Update()
    {
            ChangeParticle();
    }
    void ChangeParticle()
    {
        switch (gravityZone.GetCurrGravityState())
        {
            case ReverseGravity_Adjust.GravityState.Normal:
                reverseparticle.SetActive(false);
                halfparticle.SetActive(false);
                doubleparticle.SetActive(false);
                break;
            case ReverseGravity_Adjust.GravityState.Reversed:
                reverseparticle.SetActive(true);
                halfparticle.SetActive(false);
                doubleparticle.SetActive(false);
                break;
            case ReverseGravity_Adjust.GravityState.Half:
                reverseparticle.SetActive(false);
                halfparticle.SetActive(true);
                doubleparticle.SetActive(false);
                break;
            case ReverseGravity_Adjust.GravityState.Double:
                reverseparticle.SetActive(false);
                halfparticle.SetActive(false);
                doubleparticle.SetActive(true);
                break;
        }
    }
}
