using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] Slider ManaSlider;
    public float currentmana = 1f;
    public bool isregen = false;

    private void Start()
    {
        ManaSlider.value = currentmana;
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
        ManaSlider.value = currentmana;
    }

    public void Regen()
    {
        StartCoroutine(IRegen());
    }

    IEnumerator IRegen()
    {
        isregen = true;

        yield return new WaitForSecondsRealtime(.5f);

        while (currentmana < 1f)
        {
            currentmana += 0.3f * Time.deltaTime; // Slower, framerate-independent
            currentmana = Mathf.Clamp01(currentmana); // Ensure it doesn't go over 1
            ManaSlider.value = currentmana;
            yield return null;
        }

        isregen = false;
    }
}
