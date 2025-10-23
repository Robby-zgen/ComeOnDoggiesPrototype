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

    public int points;
    public TextMeshProUGUI pointText;

    public TextMeshProUGUI winLoseText;
    public GameObject panelWinLose;

    public bool isSpecialityMatchingMap;
    public bool winGame;

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
        points = PlayerPrefs.GetInt("points");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddPoints(100);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            RemovePoints();
        }

        if (pointText != null)
        {
            pointText.text = points.ToString();
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
            Debug.LogError("Error: selectedCharacterData belum diset!");
            return false;
        }
        string charAbilityString = selectedCharacterData.speciality.ToString();

        if (string.IsNullOrEmpty(mapTypeString))
        {
            isSpecialityMatchingMap = false;
            Debug.LogError("Error: mapTypeString belum diset. Pastikan MapType memanggil CheckMapType!");
            return false;
        }

        if (charAbilityString == mapTypeString)
            isSpecialityMatchingMap = true;
        else
            isSpecialityMatchingMap = false;

        Debug.Log($"[GM Check] Char Speciality: {charAbilityString}, Map Type: {mapTypeString}. Match: {isSpecialityMatchingMap}");

        return isSpecialityMatchingMap;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var audio = AudioManager.AudioInstance;
        if (scene.name == "InGame")
        {
            audio.bgmPlay(audio.IngameBgm);
            Time.timeScale = 1f;
            winGame = false;
            FindUIElements();
        }

        if (scene.name == "MainMenu")
        {
            audio.bgmPlay(audio.mainmenuBgm);
            Debug.Log("MainMenu");

            pointText = FindAnyObjectByType<TextMeshProUGUI>();
            GameObject pointObj = GameObject.Find("Point Text");

            if (pointObj != null)
            {
                pointText = pointObj.GetComponent<TextMeshProUGUI>();
            }

            if (pointText != null)
            {
                // Langsung update Text dengan poin yang sudah dimuat dari PlayerPrefs
                pointText.text ="Points: " + points.ToString();
                Debug.Log("[GM] PointText di MainMenu berhasil ditemukan dan diupdate.");
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
        if (winGame) return;
        winGame = true;
        var audio = AudioManager.AudioInstance;
        if (finalCondition)
        {
            Time.timeScale = 0f;
            winLoseText.text = "You Win";
            AddPoints(100);
            panelWinLose.SetActive(true);
            audio.sfxPlay(audio.winSfx);
        }
        else
        {
            Time.timeScale = 0f;
            winLoseText.text = "You Lose";
            AddPoints(50);
            panelWinLose.SetActive(true);
        }
        audio.bgmSource.Stop();
    }

    public void AddPoints(int point)
    {
        points += point;
        PlayerPrefs.SetInt("points", point);
        PlayerPrefs.Save();
    }

    public void RemovePoints()
    {
        points = 0;
        PlayerPrefs.SetInt("points", 0);
        PlayerPrefs.Save();
    }
}
