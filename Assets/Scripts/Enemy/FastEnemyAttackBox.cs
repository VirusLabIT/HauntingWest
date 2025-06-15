using UnityEngine;

public class FastEnemyAttackBox : MonoBehaviour
{
    int damage;
    private void Start()
    {
        damage = GetComponentInParent<FastEnemy>().Damage;
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
