using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("---Base Speed & Control---")]
    public float tapSpeedGain;
    public float speedDeceleration;

    [Header("---Character Condition Modifiers (Initial Tap---")]
    public float happySpeed;
    public float normalSpeed;
    public float exhaustSpeed;

    [Header("---QTE Results Effect---")]
    public float qteSuccessBoost;
    public float qteFailurePenalty;


}
