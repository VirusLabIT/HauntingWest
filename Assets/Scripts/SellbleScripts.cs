using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellbleScripts : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] Sprite ItemIcon;
    [SerializeField] string ItemName;
    [SerializeField] int ItemPrice;

    [Header("Serialize")]
    [SerializeField] TextMeshProUGUI ItemNameText;
    [SerializeField] TextMeshProUGUI ItemPriceText;
    [SerializeField] SpriteRenderer ItemIconRenderer;
    [SerializeField] SpriteRenderer ItemIconBackGround;

    private void Start()
    {
        SetSellbleUI();
    }

    void SetSellbleUI()
    {
        if (ItemIconRenderer != null)
        {
            ItemIconRenderer.sprite = ItemIcon;
        }
        if (ItemNameText != null)
        {
            ItemNameText.text = ItemName;
        }
        if (ItemPriceText != null)
        {
            ItemPriceText.text = ItemPrice.ToString();
        }
        if (ItemIconBackGround != null)
        {
            ItemIconBackGround.sprite = ItemIcon;
        }
    }


}
