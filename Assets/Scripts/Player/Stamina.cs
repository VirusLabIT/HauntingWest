using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] Image DashImage;
    public float currentmana = 1f;
    public bool isregen = false;
    [SerializeField] AnimationCurve RegenCurve;

    private void Start()
    {
        UpdateSlider();
    }

    private void Update()
    {
        if (!GetComponent<Movement>().isDashing && !isregen && currentmana < 1f)
        {
            Regen();
        }
    }

    public void UpdateSlider()
    {
        DashImage.materialForRendering.SetFloat("_Value", currentmana);
    }

    public void Regen()
    {
        StartCoroutine(IRegen());
    }

    IEnumerator IRegen()
    {
        isregen = true;

        yield return new WaitForSecondsRealtime(.5f);
        float mana = currentmana;
        while (currentmana < 1f)
        {
            mana += .3f * Time.deltaTime;
            currentmana = RegenCurve.Evaluate(mana);
            currentmana = Mathf.Clamp01(currentmana); // Ensure it doesn't go over 1
            UpdateSlider();
            yield return null;
        }

        isregen = false;
    }
}
