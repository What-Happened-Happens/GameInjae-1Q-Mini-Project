using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [ �з����� �׼� ���� Enum ] : �߰��� �׼� ���⿡ �������ֱ� ! 
public enum ActionType 
{ 
    MoveBox,
    MoveBall
}


// [ �ð� ��� + ���� �ڵ� ���� �̺�Ʈ ���� ]
// 
public class ParadoxEvent 
{
    public float time;
    public GameObject target;
    public ActionType action;

    public ParadoxEvent(float time, GameObject target, ActionType action)
    {
        this.time = time;
        this.target = target;
        this.action = action;
    }

    public void Execute()
    {
        switch (action)
        {
            case ActionType.MoveBox:
                target.GetComponent<Box>().MoveRight();
                Debug.Log($"[Paradox] �ڽ� �̵� ���� at {time}s");
                break;
            case ActionType.MoveBall:
                target.GetComponent<Ball>().MoveUp();
                Debug.Log($"[Paradox] �� �̵� ���� at {time}s");
                break;
        }
    }
}
