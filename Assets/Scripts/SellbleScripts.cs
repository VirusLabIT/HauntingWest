using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
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
    [SerializeField] GameObject ObjectToSell;
    [SerializeField] Animator InfoUIAnimator;
    [SerializeField] GameObject SellObjectPos;

    bool IsPlayerOn = false;
    CoinManager CoinManager;

    private void Start()
    {
        SetSellbleUI();
    }

    private void Update()
    {
        if (IsPlayerOn && Input.GetKeyDown(KeyCode.X) && CoinManager.Coins >= ItemPrice)
        {
            if (ObjectToSell != null)
            {
                Vector3 Offset = new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), 0);

                Instantiate(ObjectToSell, SellObjectPos.transform.position + Offset, Quaternion.identity);

                CoinManager.RemoveCoins(ItemPrice);
            }
        }
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
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinManager = collision.GetComponent<CoinManager>();
            StartCoroutine(TriggerEnter());
            IsPlayerOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TriggerExit());
            IsPlayerOn = false;
        }
    }

    IEnumerator TriggerEnter()
    {
        UIPopUp();
        yield return new WaitForSeconds(0.5f);
        UIPopIdle();
    }

    IEnumerator TriggerExit()
    {
        UIPopDown();
        yield return null;
    }

    void UIPopUp()
    {
        if (InfoUIAnimator.GetInteger("Pop") == 2)
        {
            InfoUIAnimator.SetInteger("Pop", 1);
        }
    }

    void UIPopDown()
    {
        if (InfoUIAnimator.GetInteger("Pop") == 0)
        {
            InfoUIAnimator.SetInteger("Pop", 2);
        }
    }

    void UIPopIdle()
    {
        if (InfoUIAnimator.GetInteger("Pop") != 0)
        {
            InfoUIAnimator.SetInteger("Pop", 0);
        }
    }

}
