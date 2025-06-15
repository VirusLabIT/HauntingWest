using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GunText;
    [SerializeField] GameObject[] Guns;

    public List<GameObject> DroppedGuns = new() { };
    public GameObject CurrentGun;
    public int CurrentGunIndex;
    public string GunType;
    private void Update()
    {
        //ScrollManager();
        UpdateGun();
    }

    private void Start()
    {
        UpdateGun();
    }

   /* void ScrollManager()
    {
        


        if (Input.mouseScrollDelta.y > 0)
        {
            ScrollUp();
        }else if (Input.mouseScrollDelta.y < 0)
        {
            ScrollDown();
        }
    }
*/
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

    public void DropGun()
    {
        switch (CurrentGunIndex)
        {
            case 0:
                // No action taken for now
                Instantiate(DroppedGuns[0], transform.position, Quaternion.identity);
                break;
            case 1:
                // No action taken for now
                Instantiate(DroppedGuns[1], transform.position, Quaternion.identity);
                break;
            case 2:
                // No action taken for now
                Instantiate(DroppedGuns[2], transform.position, Quaternion.identity);
                break;
            case 3:
                // No action taken for now
                Instantiate(DroppedGuns[3], transform.position, Quaternion.identity);
                break;
        }
        
    }
/*
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
*/
}
