using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float TimeToLast = 3f;
    public bool hitDetection;
    public bool bulletDetection;
    Vector3 Dir;
    float Speed;
    public int Damage;


    public void Setup(Vector3 dir, float speed, int damage)
    {
        Dir = dir;
        Speed = speed;
        Damage = damage;
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
        if (!collision.CompareTag("Player") && !collision.CompareTag("Bullet"))
        {
            if (collision.gameObject.layer == 7)
            {
                hitDetection = true;
                bulletDetection = false;
            }else if (collision.gameObject.layer == 8)
            {
                bulletDetection = true;
                hitDetection = false;
            }else if (collision.CompareTag("Wall"))
            {
                print(collision.name);
                hitDetection = false;
                bulletDetection = false;
                Destroy(gameObject);
            }
            else
            {
                hitDetection = false;
                bulletDetection = false;
                Destroy(gameObject);
            }
        }else
        {
            print(collision.name);
        }
    }

    

    private void Update()
    {
        transform.position += Dir * Speed * Time.deltaTime;
    }


}
