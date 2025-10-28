using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("---Base Speed & Control---")]
    [Tooltip("tambah kecepatan setiap tap")]
    public float tapSpeedGain;

    [Tooltip("pengurang kecepatan jika tidak di tap")]
    public float speedDeceleration;

    [Tooltip("maximal kecepatan player")]
    public float maxSpeed;

    [Tooltip("waktu toleransi sejak terakhir tap layar")]
    public float tapTolerance;

    [Header("---Character Condition Modifiers (Initial Tap)---")]
    [Tooltip("kecepatan kalau lagi happy")]
    public float happySpeed;

    [Tooltip("kecepatan kalau lagi normal")]
    public float normalSpeed;

    [Tooltip("kecepatan kalau lagi exhaust")]
    public float exhaustSpeed;

    [Header("---QTE / During on obstacle---")]
    [Tooltip("tambahan kecepatan kalau QTE sukses")]
    public float qteSuccessBoost;

    [Tooltip("pengurangan kecepatan kalau QTE gagal")]
    public float qteFailurePenalty;

    [Tooltip("waktu toleransi setelah obstacle")]
    public float postObstacleDuration;

    [Tooltip("kecepatan selama di obtacle")]
    public float speedDuringObstacle;
}
