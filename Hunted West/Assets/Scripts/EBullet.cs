using System.Collections;
using UnityEngine;

public class EBullet : MonoBehaviour
{
    [SerializeField] float TimeToLast = 3f;

    Vector3 Dir;
    float Speed;


    public void ESetup(Vector3 dir, float speed)
    {
        Dir = dir;
        Speed = speed;
    }


    private void Start()
    {
        StartCoroutine(ITimer(TimeToLast));
    }

    IEnumerator ITimer(float Time)
    {
        yield return new WaitForSecondsRealtime(Time);

        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Bullet") && collision.gameObject.layer != 7 && collision.gameObject.layer != 8)
        {
            if (collision.CompareTag("Player"))
            {
                GameObject player = collision.gameObject;

                player.GetComponent<Health>().DealDamage(5);

                Destroy(gameObject);
            }else
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.position += Dir * Speed * Time.deltaTime;
    }
}
