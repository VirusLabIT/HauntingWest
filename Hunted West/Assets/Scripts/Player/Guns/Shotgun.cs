using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] GameObject BulletPre;
    [SerializeField] float ReloadTime;
    [SerializeField] float BulletSpeed;
    [SerializeField] float Delay;
    [SerializeField] int Damage;
    [SerializeField] int MinNumOfShootsInOne = 3;
    [SerializeField] int MaxNumOfShootsInOne = 7;
    [SerializeField] float LifeTime;


    [Header("Ammo")]
    [SerializeField] int MaxAmmo;
    [SerializeField] int CurrentAmmo;

    [Header("GUI")]
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] string AmmoDevider = "/";

    public bool isShootingShotGun;
    public bool isReloadingShotGun;




    private void Start()
    {
        CurrentAmmo = MaxAmmo;
    }


    void Reload()
    {
        if ((Input.GetKeyDown(KeyCode.R) && !isReloadingShotGun && CurrentAmmo != MaxAmmo && Input.mouseScrollDelta.y == 0) || (CurrentAmmo <= 0 && !isReloadingShotGun && Input.mouseScrollDelta.y == 0))
        {
            StartCoroutine(IReload());
        }


    }


    IEnumerator IReload()
    {
        isReloadingShotGun = true;

        yield return new WaitForSeconds(ReloadTime);

        CurrentAmmo = MaxAmmo;

        isReloadingShotGun = false;
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
        if (Input.GetMouseButtonDown(0) && !isShootingShotGun && !isReloadingShotGun && Input.mouseScrollDelta.y == 0)
        {
            StartCoroutine(IShoot());
        }
    }


    IEnumerator IShoot()
    {
        isShootingShotGun = true;

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingShotGun = false;
            yield break;
        }

        Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MouseWorldPos.z = 0;

        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingShotGun = false;
            yield break;
        }

        Vector3 Dir = (MouseWorldPos - transform.position).normalized;

        CurrentAmmo--;

        int numofshots = Random.Range(MinNumOfShootsInOne, MaxNumOfShootsInOne);

        for (int i = 0; i != numofshots; i++)
        {

            if (Input.mouseScrollDelta.y != 0)
            {
                isShootingShotGun = false;
                yield break;
            }

            Vector3 ofset = new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f,.2f), 0);

            if (Input.mouseScrollDelta.y != 0)
            {
                isShootingShotGun = false;
                yield break;
            }

            GameObject Bullet = Instantiate(BulletPre, transform.position, Quaternion.identity);
            Bullet.GetComponent<Bullet>().Setup(Dir + ofset, BulletSpeed, Damage, LifeTime);

            if (Input.mouseScrollDelta.y != 0)
            {
                isShootingShotGun = false;
                yield break;
            }

            float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
            Bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 90 + ofset.x);

            if (Input.mouseScrollDelta.y != 0)
            {
                isShootingShotGun = false;
                yield break;
            }
        }


        if (Input.mouseScrollDelta.y != 0)
        {
            isShootingShotGun = false;
            yield break;
        }

        float elapsedTime = 0;

        while (elapsedTime < Delay)
        {
            elapsedTime += Time.deltaTime;

            if (Input.mouseScrollDelta.y != 0)
            {
                isShootingShotGun = false;
                yield break;
            }

            yield return null;
        }

        isShootingShotGun = false;
    }
}
