using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour
{
    // ��������Ʈ ������. �������� ����� �� Ȯ���ؼ� 
    [Header("SpriteRenderer Script")]
    [SerializeField] private Sprite fullTimer;
    [SerializeField] private Sprite fourTimer;
    [SerializeField] private Sprite thirdTimer;
    [SerializeField] private Sprite secondTimer;
    [SerializeField] private Sprite emptyTimer;

    [Header("Image Sprite")]
    [SerializeField] private Image timerPrefab; 

    [Header("SpriteContaiverTransform")]
    public Transform timerContainer;
   
    private List<Image> timers = new();
    private float StartTime = Time.time;
    public void UpdateSprites(bool isCloneCreated, float time)
    {
        Debug.Assert(isCloneCreated , $"Ŭ���� ����������ϴ�! isCloneCreated �� :  {isCloneCreated}");

        if (isCloneCreated && timers.Count == 0) // Ŭ���� ��������ٸ�, 
        {
            Image timerSprite = Instantiate(timerPrefab, timerContainer);
            timers.Add(timerSprite);
        }

        int i = 0; 

            while (i < timers.Count)
            {
            if (time > 5f)           // 5�� ����
                timers[i].sprite = fullTimer;
            else if (time > 4f)      // 4�� ����
                timers[i].sprite = thirdTimer;
            else if (time > 3f)      // 3�� ����
                timers[i].sprite = thirdTimer;
            else if (time > 2f)      // 2�� ����
                timers[i].sprite = secondTimer;
            else                     // 1�� ����
                timers[i].sprite = emptyTimer;
        }    
               
    }
    // duration �� ���޵� ����ŭ �ð��� ������ �� Ȯ�� 
    public bool IsElapsed(float duration) 
    {
        return (Time.time - StartTime) >= duration;
    }
    public float GetDeltaTime()
    {
        return Time.deltaTime;
    }

}
