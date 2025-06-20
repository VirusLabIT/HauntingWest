using System.Collections;
using TMPro;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] GameObject BulletPre;
    [SerializeField] float ReloadTime;
    [SerializeField] float BulletSpeed;
    [SerializeField] float Delay;
    [SerializeField] int Damage;
    [SerializeField] float LifeTime;

    [Header("Serialized Fields")]
    [SerializeField] GameObject BulletShootingPoint;
    [SerializeField] crosshair CrosshairScript;

    [Header("Ammo")]
    [SerializeField] int MaxAmmo;
    public int CurrentAmmo;

    [Header("GUI")]
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] string AmmoDevider = "/";

    public bool isShootingSniper;
    public bool isReloadingSniper;




    private void Start()
    {
        CurrentAmmo = MaxAmmo;
    }


    void Reload()
    {
        if ((Input.GetKeyDown(KeyCode.R) && !isReloadingSniper && CurrentAmmo != MaxAmmo && Input.mouseScrollDelta.y == 0) || (CurrentAmmo <= 0 && !isReloadingSniper && Input.mouseScrollDelta.y == 0))
        {
            StartCoroutine(IReload());
        }


    }


    IEnumerator IReload()
    {
        isReloadingSniper = true;

        CrosshairScript.isReloading = true;

        yield return new WaitForSeconds(ReloadTime);

        CurrentAmmo = MaxAmmo;

        CrosshairScript.isReloading = false;

        isReloadingSniper = false;
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
        if (Input.GetMouseButtonDown(0) && !isShootingSniper && !isReloadingSniper && Input.mouseScrollDelta.y == 0 && CurrentAmmo >= 1)
        {
            StartCoroutine(IShoot());
        }
    }


    IEnumerator IShoot()
    {
        isShootingSniper = true;

        if (Input.mouseScrollDelta.y != 0)
        {
            isReloadingSniper = false;
            yield break;
        }

        Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MouseWorldPos.z = 0;

        if (Input.mouseScrollDelta.y != 0)
        {
            isReloadingSniper = false;
            yield break;
        }

        Vector3 Dir = (MouseWorldPos - BulletShootingPoint.transform.position).normalized;

        CurrentAmmo--;

        if (Input.mouseScrollDelta.y != 0)
        {
            isReloadingSniper = false;
            yield break;
        }

        GameObject Bullet = Instantiate(BulletPre, BulletShootingPoint.transform.position, Quaternion.identity);
        Bullet.GetComponent<Bullet>().Setup(Dir, BulletSpeed, Damage, LifeTime);

        if (Input.mouseScrollDelta.y != 0)
        {
            isReloadingSniper = false;
            yield break;
        }

        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        Bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        if (Input.mouseScrollDelta.y != 0)
        {
            isReloadingSniper = false;
            yield break;
        }

        float elapsedTime = 0;

        while (elapsedTime < Delay)
        {
            elapsedTime += Time.deltaTime;

            if (Input.mouseScrollDelta.y != 0)
            {
                isReloadingSniper = false;
                yield break;
            }

            yield return null;
        }

        isShootingSniper = false;
    }
}
