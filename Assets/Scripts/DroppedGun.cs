using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    standard,
    rusty,
    guilded
}



public class DroppedGun : MonoBehaviour
{
    public int Index;
    public int Ammo;
    bool ispressed;
    bool isPlayeron = false;
    GameObject Player;
    public State GunStates;
    [SerializeField] GameObject[] GunArt;

    


    private void Start()
    {
        foreach (GameObject gun in GunArt)
        {
            if (gun.name.ToLower() == GunStates.ToString())
            {
                gun.SetActive(true);
                print("Gun Art: " + gun.name);
            }
            else
            {
                gun.SetActive(false);
            }
        }
    }

    private void Update()
    {
        Press(Player);
    }

    void Press(GameObject Player)
    {
        if (Input.GetKeyDown(KeyCode.E) && Player.CompareTag("Player") && isPlayeron)
        {
            Player.GetComponentInChildren<Gun>().DropGun();
            Player.GetComponentInChildren<Gun>().CurrentGunIndex = Index;
            Player.GetComponentInChildren<Gun>().GunState = GunStates.ToString();
            print(Player.GetComponentInChildren<Gun>().GunState);
            if (Player.GetComponentInChildren<Shotgun>() != null)
            {
                Player.GetComponentInChildren<Shotgun>().CurrentAmmo = Ammo;
            }
            else if (Player.GetComponentInChildren<Revolver>() != null)
            {
                Player.GetComponentInChildren<Revolver>().CurrentAmmo = Ammo;
            }
            else if (Player.GetComponentInChildren<Sniper>() != null)
            {
                Player.GetComponentInChildren<Sniper>().CurrentAmmo = Ammo;
            }
            else if (Player.GetComponentInChildren<Rifle>() != null)
            {
                Player.GetComponentInChildren<Rifle>().CurrentAmmo = Ammo;
            }
            Player.GetComponentInChildren<Gun>().UpdateGun();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayeron = true;
            Player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayeron = false;
        }
    }

}
