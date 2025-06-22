
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator controller;

    private void Start()
    {
        controller = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CoinManager>() != null && collision.CompareTag("Player"))
        {
            CoinManager coinManager = collision.gameObject.GetComponent<CoinManager>();

            coinManager.AddCoins(1);

            controller.SetTrigger("PickedUp");

            print(gameObject.name);

            Destroy(gameObject, 0.35f);
            
        }
    }

}
