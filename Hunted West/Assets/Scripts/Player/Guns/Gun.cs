using NUnit.Framework.Constraints;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject BulletPre;
    [SerializeField] float BulletSpeed = 1.0f;
    [SerializeField] int Damage = 1;
    void Update()
    {
        Shoot();
    }


    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            MouseWorldPos.z = 0;

            Vector3 Dir = (MouseWorldPos - transform.position).normalized;



            GameObject Bullet = Instantiate(BulletPre, transform.position, Quaternion.identity);
            Bullet.GetComponent<Bullet>().Setup(Dir, BulletSpeed, Damage);


            float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
            Bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }
}
