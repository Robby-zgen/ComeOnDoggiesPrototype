using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
