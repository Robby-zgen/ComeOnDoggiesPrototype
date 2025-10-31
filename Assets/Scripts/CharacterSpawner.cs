using UnityEngine;
using System;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characterPrefab;
    public Transform[] spawnPoints;

    private Type PlayerScriptType = typeof(PlayerMovement);
    private Type NPCScriptType = typeof(NPC);

    private Transform mainCameraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        SpawnAllCharacters();
    }

    private void SpawnAllCharacters()
    {
        if (GameManager.instance == null)
        {
            return;
        }

        // Ambil data yang udh di kirim ke GameManager sebelumnya
        int selectedIndex = GameManager.instance.selectedCharacterIndex;
        DataChar[] allData = GameManager.instance.allDataChars;

        if (allData == null || allData.Length == 0)
        {
            Debug.LogError("[CS] Setup Error: Data karakter tidak dimuat dari GameManager. (Array data kosong).");
            return;
        }

        int spawnCount = Mathf.Min(allData.Length, spawnPoints.Length, characterPrefab.Length);

        if (characterPrefab.Length < allData.Length)
        {
            Debug.LogWarning($"[CS] Peringatan: Hanya {spawnCount} karakter yang di-spawn karena Spawn Points tidak cukup.");
        }
        if (spawnCount < allData.Length)
        {
            Debug.LogWarning($"[CS] Peringatan: Hanya {spawnCount} karakter yang di-spawn.");
        }

        for (int i = 0; i < spawnCount; i++)
        {
            DataChar charData = allData[i];
            GameObject prefabToSpawn = characterPrefab[i];

            GameObject charInstance = Instantiate(
                prefabToSpawn,
                spawnPoints[i].position,
                Quaternion.identity
            );

            Vector3 targetScale = new Vector3(0.35f, 0.35f, 0.35f);// sesuaikan ukuran sprite karakter
            charInstance.transform.localScale = targetScale;

            if (i == selectedIndex)
            {
                //player
                Destroy(charInstance.GetComponent(NPCScriptType));
                MonoBehaviour playerComponent = charInstance.GetComponent(PlayerScriptType) as MonoBehaviour;
                if (playerComponent != null) playerComponent.enabled = true;

                charInstance.name = $"Player {charData.characterName}";
                charInstance.tag = "Player";

                if (mainCameraTransform != null)
                {
                    mainCameraTransform.SetParent(charInstance.transform);
                }
            }
            else
            {
                //npc
                Destroy(charInstance.GetComponent(PlayerScriptType));
                MonoBehaviour npcComponent = charInstance.GetComponent(NPCScriptType) as MonoBehaviour;
                if (npcComponent != null) npcComponent.enabled = true;

                charInstance.name = $"NPC {charData.characterName}";
                charInstance.tag = "Npc";
            }
        }
    }

}
