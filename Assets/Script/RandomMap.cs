using UnityEngine;

public class RandomMap : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MapGenerator(GameManager.instance.mapIndex);
    }

    void MapGenerator(int index)
    {
        GameObject map = GameManager.instance.mapData.mapPrefabs[index].prefab;
        Instantiate(map, transform.position , Quaternion.identity);
    }


}
