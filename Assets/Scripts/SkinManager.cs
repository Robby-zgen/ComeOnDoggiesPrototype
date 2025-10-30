using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public DataChar[] dataChars; // nyimpen semua data karakter yang ada di scene karakter selection
    public DataChar currentchara;// nyimpen data karakter yang dipilih player

    [Header("UI Components")]
    public Image characterImage; 
    public TextMeshProUGUI characterName; 
    public Image conditionImage; 
    public Image specialityImage;


    private Dictionary<DataChar, Sprite> characterConditions = new Dictionary<DataChar, Sprite>();// buat nyimpen kondisi karakter yang ada

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dataChars.Length > 0 && currentchara == null)
        {
            currentchara = dataChars[0];// kalau player blm milih tampilin yang paling depan
        }

        //untuk membuat kondisi yang berbeda setiap karakter
        foreach (DataChar data in dataChars)
        {
            if (data != null && !characterConditions.ContainsKey(data))
            {
                Sprite uniqueSprite = RandomCondition(data);
                characterConditions.Add(data, uniqueSprite);
            }
        }
        UpdateCharacterDisplay(currentchara);
    }

    // Buat button select karakter
    public void SelectCharacter (int charIndex)
    {
        if (charIndex >= 0 && charIndex < dataChars.Length)
        {
            currentchara = dataChars[charIndex];

            UpdateCharacterDisplay(currentchara);
        }
    }

    // Buat nampilin image & data karakter di scene
    private void UpdateCharacterDisplay(DataChar data)
    {
        if (characterImage != null && data.characterImage != null)
            characterImage.sprite = data.characterImage;

        if (characterName != null)
            characterName.text = data.characterName;

        if (specialityImage != null)
            specialityImage.sprite = data.specialityImage;

        if (conditionImage != null && characterConditions.ContainsKey(data))
        {
            //Sprite uniqueConditionSprite = characterConditions[data];
            conditionImage.sprite = characterConditions[data];
        }
    }


    //Buat ngeroll kondisi karakter
    public Sprite RandomCondition(DataChar dataToRoll)
    {
        float randomRoll = Random.value;

        if (randomRoll <= 0.3333f)
        {
            dataToRoll.condition = Condition.Happy;// kondisi di acak dan tetapkan state kondisi ke happy
            return dataToRoll.conditionImage[0];
        }
        else if (randomRoll <= 0.66666f)
        {
            dataToRoll.condition = Condition.Normal;
            return dataToRoll.conditionImage[1];// Normal
        }
        else
        {
            dataToRoll.condition = Condition.Exhaust;
            return dataToRoll.conditionImage[2];// Exhaust
        }
    }

    //untuk ngirim data karakter yang dipilih ke Game Manager
    public void ConfirmSelection()
    {
        if (GameManager.instance == null || dataChars.Length == 0)
        {
            return;
        }

        // 1. Temukan indeks karakter yang sedang dipilih
        int selectedIndex = -1;
        for (int i = 0; i < dataChars.Length; i++)
        {
            if (dataChars[i] == currentchara)
            {
                selectedIndex = i;
                break;
            }
        }

        if (selectedIndex == -1)
        {
            Debug.LogError("[SM] Karakter yang dipilih tidak ditemukan di array dataChars.");
            return;
        }

        GameManager.instance.SetSelectedCharacter(selectedIndex, dataChars);

        Debug.Log($"[SM] Data untuk {dataChars.Length} karakter terkirim. Player Index: {selectedIndex}.");
    }
}
