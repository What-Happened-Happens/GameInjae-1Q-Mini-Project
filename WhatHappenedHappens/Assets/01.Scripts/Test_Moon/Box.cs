using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Vector3 startPosition;
    public float moveDistance = 2f;
    public float moveDuration = 1f;

    private void Start()
    {
        startPosition = transform.position;
    }

    public void MoveRight()
    {
        StopAllCoroutines();
        StartCoroutine(MoveSmoothly());
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }

    // [ 해당 부분은 애니메이션으로 대체할 가능성 있음 ]
    private IEnumerator MoveSmoothly()
    {
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.right * moveDistance;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
    }
}
