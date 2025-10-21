using UnityEngine;

public class FinishCondition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player adalah yang pertama menyentuh garis finish
            GameManager.instance.winLoseCondition(true); // Player Win

            // Opsional: Hentikan pergerakan Player dan NPC lain
            // ...
        }
        else if (collision.gameObject.CompareTag("Npc")) // Asumsi NPC memiliki tag "npc"
        {
            // NPC adalah yang pertama menyentuh garis finish sebelum Player
            GameManager.instance.winLoseCondition(false); // Player Lose

            // Opsional: Hentikan pergerakan Player dan NPC lain
            // ...
        }
    }
}
