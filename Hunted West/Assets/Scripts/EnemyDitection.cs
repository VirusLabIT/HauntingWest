using UnityEngine;

public class EnemyDitection : MonoBehaviour
{
    public bool IsPlayerDetected = false;


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerDetected = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerDetected = true;
        }
    }

}
