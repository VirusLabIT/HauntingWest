using System.Collections.Generic;
using JetBrains.Annotations;
using Mono.Cecil;
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
    public string GunState;


    private void Start()
    {
        CurrentGun = Guns[CurrentGunIndex];

        GunState = CurrentGun.GetComponentInChildren<RifleArt>().rifleArtState.ToString();

        UpdateGun();
    }
    public void UpdateGun()
    {
        GunText.text = Guns[CurrentGunIndex].name;

        foreach (GameObject gun in Guns)
        {
            if (gun.name == Guns[CurrentGunIndex].name)
            {
                gun.SetActive(true);
                CurrentGun = gun;
                print(GunState);
                switch (GunState.ToLower())
                {
                    case "standard":
                        gun.GetComponentInChildren<RifleArt>().rifleArtState = RifleArt.RifleArtState.Standard;
                        break;
                    case "rusty":
                        gun.GetComponentInChildren<RifleArt>().rifleArtState = RifleArt.RifleArtState.Rusty;
                        break;
                    case "guilded":
                        gun.GetComponentInChildren<RifleArt>().rifleArtState = RifleArt.RifleArtState.Guilded;
                        break;
                    default:
                        {
                            print(gun.name + " does not have a valid state.");
                            break;
                        }
                }


                
            }
            else
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
        CurrentGun.GetComponentInChildren<RifleArt>().UpdateGunState();
    }


    public void DropGun()
    {
        GameObject gun = Instantiate(DroppedGuns[CurrentGunIndex], transform.position, Quaternion.identity);

        int ammo = 0;

        if (CurrentGun.GetComponent<Rifle>() != null)
        {
            ammo = CurrentGun.GetComponent<Rifle>().CurrentAmmo;

        }
        else if (CurrentGun.GetComponent<Shotgun>() != null)
        {
            ammo = CurrentGun.GetComponent<Shotgun>().CurrentAmmo;

        }
        else if (CurrentGun.GetComponent<Revolver>() != null)
        {
            ammo = CurrentGun.GetComponent<Revolver>().CurrentAmmo;

        }
        else if (CurrentGun.GetComponent<Sniper>() != null)
        {
            ammo = CurrentGun.GetComponent<Sniper>().CurrentAmmo;
            
        }
            
        gun.GetComponent<DroppedGun>().Ammo = ammo;

        print("ThisIsGunState from Gun.cs: " + GunState);


        switch (GunState.ToLower())
        {
            case "standard":
                gun.GetComponent<DroppedGun>().GunStates = State.standard;
                break;
            case "rusty":
                gun.GetComponent<DroppedGun>().GunStates = State.rusty;
                break;
            case "guilded":
                gun.GetComponent<DroppedGun>().GunStates = State.guilded;
                break;
            default:
                {
                    break;
                }
        }

        
        UpdateGun();
    }
}
