using UnityEngine;

public class RandomMap : MonoBehaviour
{
    public GameObject[] mapPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MapGenerator();
    }
    void MapGenerator()
    {
        int mapIndex= Random.Range(0, mapPrefabs.Length);
        GameObject map = mapPrefabs[mapIndex];
        Instantiate(map, transform.position , Quaternion.identity);
    }
}
