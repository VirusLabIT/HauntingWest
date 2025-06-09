using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [Header("Stats")]
    public int MaxStamina = 100;
    public int CurrentStamina = 0;
    public bool isRegenarating = false;
    [SerializeField] int MinStaminaToRegenarate = 20;

    [Header("GUI")]
    [SerializeField] TextMeshProUGUI StaminaTXT;
    [SerializeField] Slider StaminaSlider;
    private void Start()
    {
        CurrentStamina = MaxStamina;
    }

    private void Update()
    {
        ClampStamina();
        RegenerateStamina();

        StaminaTXT.text = CurrentStamina.ToString();
        StaminaSlider.value = CurrentStamina;
    }

    void RegenerateStamina()
    { 

        if (CurrentStamina <= MinStaminaToRegenarate && !isRegenarating)
        {
            isRegenarating = true;

            int staminaToAdd = 100 - CurrentStamina;

            AddStamina(staminaToAdd, .1f);

            ClampStamina();

        }
        if (CurrentStamina == 100)
        {
            isRegenarating = false;
        }
    }

    void ClampStamina()
    {
        if (CurrentStamina > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        }

        if (CurrentStamina <= 0)
        {
            CurrentStamina = 0;
        }
    }

    public void DealStamina(int StaminaToDeal)
    {
        StartCoroutine(IDealStamina(StaminaToDeal));
        ClampStamina();
    }

    IEnumerator IDealStamina(int staminatodeal)
    {
        int TargerStamina = CurrentStamina - staminatodeal;


        while (CurrentStamina > TargerStamina)
        {
            CurrentStamina--;

            yield return new WaitForSecondsRealtime(0.02f);
        }
    } 

    public void AddStamina(int StaminaToAdd, float Delay)
    {
        StartCoroutine(IAddStamina(StaminaToAdd, Delay));
        ClampStamina();
    }

    IEnumerator IAddStamina(int staminatoadd, float delay)
    {
        int TargerStamina = CurrentStamina + staminatoadd;


        while (CurrentStamina < TargerStamina)
        {
            CurrentStamina++;

            yield return new WaitForSecondsRealtime(delay);
        }
    }

}
