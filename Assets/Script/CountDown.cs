using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CountDown : MonoBehaviour
{
    private float currentTime;
    private float startingTime = 3.99f;
    public TextMeshProUGUI countdownText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (GameManager.instance != null)
        {
            GameManager.instance.startPlay = false; 
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            currentTime = startingTime;

            enabled = true;
            UpdateCountdownDisplay(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        UpdateCountdownDisplay();
    }

    void UpdateCountdownDisplay()
    {
        if (currentTime > 1f)
        {
            countdownText.text = Mathf.CeilToInt(currentTime - 1f).ToString();
        }
        else if (currentTime > 0f)
        {
            countdownText.text = "GO";
        }
        else
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.startPlay = true;
            }
            currentTime = 0;
            countdownText.gameObject.SetActive(false);
            enabled = false;
        }
    }

}
