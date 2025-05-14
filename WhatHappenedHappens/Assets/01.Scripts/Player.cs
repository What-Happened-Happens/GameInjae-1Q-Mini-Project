using Script.Gimmick;
using Script.UI;
using UnityEngine;

namespace Script.Player
{
    public class Player : MonoBehaviour
    {
        private PlayerMovement _movement;
        private PlayerJump _jump;
        private PlayerInput _input;
        private PlayerHealth _playerHealth;

        private HeartUI _heartUI;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _jump = GetComponent<PlayerJump>();
            _input = GetComponent<PlayerInput>();
            _playerHealth = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            _movement.Move(PlayerInput.MoveInput);
        
            if(PlayerInput.JumpPressed && _jump.CanJump)
                _jump.Jump();
        }
        
        private static void Die()
        {
            Debug.Log("Player Die!");
        }

        // private void OnTriggerStay2D(Collider2D col)
        // {
        //     if (col.CompareTag($"Charger") && col.TryGetComponent<Charger>(out var charger))
        //     {
        //         charger.UpdateCharge(Time.deltaTime);
        //
        //         if (TryGetComponent<PlayerHealth>(out var playerHealth))
        //         {
        //             playerHealth.AddHealth(charger.ChargeForce);
        //         }
        //     }
        // }
    }
}

