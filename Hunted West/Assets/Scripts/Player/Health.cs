using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Stats")]
    public int MaxHealth = 100;
    public int health;

    [Header("GUI")]
    [SerializeField] TextMeshProUGUI HealthTXT;
    [SerializeField] Slider HealthSlider;

    private void Update()
    {
        ClampHealth(health);
        HealthTXT.text = health.ToString();
        HealthSlider.value = health;
    }

    void ClampHealth(float health)
    {
        if (health <= 0)
        {
            Dead();
        }

        if (health > MaxHealth)
        {
                health = MaxHealth;
        }
    }
 
    private void Start()
    {
        health = MaxHealth;
    }

    public void DealDamage(int damage)
    {
        StartCoroutine(IDealDamage(damage));
        print(health);
        ClampHealth(health);
    }

    IEnumerator IDealDamage(int damage)
    {
        int finlehealth = health - damage;

        while (health > finlehealth)
        {
            health--;

            yield return new WaitForSecondsRealtime(.02f);
        }
    }

    public void ReviveHealth(int healthToRevive)
    {
        health += healthToRevive;

        ClampHealth(health);
    }


    void Dead()
    {
        int BuildIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(BuildIndex);
    }

}
