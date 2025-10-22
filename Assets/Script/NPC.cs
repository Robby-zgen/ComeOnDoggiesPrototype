using UnityEngine;
using UnityEngine.Rendering;

public class NPC : MonoBehaviour
{
    public NPCData npc;

    public float currentSpeed;// kecepatan sekarang

    private float speedChangeAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //buat kecepatan random diawal
        speedChangeAmount = Random.Range(npc.initialRandomSpeedMin, npc.initialRandomSpeedMax);
        currentSpeed += speedChangeAmount;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(currentSpeed < npc.baseNormalSpeed)
        {
            currentSpeed += npc.accelerationRate * Time.deltaTime;
        }
        transform.position += Vector3.right * currentSpeed  * Time.deltaTime;
        if(currentSpeed < 0)
        {
            currentSpeed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Trigger"))
        {
            if (Random.value < 0.5f)
            {
                speedChangeAmount = Random.Range(npc.minRandomIncrease, npc.maxRandomIncrease);
                currentSpeed += speedChangeAmount * Time.deltaTime;
            }
            else
            {
                speedChangeAmount = Random.Range(npc.minRandomDecrease, npc.maxRandomDecrease);
                currentSpeed -= speedChangeAmount * Time.deltaTime;
            }            
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
