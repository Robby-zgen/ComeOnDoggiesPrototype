using UnityEngine;

public class QTETrigger : MonoBehaviour
{
    public GameObject qtePrefab;
    private GameObject qteInstance;
    private Transform canvasTransform;

    void Start()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
    }

    public void TriggerQTE()
    {
        if (qteInstance == null)
        {
            qteInstance = Instantiate(qtePrefab, canvasTransform);
            qteInstance.SetActive(true);
        }
    }

    public void CloseQTE()
    {
        if (qteInstance != null)
        {
            Destroy(qteInstance);
        }
    }
}
