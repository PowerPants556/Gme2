using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerData", order = 3)]
public class PlayerData : ScriptableObject
{
    public float maxHealth;
    public float maxStamina;
    public float baseWalkSpeed;
    public float baseSprintSpeed;
    public float mouseSensetivity;
    public float baseJumpForce;
}
