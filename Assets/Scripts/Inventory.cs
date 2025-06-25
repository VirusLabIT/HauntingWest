using System.Diagnostics;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GunText;
    [SerializeField] TextMeshProUGUI CurrentGun;

    public UnityEngine.UI.Image GunTexture;
    public Sprite[] GunsTextures;

    public GameObject inv;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentGun != null) { GunText.text = CurrentGun.text; }

        switch (GunText.text)
        {
            case "Rifle":
                GunTexture.sprite = GunsTextures[0];
                break;
            
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inv.activeSelf)
            {
                inv.SetActive(false);
            }
            else
            {
                inv.SetActive(true);
            }
        }

    }
}
