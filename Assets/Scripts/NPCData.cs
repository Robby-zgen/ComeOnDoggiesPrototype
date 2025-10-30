using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Scriptable Objects/NPCData")]
public class NPCData : ScriptableObject
{
    [Header("Base Speed & Limits")]
    [Tooltip("penambahan kecepatan secara normal, saat NPC tidak sedang dalam penambahan atau pengurangan gara gara trigger kecepatan.")]
    public float accelerationRate;
    [Tooltip("Batas kecepatan maksimum.")]
    public float maxSpeed;
    [Tooltip("Durasi waktu (dalam detik) yang dibutuhkan NPC untuk menyesuaikan kecepatan setelah Trigger atau QTE.")]
    public float randomChangeDuration;

    [Header("Initial Random Speed Range")]
    [Tooltip("Kecepatan minimum acak yang ditambahkan saat game dimulai.")]
    public float initialRandomSpeedMin;
    [Tooltip("Kecepatan maksimum acak yang ditambahkan saat game dimulai.")]
    public float initialRandomSpeedMax;

    [Header("RANDOM SPEED CHANGE (TRIGGER)")]
    [Tooltip("penambahan minimum ketika kena trigger")]
    public float minRandomIncrease;
    [Tooltip("penambahan maksimum ketika kena trigger")]
    public float maxRandomIncrease;
    [Tooltip("pengurangan minimum ketika kena trigger")]
    public float minRandomDecrease;
    [Tooltip("pengurangan maksimum ketika kena trigger")]
    public float maxRandomDecrease;

    

    [Header("OBSTACLE / QTE PARAMETERS")]
    [Tooltip("Kecepatan yang digunakan NPC saat berada di dalam area Obstacle/QTE.")]
    public float speedDuringObstacle;
    [Tooltip("Peluang kegagalan QTE (0.0 = 0% gagal, 1.0 = 100% gagal).")]
    [Range(0f, 1f)]
    public float qteFailChance;
    [Tooltip("pengurangan minimum ketika NPC GAGAL QTE.")]
    public float minQtePenalty;
    [Tooltip("pengurangan maksimum ketika NPC GAGAL QTE.")]
    public float maxQtePenalty;
    [Tooltip("penambahan minimum ketika NPC BERHASIL QTE.")]
    public float minQteBoost;
    [Tooltip("penambahan maksimum ketika NPC BERHASIL QTE.")]
    public float maxQteBoost;

    

    
}
