using System.Collections;
using TMPro;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] GameObject BulletPre;
    [SerializeField] float ReloadTime;
    [SerializeField] float BulletSpeed;
    [SerializeField] float Delay;
    [SerializeField] int Damage;

    [Header("Ammo")]
    [SerializeField] int MaxAmmo;
    [SerializeField] int CurrentAmmo;

    [Header("GUI")]
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] string AmmoDevider = "/";

    bool isShooting;
    bool isReloading;




    private void Start()
    {
        CurrentAmmo = MaxAmmo;
    }


    void Reload()
    {
        if ((Input.GetKeyDown(KeyCode.R) && !isReloading && CurrentAmmo != MaxAmmo) || (CurrentAmmo <= 0 && !isReloading))
        {
            StartCoroutine(IReload());
        }

        
    }


    IEnumerator IReload()
    {
        isReloading = true;

        yield return new WaitForSeconds(ReloadTime);

        CurrentAmmo = MaxAmmo;

        isReloading = false;
    }

    private void Update()
    {
        Shoot();
        Reload();
        UpdateAmmoText();
    }

    void UpdateAmmoText()
    {
        string FinleText = $"{CurrentAmmo} {AmmoDevider} {MaxAmmo}";

        AmmoText.text = FinleText;
    }


    void Shoot()
    {
        if (Input.GetMouseButton(0) && !isShooting && !isReloading)
        {
            StartCoroutine(IShoot());
        }
    }


    IEnumerator IShoot()
    {
        isShooting = true;

        Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MouseWorldPos.z = 0;

        Vector3 Dir = (MouseWorldPos - transform.position).normalized;

        CurrentAmmo--;

        GameObject Bullet = Instantiate(BulletPre, transform.position, Quaternion.identity);
        Bullet.GetComponent<Bullet>().Setup(Dir, BulletSpeed, Damage);


        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        Bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        yield return new WaitForSecondsRealtime(Delay);

        isShooting = false;
    }

}
