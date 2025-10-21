using UnityEngine;

public class ButtonQTE : MonoBehaviour
{
    public QTE qte;
    public RectTransform outerCircle;  // Lingkaran luar yang mengecil
    public RectTransform targetCircle; // Lingkaran target di tengah

    private float shrinkDuration;

    private float time;
    private bool isActive;

    private PlayerMovement player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (!isActive) return;

        time += Time.deltaTime;
        float t = time / shrinkDuration;
        outerCircle.localScale = Vector3.Lerp(Vector3.one * 3f, Vector3.one, t);
        
        if (time >= shrinkDuration)
        {
            EndQTE(false);
        }
    }

    void OnEnable()// ketika QTE ke enable langsung jalankan QTEnya
    {
        StartQTE();
    }

    public void OnClick()
    {
        if (!isActive) return;

        float distance = Vector3.Distance(outerCircle.localScale, targetCircle.localScale);
        if (distance < 1f)// cek jarak kedua lingkaran
        {
            EndQTE(true);
        }
        else
        {
            EndQTE(false);
        }
    }
    private void StartQTE()
    {
        time = 0;
        isActive = true;
        outerCircle.localScale = Vector3.one * 3f;

        bool sameCondition = GameManager.instance.isSpecialityMatchingMap;
        if (sameCondition)
        {
            shrinkDuration = qte.slowDuration;
            Debug.Log("Karena sama jadi pelan " + shrinkDuration + " Detik");
        }
        else
        {
            shrinkDuration = qte.fastDuration;
            Debug.Log("Karena beda jadi cepat" + shrinkDuration + " Detik");
        }
    }

    private void EndQTE(bool success)
    {
        isActive = false;
        if (success)
        {
            // Kalau sukses tambah kecepatan pemain
            player.currentSpeed += player.playerData.qteSuccessBoost;
            Debug.Log("NICE");
        }

        else
        {
            // Kalau gagal kurangi kecepatan pemain
            player.currentSpeed -= player.playerData.qteFailurePenalty;
            Debug.Log("MISSED");
        }

        gameObject.SetActive(false);
        outerCircle.gameObject.SetActive(false);
    }
}
