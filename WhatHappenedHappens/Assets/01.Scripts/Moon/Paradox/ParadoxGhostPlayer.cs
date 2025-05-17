using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParadoxGhostPlayer 
{
    public IEnumerator Replay(GameObject ghost, List<PlayerMovementRecord> moveData, List<PlayerAnimationRecord> animData, TextMeshPro timerText, System.Action onComplete)
    {
        SpriteRenderer sr = ghost.GetComponentInChildren<SpriteRenderer>();
        Animator animator = ghost.GetComponentInChildren<Animator>();

        float totalDuration = moveData[moveData.Count - 1].time;
        float elapsedTotal = 0f;
        int animIndex = 0;
        string lastPlayedAnim = "";

        for (int i = 1; i < moveData.Count; i++)
        {
            float waitTime = moveData[i].time - moveData[i - 1].time;
            Vector3 start = moveData[i - 1].position;
            Vector3 end = moveData[i].position;

            if (sr != null)
                sr.flipX = end.x < start.x;

            float elapsed = 0f;
            while (elapsed < waitTime)
            {
                if (ghost == null) yield break;

                ghost.transform.position = Vector3.Lerp(start, end, elapsed / waitTime);
                elapsed += Time.deltaTime;
                elapsedTotal += Time.deltaTime;

                if (animIndex < animData.Count && animData[animIndex].time <= elapsedTotal)
                {
                    string nextAnim = animData[animIndex].animationState;
                    if (lastPlayedAnim != nextAnim)
                    {
                        animator?.Play(nextAnim);
                        lastPlayedAnim = nextAnim;
                    }
                    animIndex++;
                }

                if (timerText != null)
                    timerText.text = ((int)Mathf.Max(0, totalDuration - elapsedTotal)).ToString("D2");

                yield return null;
            }

            ghost.transform.position = end;
        }

        if (ghost != null)
            GameObject.Destroy(ghost);

        onComplete?.Invoke();
    }
}
