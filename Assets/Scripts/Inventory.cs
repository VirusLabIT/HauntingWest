 using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GunText;
    [SerializeField] TextMeshProUGUI CurrentGun;

    public GameObject inv;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentGun != null) { GunText.text = CurrentGun.text; }

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
