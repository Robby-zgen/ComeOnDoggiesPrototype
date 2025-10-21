using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Scriptable Objects/NPCData")]
public class NPCData : ScriptableObject
{
    [Header("Base Speed & Limits")]
    public float accelerationRate;
    public float baseNormalSpeed;


    [Header("Speed Boost/Reduce Ranges (Trigger)")]
    public float minRandomIncrease;
    public float maxRandomIncrease;

    public float minRandomDecrease;
    public float maxRandomDecrease;

    [Header("Initial Random Speed Range")]
    public float initialRandomSpeedMin;
    public float initialRandomSpeedMax;

    [Header("NPC QTE Failure Parameters")]
    public float qteFailChance;
    public float minQtePenalty;
    public float maxQtePenalty;

}
