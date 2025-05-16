using UnityEngine;


public class SpriteManager : MonoBehaviour
{
    [SerializeField] private GetItemUI _getItemUI;

    private void Start()
    {
      if (_getItemUI == null)  _getItemUI = GetComponent<GetItemUI>();
    }

    private void Update()
    {

    }

   

}
