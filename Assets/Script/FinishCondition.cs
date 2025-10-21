using UnityEngine;

public class FinishCondition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("You Win");
        }
        else if (collision.gameObject.CompareTag("Npc"))
        {
            Debug.Log("You Lose");
        }
    }
}
