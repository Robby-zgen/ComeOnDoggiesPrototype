using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CountDown : MonoBehaviour
{
    private float currentTime;
    private float startingTime = 3;
    public TextMeshProUGUI countdownText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countdownText.gameObject.SetActive(true);
        currentTime = startingTime;    
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if(currentTime <= 1)
            countdownText.text = currentTime.ToString("GO");
        if (currentTime <=0)
        {
            GameManager.instance.startPlay = true;
            currentTime = 0;
            countdownText.gameObject.SetActive(false);
        }
    }

}
