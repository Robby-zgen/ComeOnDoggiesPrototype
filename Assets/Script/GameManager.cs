using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public DataChar[] allDataChars; // nyimpen semua data karakter di scene ingame
    public int selectedCharacterIndex = -1;
    public DataChar selectedCharacterData;// nyimpen data karakter player yang dimainkan

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Hapus event listener saat objek dihancurkan
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
        //Debug.Log("TIPE MAP SAAT INI ADALAH: " + mapTypeString);
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
        if (scene.name == "InGame")
        {
            Time.timeScale = 1f;
            FindUIElements();
        }
    }

    private void FindUIElements()
    {
        GameObject panelObj = GameObject.Find("WinLosePanel");

        if (panelObj != null)
        {
            panelWinLose = panelObj;

            // --- Langkah 2: Cari Text Sebagai Anak dari Panel ---
            // Mencari component TextMeshProUGUI HANYA di dalam objek panelWinLose (dan anak-anaknya)
            TextMeshProUGUI resultText = panelWinLose.GetComponentInChildren<TextMeshProUGUI>(true);

            if (resultText != null)
            {
                winLoseText = resultText;

                // Lakukan inisialisasi: Sembunyikan panel induk
                panelWinLose.SetActive(false);
                Debug.Log("[GameManager] Berhasil mereferensikan UI Win/Lose.");
                return; // Keluar dari fungsi jika berhasil
            }
        }

        // --- Langkah 3: Error Handling Jika Gagal ---
        // Jika kode mencapai sini, berarti ada yang gagal ditemukan.
        Debug.LogError("[GameManager] GAGAL menemukan WinLosePanel atau final Text di Scene InGame. Pastikan nama GameObject sudah benar.");

        // Set referensi ke null jika gagal untuk safety, meskipun sudah null secara default
        panelWinLose = null;
        winLoseText = null;
    }

    public void winLoseCondition(bool finalCondition)
    {
        if (winGame) return;
        winGame = true;

        if (finalCondition)
        {
            Time.timeScale = 0f;
            winLoseText.text = "You Win";
            panelWinLose.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 0f;
            winLoseText.text = "You Lose";
            panelWinLose.gameObject.SetActive(true);
        }
    }
}
