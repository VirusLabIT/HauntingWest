using NUnit.Framework.Constraints;
using TMPro;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GunText;
    [SerializeField] GameObject[] Guns;
    public GameObject CurrentGun;
    public int CurrentGunIndex;
    public string GunType;
    private void Update()
    {
        ScrollManager();
    }

    private void Start()
    {
        UpdateGun();
    }

    void ScrollManager()
    {
        


        if (Input.mouseScrollDelta.y > 0)
        {
            ScrollUp();
        }else if (Input.mouseScrollDelta.y < 0)
        {
            ScrollDown();
        }
    }

    void UpdateGun()
    {
        GunText.text = Guns[CurrentGunIndex].name;

        foreach (GameObject gun in Guns)
        {
            if (gun.name == Guns[CurrentGunIndex].name)
            {
                gun.SetActive(true);
                CurrentGun = gun;
            }else
            {
                gun.SetActive(false);
            }
        }


        if (CurrentGun.GetComponent<Shotgun>() != null)
        {
            GunType = "ShotGun";

            CurrentGun.GetComponent<Shotgun>().isShootingShotGun = false;
            CurrentGun.GetComponent<Shotgun>().isReloadingShotGun = false;
        }
        else if (CurrentGun.GetComponent<Revolver>() != null)
        {
            GunType = "Revolver";

            CurrentGun.GetComponent<Revolver>().isShootingRevolver = false;
            CurrentGun.GetComponent<Revolver>().isReloadingRevolver = false;
        }
        else if ( CurrentGun.GetComponent<Sniper>() != null)
        {
            GunType = "Sniper";

            CurrentGun.GetComponent<Sniper>().isShootingSniper = false;
            CurrentGun.GetComponent<Sniper>().isReloadingSniper = false;
        }
        else if (CurrentGun.GetComponent<Rifle>() != null)
        {
            GunType = "Rifle";

            CurrentGun.GetComponent<Rifle>().isShootingRifle = false;
            CurrentGun.GetComponent<Rifle>().isReloadingRifle = false;
        }
    }



    void ScrollDown()
    {
        int index = CurrentGunIndex + 1;
        if (index > Guns.Length - 1)
        {
            CurrentGunIndex = 0;
        }else
        {
            CurrentGunIndex++;
        }

        
        UpdateGun();
    }

    void ScrollUp()
    {
        int index = CurrentGunIndex - 1;

        if (index < 0)
        {
            CurrentGunIndex = Guns.Length - 1;
        }else
        {
            CurrentGunIndex--;
        }
        UpdateGun();
    }

}
