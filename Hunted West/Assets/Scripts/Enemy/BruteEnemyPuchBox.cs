using UnityEngine;

public class BruteEnemyPuchBox : MonoBehaviour
{

    int damage;

    private void Start()
    {
        damage = GetComponentInParent<BruteEnemy>().Damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;

            player.GetComponent<Health>().DealDamage(damage);
        }
    }
}
