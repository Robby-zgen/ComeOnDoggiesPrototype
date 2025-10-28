using UnityEngine;
using UnityEngine.UI;
public class NextObsImageInfo : MonoBehaviour
{
    public Image nextObsImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MapImage(GameManager.instance.mapIndex);
    }

    void MapImage(int index)
    {
        nextObsImage.sprite = GameManager.instance.mapData.mapPrefabs[index].obstacleImage;

    }
}
