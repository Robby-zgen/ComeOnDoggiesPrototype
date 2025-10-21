using UnityEngine;
public enum Condition
{
    Happy,
    Normal,
    Exhaust
}

public enum Speciality
{
    Water,
    DrumRoll,
    Tire,
    Log
}

[CreateAssetMenu (fileName ="CharacterData", menuName = "Scriptable Object/CharacterData")]
public class DataChar : ScriptableObject
{
    public string characterName;
    public Sprite characterImage;
    public Sprite[] conditionImage;
    public Sprite specialityImage;

    public Speciality speciality;
    public Condition condition;

}
