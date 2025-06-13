using UnityEngine;

public class FastEnemyAttackDitection : MonoBehaviour
{
    public bool IsPlayerDetectedInAttackRadios;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerDetectedInAttackRadios = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerDetectedInAttackRadios = false;
        }
    }
}
