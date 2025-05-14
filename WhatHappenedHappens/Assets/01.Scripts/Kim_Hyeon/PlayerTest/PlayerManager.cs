using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerMovement _movement;
    private PlayerJump _jump;

    //Test Code 
    private UIManager _uiManager;

    private void Start()
    {
      _input = GetComponent<PlayerInput>();
      _movement = GetComponent<PlayerMovement>();
      _jump = GetComponent<PlayerJump>();

      //Test Code UI 
      _uiManager = GetComponent<UIManager>(); 
    }

    private void Update()
    {
        _movement.Move(PlayerInput.MoveInput);

        if(_jump.CanJump && PlayerInput.JumpPressed){
            _jump.Jump(); 
        }

        _uiManager.IsGetCardKey = true; 
        bool isGetCardKey = _uiManager.IsGetCardKey;

        _uiManager.CardKeyUISetActive(isGetCardKey); 
    }
}
