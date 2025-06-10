using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] int HealthPackValue = 10;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Health health = collision.gameObject.GetComponent<Health>();

            
            int healthtoheal = health.MaxHealth - health.health;

            int healamount = Mathf.Min(healthtoheal, HealthPackValue);

            if (healamount > 0)
            {
                health.ReviveHealth(healamount);

                health.ClampHealth(health.health);
            }

            Destroy(gameObject);

        }
    }
}
