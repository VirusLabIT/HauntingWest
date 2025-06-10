using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int Coins;
    [SerializeField] TextMeshProUGUI CoinsGUI;


    private void Start()
    {
        UpdateText();
    }

    void UpdateText()
    {
        CoinsGUI.text = Coins.ToString();
    }

    public void AddCoins(int coinsToAdd)
    {
        Coins += coinsToAdd;
        UpdateText();
    }

    public void RemoveCoins(int coinsToRemove)
    {
        Coins -= coinsToRemove;
        UpdateText();
    }

}
