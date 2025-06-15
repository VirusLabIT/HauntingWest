using System;
using Unity.VisualScripting;
using UnityEngine;

public class DroppedGun : MonoBehaviour
{
    public int Index;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            collision.gameObject.GetComponentInChildren<Gun>().DropGun();
            collision.gameObject.GetComponentInChildren<Gun>().CurrentGunIndex = Index;
            Destroy(this.gameObject);
        }
    }
}
