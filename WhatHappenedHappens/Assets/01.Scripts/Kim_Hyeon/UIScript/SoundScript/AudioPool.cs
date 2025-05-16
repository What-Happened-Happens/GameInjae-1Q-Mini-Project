using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPool : MonoBehaviour
{
   public static SoundManager Instance { get; private set; }

    [Tooltip("SoundStart���� ������ �⺻ ����� ������")]
    public GameObject audioPrefab;
    [Tooltip("���� ������ Ǯ ������")]
    public int initialPoolSize = 10;

    private Queue<AudioSource> pool = new Queue<AudioSource>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Ǯ �ʱ�ȭ
        for (int i = 0; i < initialPoolSize; i++)
            pool.Enqueue(CreateNewAudioSource());
    }
    private AudioSource CreateNewAudioSource()
    {
        var go = Instantiate(audioPrefab, transform);
        go.SetActive(false);

        return go.GetComponent<AudioSource>();
    }

    public AudioSource Get()
    {
        AudioSource audioSrc;
        if (pool.Count > 0)
            audioSrc = pool.Dequeue();
        else
            audioSrc = CreateNewAudioSource();

        audioSrc.gameObject.SetActive(true);
        return audioSrc;
    }

    public void Return(AudioSource audioSrc)
    {
        audioSrc.Stop();
        audioSrc.clip = null;
        audioSrc.loop = false;
        audioSrc.gameObject.SetActive(false);
        pool.Enqueue(audioSrc);
    }

}
