using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterBasePrefab;// nyimpen karakter base gameobject dalam prefab
    public Transform[] spawnPoints;

    private string PlayerScript = "PlayerMovement";
    private string NPCScript = "NPC";

    private Transform mainCameraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        SpawnAllCharacters();
    }

    private void SpawnAllCharacters()
    {
        if (GameManager.instance == null || characterBasePrefab == null)
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

        // Fleksibilitas: Hitung batas spawn berdasarkan yang paling sedikit
        int spawnCount = Mathf.Min(allData.Length, spawnPoints.Length);

        if (spawnCount < allData.Length)
        {
            Debug.LogWarning($"[CS] Peringatan: Hanya {spawnCount} karakter yang di-spawn karena Spawn Points tidak cukup.");
        }

        // Loop untuk men-spawn semua karakter yang datanya tersedia
        for (int i = 0; i < spawnCount; i++)
        {
            DataChar charData = allData[i];

            GameObject charInstance = Instantiate(
                characterBasePrefab,
                spawnPoints[i].position,
                Quaternion.identity
            );

            Vector3 targetScale = new Vector3(0.2f, 0.2f, 0.2f);// sesuaikan ukuran sprite karakter
            charInstance.transform.localScale = targetScale;

            // 2. Terapkan Visual
            ApplyVisuals(charInstance, charData);

            // 3. Terapkan Logika Player vs. NPC
            if (i == selectedIndex)
            {
                // === PLAYER LOGIC ===
                SetMovementScriptState(charInstance, PlayerScript, true);
                MonoBehaviour npcComponent = charInstance.GetComponent(NPCScript) as MonoBehaviour;
                if (npcComponent != null)
                {
                    Destroy(npcComponent);
                }
                charInstance.name = $"Player {charData.characterName}";
                charInstance.tag = "Player";

                if (mainCameraTransform != null)
                {
                    mainCameraTransform.SetParent(charInstance.transform);
                }
            }
            else
            {
                // === NPC LOGIC ===
                MonoBehaviour PlayerComponent = charInstance.GetComponent(PlayerScript) as MonoBehaviour;
                if (PlayerComponent != null)
                {
                    Destroy(PlayerComponent);
                }
                SetMovementScriptState(charInstance, NPCScript, true);
                charInstance.name = $"NPC {charData.characterName}";
                charInstance.tag = "Npc";
            }
        }
    }

    private void ApplyVisuals(GameObject character, DataChar data)
    {
        SpriteRenderer sr = character.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = data.characterImage;
        }
    }

    private void SetMovementScriptState(GameObject target, string scriptName, bool isEnabled)
    {
        MonoBehaviour script = target.GetComponent(scriptName) as MonoBehaviour;
        if (script != null)
        {
            script.enabled = isEnabled;
        }
    }

}
