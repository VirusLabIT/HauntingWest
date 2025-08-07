using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    [SerializeField] float TimeToLast = 3f;
    public bool hitDetection;
    public bool bulletDetection;
    Vector3 Dir;
    float Speed;
    public int Damage;
    [SerializeField] ParticleSystem PointShotEffect;

    public void Setup(Vector3 dir, float speed, int damage, float lifetime)
    {
        Dir = dir;
        Speed = speed;
        Damage = damage;
        TimeToLast = lifetime;
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
        print("HIT");
        if (!collision.CompareTag("Player") && !collision.CompareTag("Bullet"))
        {
            if (collision.gameObject.layer == 7)
            {
                hitDetection = true;
                bulletDetection = false;
            }
            if (collision.gameObject.layer == 8)
            {
                bulletDetection = true;
                hitDetection = false;
            }
            else
            {
                hitDetection = false;
                bulletDetection = false;
                Destroy(gameObject);
            }
            if (collision.CompareTag("Wall"))
            {
                print(collision.name);
                hitDetection = false;
                bulletDetection = false;
                Destroy(gameObject);
            }
            if (collision.CompareTag("Breakble"))
            {
                collision.gameObject.GetComponent<Breakble>().LiveUpdate();
                print(collision.name + " Is Colid");
                hitDetection = false;
                bulletDetection = false;
                Destroy(gameObject);
            }

            ParticleSystem particle = Instantiate(PointShotEffect, transform.position, Quaternion.identity);
            Destroy(particle, .1f);
            print(collision.name);
        }
        else
        {
            print(collision.name);
        }
    }

    

    private void Update()
    {
        transform.position += Dir * Speed * Time.deltaTime;
    }


}
