using UnityEngine;

public class FinishCondition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.winLoseCondition(true); 
        }
        else if (collision.gameObject.CompareTag("Npc")) 
        {
            GameManager.instance.winLoseCondition(false);
        }
    }
}
