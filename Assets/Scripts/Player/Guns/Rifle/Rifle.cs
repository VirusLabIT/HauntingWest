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

    [Header("Serialized Fields")]
    [SerializeField] GameObject BulletShootingPoint;
    [SerializeField] RifleArt RifleArtScript;
    [SerializeField] crosshair CrosshairScript;

    [Header("Anim")]
    [SerializeField] Animator RifleAnimator;
    int StateIdleAnim = 0;
    int StateShootAnim = 1;
    int StateReloadAnim = 2;

    [Header("Ammo")]
    [SerializeField] int MaxAmmo;
    public int CurrentAmmo;

    [Header("GUI")]
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] string AmmoDevider = "/";
    [SerializeField] GameObject AmmoImage;
    public bool isShootingRifle;
    public bool isReloadingRifle;




    private void Start()
    {
        CurrentAmmo = MaxAmmo;
        SetUpGun();
    }

    public void SetUpGun()
    {
        RifleAnimator = RifleArtScript.GetComponentInChildren<Animator>();
        BulletShootingPoint = RifleArtScript.ShootingPoint;
        Damage = RifleArtScript.Damage;
        ReloadTime = RifleArtScript.ReloadTime;
    }


    void Reload()
    {
        if ((Input.GetKeyDown(KeyCode.R) && !isReloadingRifle && CurrentAmmo != MaxAmmo && !isShootingRifle) || (CurrentAmmo <= 0 && !isReloadingRifle && !isShootingRifle))
        {
            StartCoroutine(IReload());
        }

        
    }


    IEnumerator IReload()
    {
        isReloadingRifle = true;

        CrosshairScript.isReloading = true;

        AmmoImage.SetActive(false);

        RifleAnimator.SetInteger("State", StateReloadAnim);

        yield return new WaitForSeconds(ReloadTime);

        RifleAnimator.SetInteger("State", StateIdleAnim);

        CurrentAmmo = MaxAmmo;

        CrosshairScript.isReloading = false;

        AmmoImage.SetActive(true);

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
        if (Input.GetMouseButton(0) && !isShootingRifle && !isReloadingRifle && CurrentAmmo >= 1)
        {
            StartCoroutine(IShoot());
        }
    }


    IEnumerator IShoot()
    {

        isShootingRifle = true;



        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 dir = (mouseWorldPos - BulletShootingPoint.transform.position).normalized;

        CurrentAmmo--;

        RifleAnimator.SetInteger("State", StateShootAnim);

        GameObject bullet = Instantiate(BulletPre, BulletShootingPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Setup(dir, BulletSpeed, Damage, LifeTime);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        yield return new WaitForSecondsRealtime(Delay);

        if (RifleAnimator.GetInteger("State") != StateReloadAnim)
        {
            RifleAnimator.SetInteger("State", StateIdleAnim);
        }

        isShootingRifle = false;
    }


}
