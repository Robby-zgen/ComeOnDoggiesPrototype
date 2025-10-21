using UnityEngine;
using TMPro;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    public PlayerData playerData;
    public float currentSpeed;
    private float initialSpeed;
    public bool firstTap;

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
        if (Input.GetMouseButtonDown(0))
        {
            GainBoost();
        }

        if(currentSpeed >= 0.01f)
        {
            currentSpeed -= Time.deltaTime * playerData.speedDeceleration;
            if(currentSpeed < 0f)
            {
                currentSpeed = 0f;
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
            trigger.TriggerQTE();
            Debug.Log("Trigger QTE");
        }
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
