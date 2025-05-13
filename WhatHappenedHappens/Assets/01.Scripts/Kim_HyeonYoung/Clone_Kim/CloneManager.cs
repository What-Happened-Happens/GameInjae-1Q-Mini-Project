using System.Collections.Generic;
using UnityEngine;
public class CloneManager : MonoBehaviour
{
    [Header("Clone Manager")] 
    [SerializeField] private GameObject ClonePrefab;    // Clone Prefab 
    [SerializeField] private Transform targetTransform; // Flower Prefab 

    private GameObject cloneObj;
    private const int maxCloneCount = 3;
    // test code 
    public bool CloneCreateCount => clones.Count < maxCloneCount;
    private  List<GameObject> clones = new List<GameObject>(maxCloneCount);
    
    // real code 
    public bool IsCloneCreated { get; set; } = true;  // Clone Created 
    public bool IsCloneFinished { get; set; } = false; // Clone Finished

    public GameObject CreateClone()
    {
        if (!IsCloneCreated) return null;

        cloneObj = Instantiate(ClonePrefab, targetTransform.position, Quaternion.identity);
        clones.Add(cloneObj);

        if (clones.Count >= maxCloneCount)
        {
            IsCloneCreated = false;
        }

        return cloneObj; // Ensure a return statement is present in all code paths
    }

    public void RemoveClone()
    {
        if (clones.Count >= maxCloneCount)
        {
            Debug.Log($"최대 클론 개수({maxCloneCount}) 도달, 리셋합니다.");
            foreach (var clone in clones)
            {
                if (clone != null) 
                    Destroy(clone);
            }
            clones.Clear();
            IsCloneCreated = true;
            return; 
        }  
    }

    


}
