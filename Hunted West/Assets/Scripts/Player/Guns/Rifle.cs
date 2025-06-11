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
    [SerializeField] float LifeTime;

    [Header("Ammo")]
    [SerializeField] int MaxAmmo;
    [SerializeField] int CurrentAmmo;

    [Header("GUI")]
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] string AmmoDevider = "/";

    public bool isShootingRifle;
    public bool isReloadingRifle;




    private void Start()
    {
        CurrentAmmo = MaxAmmo;
    }


    void Reload()
    {
        if ((Input.GetKeyDown(KeyCode.R) && !isReloadingRifle && CurrentAmmo != MaxAmmo && Input.mouseScrollDelta.y == 0) || (CurrentAmmo <= 0 && !isReloadingRifle && Input.mouseScrollDelta.y == 0))
        {
            StartCoroutine(IReload());
        }

        
    }


    IEnumerator IReload()
    {
        isReloadingRifle = true;

        yield return new WaitForSeconds(ReloadTime);

        CurrentAmmo = MaxAmmo;

        isReloadingRifle = false;
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
        if (Input.GetMouseButton(0) && !isShootingRifle && !isReloadingRifle && Input.mouseScrollDelta.y == 0)
        {
            StartCoroutine(IShoot());
        }
    }


    IEnumerator IShoot()
    {
        isShootingRifle = true;

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingRifle = false;
            yield break;
        }

        Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingRifle = false;
            yield break;
        }

        MouseWorldPos.z = 0;

        Vector3 Dir = (MouseWorldPos - transform.position).normalized;

        CurrentAmmo--;

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingRifle = false;
            yield break;
        }

        GameObject Bullet = Instantiate(BulletPre, transform.position, Quaternion.identity);
        Bullet.GetComponent<Bullet>().Setup(Dir, BulletSpeed, Damage, LifeTime);

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingRifle = false;
            yield break;
        }

        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        Bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingRifle = false;
            yield break;
        }

        float elapsedTime = 0;

        while (elapsedTime < Delay)
        {
            elapsedTime += Time.deltaTime;

            if (Input.mouseScrollDelta.y != 0)
            {
                isShootingRifle = false;
                yield break;
            }

            yield return null;
        }

        isShootingRifle = false;
    }

}
