using System.Collections;
using TMPro;
using UnityEngine;

public class Revolver : MonoBehaviour
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

    [Header("Ammo")]
    [SerializeField] int MaxAmmo;
    public int CurrentAmmo;

    [Header("GUI")]
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] string AmmoDevider = "/";

    public bool isShootingRevolver;
    public bool isReloadingRevolver;




    private void Start()
    {
        CurrentAmmo = MaxAmmo;
    }


    void Reload()
    {
        if ((Input.GetKeyDown(KeyCode.R) && !isReloadingRevolver && CurrentAmmo != MaxAmmo && Input.mouseScrollDelta.y == 0) || (CurrentAmmo <= 0 && !isReloadingRevolver && Input.mouseScrollDelta.y == 0))
        {
            StartCoroutine(IReload());
        }


    }


    IEnumerator IReload()
    {
        isReloadingRevolver = true;

        yield return new WaitForSeconds(ReloadTime);

        CurrentAmmo = MaxAmmo;

        isReloadingRevolver = false;
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
        if (Input.GetMouseButtonDown(0) && !isShootingRevolver && !isReloadingRevolver && Input.mouseScrollDelta.y == 0 && CurrentAmmo >= 1)
        {
            StartCoroutine(IShoot());
        }
    }


    IEnumerator IShoot()
    {
        isShootingRevolver = true;

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingRevolver = false;
            yield break;
        }

        Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MouseWorldPos.z = 0;

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingRevolver = false;
            yield break;
        }

        Vector3 Dir = (MouseWorldPos - BulletShootingPoint.transform.position).normalized;

        CurrentAmmo--;

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingRevolver = false;
            yield break;
        }

        GameObject Bullet = Instantiate(BulletPre, BulletShootingPoint.transform.position, Quaternion.identity);
        Bullet.GetComponent<Bullet>().Setup(Dir, BulletSpeed, Damage, LifeTime);

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingRevolver = false;
            yield break;
        }

        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        Bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingRevolver = false;
            yield break;
        }

        float elapsedTime = 0;

        while (elapsedTime < Delay)
        {
            elapsedTime += Time.deltaTime;

            if (Input.mouseScrollDelta.y != 0)
            {
                isShootingRevolver = false;
                yield break;
            }

            yield return null;
        }

        isShootingRevolver = false;
    }
}
