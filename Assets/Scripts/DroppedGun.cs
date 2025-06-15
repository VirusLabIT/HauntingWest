using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class DroppedGun : MonoBehaviour
{
    public int Index;
    bool ispressed;
    bool isPlayeron = false;
    GameObject Player;
    private void Update()
    {
        Press(Player);
    }

    void Press(GameObject Player)
    {
        if (Input.GetKeyDown(KeyCode.E) && Player.CompareTag("Player") && isPlayeron)
        {
            print("Press");
            Player.GetComponentInChildren<Gun>().DropGun();
            Player.GetComponentInChildren<Gun>().CurrentGunIndex = Index;
            Player.GetComponentInChildren<Gun>().UpdateGun();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayeron = true;
            print("PlayerOn");
            Player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayeron = false;
            print("PlayerOff");
        }
    }

}
