using UnityEngine;

public class PlayerInput_Kim : MonoBehaviour
{
    public static float MoveInput => Input.GetAxisRaw("Horizontal");
    public static bool JumpPressed => Input.GetKeyDown(KeyCode.Space);
    public static bool TimePressed => Input.GetKeyDown(KeyCode.R);
    public static bool ReloadPressed => Input.GetKeyDown(KeyCode.E); 
    
}
