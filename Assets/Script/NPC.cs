using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCData npc;

    public float currentSpeed;// kecepatan sekarang

    private float speedChangeAmount;

    private float targetSpeed;
    private bool isAdjustingSpeed = false;
    public float adjustmentRate = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //buat kecepatan random diawal
        speedChangeAmount = Random.Range(npc.initialRandomSpeedMin, npc.initialRandomSpeedMax);
        currentSpeed += speedChangeAmount;

        targetSpeed = currentSpeed;

    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.startPlay)
        {
            if (isAdjustingSpeed)
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, adjustmentRate * Time.deltaTime);

                if (currentSpeed == targetSpeed)
                {
                    isAdjustingSpeed = false;
                }
            }
            else
            {
                currentSpeed += npc.accelerationRate * Time.deltaTime;
            }
            transform.position += Vector3.right * currentSpeed * Time.deltaTime;
            if (currentSpeed < 0)
            {
                currentSpeed = 0;
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trigger"))
        {
            float randomSpeedChange;
            
            if (Random.value < 0.5f)
            {
                randomSpeedChange = Random.Range(npc.minRandomIncrease, npc.maxRandomIncrease);
                
            }
            else
            {
                randomSpeedChange = Random.Range(npc.minRandomDecrease, npc.maxRandomDecrease);
            }

            targetSpeed = currentSpeed + randomSpeedChange;

            targetSpeed = Mathf.Clamp(targetSpeed, 0f, npc.baseNormalSpeed * 3f); // 1.5x buffer

            // 3. Aktifkan Flag Penyesuaian Linear
            isAdjustingSpeed = true;
        }

        if (collision.gameObject.CompareTag("QTE"))
        {
            float randomValue = Random.value;

            if (randomValue < npc.qteFailChance)
            {
                speedChangeAmount = Random.Range(npc.minQtePenalty, npc.maxQtePenalty);
                currentSpeed -= speedChangeAmount;
            }
        }
    }
}
