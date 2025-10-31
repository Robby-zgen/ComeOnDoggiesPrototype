using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCData npc;

    public float currentSpeed;
    private Animator animator;
    private float speedChangeAmount;
    private float targetSpeed;
    private bool isAdjustingSpeed = false;
    private bool onObstacle;

    private float savedSpeedBeforeQTE;
    
    private float adjustmentStartTime; 
    private float speedBeforeAdjustment;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        //buat kecepatan random diawal
        speedChangeAmount = Random.Range(npc.initialRandomSpeedMin, npc.initialRandomSpeedMax);
        currentSpeed += speedChangeAmount;

        targetSpeed = currentSpeed;

    }
    
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.startPlay) return;
        transform.position += Vector3.right * currentSpeed * Time.deltaTime;

        if (!onObstacle)
        {
            if (isAdjustingSpeed)
            {
                float timeElapsed = Time.time - adjustmentStartTime;
                float t = timeElapsed / npc.randomChangeDuration;

                currentSpeed = Mathf.Lerp(speedBeforeAdjustment, targetSpeed, t);

                if (t >= 1.0f)
                {
                    currentSpeed = targetSpeed;
                    isAdjustingSpeed = false;
                }
            }
            else
            {
                currentSpeed += npc.accelerationRate * Time.deltaTime;
                targetSpeed = currentSpeed;
            }
        }
        if (currentSpeed <= 0)
        {
            animator.SetBool("isRun", false);
            currentSpeed = 0;
        }
        else
        {
            animator.SetBool("isRun", true);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trigger"))
        {
            float randomSpeedChange;

            if (Random.value < 0.9f)
            {
                randomSpeedChange = Random.Range(npc.minRandomIncrease, npc.maxRandomIncrease);
               //Debug.Log("kecepatan kena trigger: " + currentSpeed);
               //Debug.Log("tambah kecepatan sebesar: " + randomSpeedChange);
            }
            else
            {
                randomSpeedChange = Random.Range(npc.minRandomDecrease, npc.maxRandomDecrease);
                //Debug.Log("kecepatan kena trigger: " + currentSpeed);
                //Debug.Log("kurangi kecepatan sebesar: " + randomSpeedChange);
            }

            targetSpeed = currentSpeed + randomSpeedChange;
            //Debug.Log("kecepatan akan menjadi " + targetSpeed + " dalam " + npc.randomChangeDuration);

            targetSpeed = Mathf.Clamp(targetSpeed, 0f, npc.maxSpeed); //npc.maxSpeed * 5f);

            speedBeforeAdjustment = currentSpeed;
            adjustmentStartTime = Time.time;
            isAdjustingSpeed = true;
        }

        if (collision.gameObject.CompareTag("QTE"))
        {
            EnterObstacle();
        }
        if (collision.gameObject.CompareTag("QTE Off"))
        {
            ExitObstacle();
        }
    }

    public void EnterObstacle()
    {
        isAdjustingSpeed = false;
        onObstacle = true;
        savedSpeedBeforeQTE = currentSpeed;
        currentSpeed = npc.speedDuringObstacle;
    }

    public void ExitObstacle()
    {
        float speedAfterObstacle;
        if (Random.value < npc.qteFailChance)
        {
            speedAfterObstacle = Random.Range(npc.minQtePenalty, npc.maxQtePenalty); 
            savedSpeedBeforeQTE -= speedAfterObstacle;
            Debug.Log("pengurangan speed setelah QTE: " + speedAfterObstacle);
        }
        else
        {
            speedAfterObstacle = Random.Range(npc.minQteBoost, npc.maxQteBoost);
            savedSpeedBeforeQTE += speedAfterObstacle;
            Debug.Log("penambahana speed setelah QTE: " + speedAfterObstacle);
        }
        targetSpeed = savedSpeedBeforeQTE; // Kecepatan yang dituju
        speedBeforeAdjustment = currentSpeed; // Kecepatan saat ini (speedDuringObstacle)
        adjustmentStartTime = Time.time;
        isAdjustingSpeed = true;

        //currentSpeed = savedSpeedBeforeQTE;
        Debug.Log("speed sekarang jadi: " + currentSpeed  );    
        onObstacle = false;
    }
}
