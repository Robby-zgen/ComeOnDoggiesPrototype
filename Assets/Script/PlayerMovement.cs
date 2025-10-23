    using UnityEngine;
    using TMPro;
    using System.Collections;
    using System.Threading;


    public class PlayerMovement : MonoBehaviour
    {
        public PlayerData playerData;
        public float currentSpeed;
        private float initialSpeed;

        public bool firstTap;
        public bool onObstacle;

        private DataChar dataChar;

        public QTETrigger trigger;
        public float lastTap;
        private float idleTimeThreshold = 0.5f;


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
            if (!onObstacle)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    lastTap = Time.time;
                    GainBoost();
                }
                if (Time.time > lastTap + idleTimeThreshold)
                {
                    if (currentSpeed > 0f)
                    {
                        currentSpeed = 0f;
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
                onObstacle = true;
                trigger.TriggerQTE();
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
