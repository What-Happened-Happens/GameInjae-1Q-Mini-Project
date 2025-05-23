using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParadoxRecorder 
{
    private List<PlayerMovementRecord> currentMovement = new List<PlayerMovementRecord>();
    private List<PlayerAnimationRecord> currentAnimation = new List<PlayerAnimationRecord>();

    private Queue<List<PlayerMovementRecord>> movementQueue = new Queue<List<PlayerMovementRecord>>();
    private Queue<List<PlayerAnimationRecord>> animationQueue = new Queue<List<PlayerAnimationRecord>>();

    private Dictionary<GameObject, List<ObjectTrueFalseRecord>> currentTrueFalse = new();
    private Queue<Dictionary<GameObject, List<ObjectTrueFalseRecord>>> trueFalseQueue = new();

    private float lastRecordTime = 0f;

    public void Start()
    {
        currentMovement.Clear();
        currentAnimation.Clear();
        lastRecordTime = 0f;
    }

    public void Record(GameObject player, float elapsed, List<GameObject> tfObjects)
    {
        if (elapsed - lastRecordTime >= 0.1f)
        {
            currentMovement.Add(new PlayerMovementRecord(elapsed, player.transform.position));

            var animator = player.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                string state = GetAnimatorState(animator);
                currentAnimation.Add(new PlayerAnimationRecord(elapsed, state));
            }

            foreach (var obj in tfObjects)
            {
                if (obj == null) continue;
                var tf = obj.GetComponent<TrueFalse>();
                if (tf == null) continue;

                if (!currentTrueFalse.ContainsKey(obj))
                    currentTrueFalse[obj] = new List<ObjectTrueFalseRecord>();

                // 상태가 바뀐 경우만 기록 (최적화)
                var records = currentTrueFalse[obj];
                if (records.Count == 0 || records[records.Count - 1].state != tf.IsTrue)
                {
                    records.Add(new ObjectTrueFalseRecord(elapsed, tf.IsTrue));
                }
            }

            lastRecordTime = elapsed;
        }
    }

    public void Enqueue(int maxCount)
    {
        if (movementQueue.Count >= maxCount) movementQueue.Dequeue();
        if (animationQueue.Count >= maxCount) animationQueue.Dequeue();
        if (trueFalseQueue.Count >= maxCount) trueFalseQueue.Dequeue();
        
        movementQueue.Enqueue(new List<PlayerMovementRecord>(currentMovement));
        animationQueue.Enqueue(new List<PlayerAnimationRecord>(currentAnimation));
        trueFalseQueue.Enqueue(new Dictionary<GameObject, List<ObjectTrueFalseRecord>>(currentTrueFalse));
    }

    public void Clear()
    {
        movementQueue.Clear();
        animationQueue.Clear();
    }

    public void Trim(float timePassed)
    {
        movementQueue = new Queue<List<PlayerMovementRecord>>(movementQueue.Select(list =>
            list.Where(r => r.time >= timePassed)
                .Select(r => new PlayerMovementRecord(r.time - timePassed, r.position))
                .ToList()));

        animationQueue = new Queue<List<PlayerAnimationRecord>>(animationQueue.Select(list =>
            list.Where(r => r.time >= timePassed)
                .Select(r => new PlayerAnimationRecord(r.time - timePassed, r.animationState))
                .ToList()));

        trueFalseQueue = new Queue<Dictionary<GameObject, List<ObjectTrueFalseRecord>>>(
        trueFalseQueue.Select(dict => dict.ToDictionary(
        kvp => kvp.Key,
        kvp => kvp.Value
                .Where(r => r.time >= timePassed)
                .Select(r => new ObjectTrueFalseRecord(r.time - timePassed, r.state))
                .ToList()
    ))
);
    }

    public List<List<PlayerMovementRecord>> GetAllMovementData() => new List<List<PlayerMovementRecord>>(movementQueue);
    public List<List<PlayerAnimationRecord>> GetAllAnimationData() => new List<List<PlayerAnimationRecord>>(animationQueue);
    public List<Dictionary<GameObject, List<ObjectTrueFalseRecord>>> GetAllTrueFalseData() => new List<Dictionary<GameObject, List<ObjectTrueFalseRecord>>>(trueFalseQueue);

    private string GetAnimatorState(Animator animator)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return "Idle";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walking")) return "Walking";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping")) return "Jumping";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt")) return "Hurt";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fall")) return "Fall";
        return "Unknown";
    }
}
