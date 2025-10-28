using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class PlayerMovement : MonoBehaviour
{
    public PlayerData playerData;

    [SerializeField] private float currentSpeed;
    public float savedSpeedBeforeQTE;
    [SerializeField] private float initialSpeed;
    private float postObstacleEndTime;
    private float lastTap;

    private bool firstTap = false;
    private bool onObstacle = false;
    private bool postObstacle = false;

    private DataChar dataChar;
    public QTETrigger trigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataChar = GameManager.instance.selectedCharacterData;
        CheckCondition();

        currentSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * currentSpeed * Time.deltaTime;
        if (!GameManager.instance.startPlay) return;

        if (!onObstacle)// jika lagi tidak di obstacle
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastTap = Time.time;
                GainBoost();
            }
            if (postObstacle)
            {
                if(Time.time >= postObstacleEndTime)
                {
                    postObstacle = false;
                }
            }

            if (!postObstacle)
            {
                if (Time.time > lastTap + playerData.tapTolerance)
                {
                    currentSpeed -= playerData.speedDeceleration * Time.deltaTime ;
                    if (currentSpeed <= 0f)
                    {
                        currentSpeed = 0f;
                    }
                }
            }
        }
    }

    private void GainBoost()
    {
        if (!firstTap)
        {
            currentSpeed = initialSpeed;
            firstTap = true;
        }
        else
        {
            currentSpeed += playerData.tapSpeedGain;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        onObstacle = true;
        savedSpeedBeforeQTE = currentSpeed;
        currentSpeed = playerData.speedDuringObstacle;
        trigger.TriggerQTE();
    }

    public void ExitObstacle()
    {
        onObstacle = false;
        postObstacleEndTime = Time.time + playerData.postObstacleDuration;
        postObstacle = true;
        currentSpeed = savedSpeedBeforeQTE;
    }

    private void CheckCondition()
    {
        if (dataChar.condition == Condition.Happy)
            initialSpeed = playerData.happySpeed;

        else if (dataChar.condition == Condition.Normal)
            initialSpeed = playerData.normalSpeed;

        else if (dataChar.condition == Condition.Exhaust)
            initialSpeed = playerData.exhaustSpeed;
    }
}
