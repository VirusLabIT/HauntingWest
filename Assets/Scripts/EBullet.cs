using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EBullet : MonoBehaviour
{
    [SerializeField] float TimeToLast = 3f;

    Vector3 Dir;
    float Speed;
    int Damage;

    public void ESetup(Vector3 dir, float speed, int damage)
    {
        Damage = damage;

        Dir = dir;

        Vector3 ofsset = new Vector3(Random.Range(-0.5f, .5f), Random.Range(-0.5f, .5f), Random.Range(-0.5f, .5f));

        float speedOfset = Random.Range(-.3f, .3f);

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
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Bullet") && collision.gameObject.layer != 7 && collision.gameObject.layer != 8 && !collision.CompareTag("EBullet"))
        {
            if (collision.CompareTag("Player"))
            {
                GameObject player = collision.gameObject;

                player.GetComponent<Health>().DealDamage(Damage);

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
