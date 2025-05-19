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

        // �۸�ġ ���� ����
        string glitchAnimName = "Glitch";
        float glitchDuration = 0.5f;
        bool glitchPlayedAtStart = false;
        bool glitchPlayedAtSecond = false;
        float glitchTimer = 0f;
        bool isGlitching = false;

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

                // ��ġ ������Ʈ
                ghost.transform.position = Vector3.Lerp(start, end, elapsed / waitTime);

                elapsed += Time.deltaTime;
                elapsedTotal += Time.deltaTime;

                // 1. ù �۸�ġ (0.4����)
                if (!glitchPlayedAtStart && elapsedTotal >= 0.4f)
                {
                    if (animator != null)
                    {
                        animator.Play(glitchAnimName);
                        glitchPlayedAtStart = true;
                        isGlitching = true;
                        glitchTimer = glitchDuration;
                    }
                }

                // 2. �� ��° �۸�ġ (1.5����)
                if (!glitchPlayedAtSecond && elapsedTotal >= 1.5f)
                {
                    if (animator != null)
                    {
                        animator.Play(glitchAnimName);
                        glitchPlayedAtSecond = true;
                        isGlitching = true;
                        glitchTimer = glitchDuration;
                    }
                }

                // �۸�ġ Ÿ�̸� ó��
                if (isGlitching)
                {
                    glitchTimer -= Time.deltaTime;
                    if (glitchTimer <= 0f)
                    {
                        isGlitching = false;
                        if (animator != null && !string.IsNullOrEmpty(lastPlayedAnim))
                            animator.Play(lastPlayedAnim);
                    }
                }

                // �Ϲ� �ִϸ��̼� ó�� (�۸�ġ �ƴ� ����)
                if (!isGlitching && animIndex < animData.Count && animData[animIndex].time <= elapsedTotal)
                {
                    if (animator != null)
                    {
                        string nextAnim = animData[animIndex].animationState;
                        if (lastPlayedAnim != nextAnim)
                        {
                            animator.Play(nextAnim);
                            lastPlayedAnim = nextAnim;
                        }
                    }
                    animIndex++;
                }

                // Ÿ�̸� ����
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