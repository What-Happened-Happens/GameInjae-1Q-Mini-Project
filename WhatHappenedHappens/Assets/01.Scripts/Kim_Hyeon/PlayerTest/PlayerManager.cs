using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerMovement _movement;
    private PlayerJump _jump;

    private void Start()
    {
      _input = GetComponent<PlayerInput>();
      _movement = GetComponent<PlayerMovement>();
      _jump = GetComponent<PlayerJump>();
    }

    private void Update()
    {
        _movement.Move(PlayerInput.MoveInput);

        if(_jump.CanJump && PlayerInput.JumpPressed)
        {
            _jump.Jump(); 
        }
    }
}
