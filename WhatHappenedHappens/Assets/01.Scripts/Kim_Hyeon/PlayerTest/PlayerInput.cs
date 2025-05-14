using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static float MoveInput => Input.GetAxisRaw("Horizontal");
    public static bool JumpPressed => Input.GetKeyDown(KeyCode.Space);
}
