using UnityEngine;
using System;


[Serializable]
public class MapElementData
{
    public GameObject prefab;
    public Sprite obstacleImage;
}


[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Objects/MapData")]
public class MapData : ScriptableObject
{
    public MapElementData[] mapPrefabs;
}
