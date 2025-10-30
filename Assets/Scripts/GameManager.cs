using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public DataChar[] allDataChars; // nyimpen semua data karakter di scene ingame
    public int selectedCharacterIndex = -1;
    public DataChar selectedCharacterData;// nyimpen data karakter player yang dimainkan

    public MapData mapData;
    public int mapIndex;


    public int points;
    public int pointWin;
    public int pointLose;
    public TextMeshProUGUI pointText;

    public TextMeshProUGUI winLoseText;
    public GameObject panelWinLose;
    public GameObject panelTutorial;

    public bool isSpecialityMatchingMap;
    public bool gameEnded;
    public bool hasPlayedBefore;
    public bool startPlay;

    private string mapTypeString;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        points = PlayerPrefs.GetInt("points", 0);
        hasPlayedBefore = PlayerPrefs.GetInt("HasPlayedBefore", 0) == 1;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddPoints(pointWin);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            RemovePoints();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("HasPlayedBefore", 0);
            hasPlayedBefore = false;    
            Debug.Log("Reset");
        }

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetSelectedCharacter(int selectedIndex, DataChar[] allData)
    {
        selectedCharacterIndex = selectedIndex;
        allDataChars = allData;

        if (selectedIndex >= 0 && selectedIndex < allData.Length)
        {
            selectedCharacterData = allData[selectedIndex];
        }
        else
        {
            selectedCharacterData = null;
        }
    }

    public int RandomMapIndex()
    {
        if (mapData != null && mapData.mapPrefabs != null && mapData.mapPrefabs.Length > 0)
        {
            mapIndex = Random.Range(0, mapData.mapPrefabs.Length);
            return mapIndex;
        }
        else
        {
            return -1;
        }
    }

    public string CheckMapType(MapTypes types)
    {
        mapTypeString = types.ToString();
        if (selectedCharacterData != null)
        {
            IsSpecialityMatchingMap();
        }
        return mapTypeString;
    }

    public bool IsSpecialityMatchingMap()
    {
        if (selectedCharacterData == null)
        {
            isSpecialityMatchingMap = false;
            return false;
        }
        string charAbilityString = selectedCharacterData.speciality.ToString();

        if (string.IsNullOrEmpty(mapTypeString))
        {
            isSpecialityMatchingMap = false;
            return false;
        }

        if (charAbilityString == mapTypeString)
            isSpecialityMatchingMap = true;
        else
            isSpecialityMatchingMap = false;

        return isSpecialityMatchingMap;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var audio = AudioManager.AudioInstance;

        if (scene.name == "MainMenu")
        {
            audio.bgmPlay(audio.mainmenuBgm);

            pointText = FindAnyObjectByType<TextMeshProUGUI>();
            GameObject pointObj = GameObject.Find("Point Text");

            if (pointObj != null)
            {
                pointText = pointObj.GetComponent<TextMeshProUGUI>();
            }

            if (pointText != null)
            {
                pointText.text = "Points: " + points.ToString();
            }
        }

        if (scene.name == "Select Chara")
        {
            RandomMapIndex();
            panelTutorial = GameObject.Find("panel tutorial");
            panelTutorial.SetActive(false);
            if (!hasPlayedBefore)
            {
                if (panelTutorial != null)
                    panelTutorial.SetActive(true);

                hasPlayedBefore = true;
                PlayerPrefs.SetInt("HasPlayedBefore", 1);
                PlayerPrefs.Save();
            }
            //startPlay = false;
        }

        if (scene.name == "InGame")
        {
            audio.bgmPlay(audio.IngameBgm);
            gameEnded = false;
            //startPlay= false;
            FindUIElements();
            if (startPlay)
            {
                Time.timeScale = 1f;
            }
        }   
    }

    private void FindUIElements()
    {
        GameObject panelObj = GameObject.Find("WinLosePanel");

        if (panelObj != null)
        {
            panelWinLose = panelObj;
            TextMeshProUGUI resultText = panelWinLose.GetComponentInChildren<TextMeshProUGUI>(true);

            if (resultText != null)
            {
                winLoseText = resultText;
                panelWinLose.SetActive(false);
                return;
            }
        }
        if (pointText = null) return;
        panelWinLose = null;
        winLoseText = null;
    }

    public void winLoseCondition(bool finalCondition)
    {
        if (gameEnded) return;
        gameEnded = true;
        var audio = AudioManager.AudioInstance;
        if (finalCondition)
        {
            Time.timeScale = 0f;
            winLoseText.text = "You Win";
            AddPoints(pointWin);
            panelWinLose.SetActive(true);
            audio.sfxPlay(audio.winSfx);
        }
        else
        {
            Time.timeScale = 0f;
            winLoseText.text = "You Lose";
            AddPoints(pointLose);
            panelWinLose.SetActive(true);
        }
        audio.bgmSource.Stop();
    }

    public void AddPoints(int point)
    {
        points += point;
        PlayerPrefs.SetInt("points", points);
        PlayerPrefs.Save();

        if (pointText != null)
        {
            pointText.text = "Points: " + points.ToString();
        }
    }

    public void RemovePoints()
    {
        points = 0;
        PlayerPrefs.SetInt("points", 0);
        PlayerPrefs.Save();

        if (pointText != null)
        {
            pointText.text = "Points: " + points.ToString();
        }
    }

}
