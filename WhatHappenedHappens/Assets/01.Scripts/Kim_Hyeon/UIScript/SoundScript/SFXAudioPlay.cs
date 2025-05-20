using UnityEngine;

public class SFXAudioPlay : SFXAudioManager
{
    [Header("AudioSource")]
    [SerializeField] private AudioSource audioSource;

    [Header("Short or Long Threshold")]
    [SerializeField] private float shortThreshold = 0.5f; 

    public void ClickEventAudioPlay(float MouseInput)
    {

    }
    public void ObjectAudioPlay(float MouseInput)
    {
       
    }


}
