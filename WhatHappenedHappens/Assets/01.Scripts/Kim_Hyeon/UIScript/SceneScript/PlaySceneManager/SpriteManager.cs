using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour
{
    // 스프라이트 렌더러. 아이템을 얻었는 지 확인해서 
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
        Debug.Assert(isCloneCreated , $"클론이 만들어졌습니다! isCloneCreated 값 :  {isCloneCreated}");

        if (isCloneCreated && timers.Count == 0) // 클론이 만들어졌다면, 
        {
            Image timerSprite = Instantiate(timerPrefab, timerContainer);
            timers.Add(timerSprite);
        }

        int i = 0; 

            while (i < timers.Count)
            {
            if (time > 5f)           // 5초 구간
                timers[i].sprite = fullTimer;
            else if (time > 4f)      // 4초 구간
                timers[i].sprite = thirdTimer;
            else if (time > 3f)      // 3초 구간
                timers[i].sprite = thirdTimer;
            else if (time > 2f)      // 2초 구간
                timers[i].sprite = secondTimer;
            else                     // 1초 이하
                timers[i].sprite = emptyTimer;
        }    
               
    }
    // duration 에 전달된 값만큼 시간이 지났는 지 확인 
    public bool IsElapsed(float duration) 
    {
        return (Time.time - StartTime) >= duration;
    }
    public float GetDeltaTime()
    {
        return Time.deltaTime;
    }

}
