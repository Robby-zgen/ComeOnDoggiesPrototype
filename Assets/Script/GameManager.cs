using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public DataChar[] allDataChars; // nyimpen semua data karakter di scene ingame
    public int selectedCharacterIndex = -1;
    public DataChar selectedCharacterData;// nyimpen data karakter player yang dimainkan

    public bool isSpecialityMatchingMap;
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
    }
    
    public void SetSelectedCharacter(int selectedIndex, DataChar[] allData)
    {
        selectedCharacterIndex = selectedIndex;
        allDataChars = allData;

        if (selectedIndex >= 0 && selectedIndex < allData.Length)
        {
            selectedCharacterData = allData[selectedIndex];
            //Debug.Log("Speciality karakter yang dipilih: " + selectedCharacterData.speciality);
        }
        else
        {
            selectedCharacterData = null;
        }
    }

    public string CheckMapType(MapTypes types)
    {
        mapTypeString = types.ToString();
        Debug.Log("TIPE MAP SAAT INI ADALAH: " + mapTypeString);
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
}
