using UnityEngine;

public enum MapTypes
{
    Water,
    DrumRoll,
    Tire,
    Log,
    Mud
}

public class MapType : MonoBehaviour
{
    public MapTypes type;
    private void Start()
    {
        GameManager.instance.CheckMapType(this.type);// kirim data berupa type map ini ke game manager
    }




}
