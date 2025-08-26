using System.Collections.Generic;
using UnityEngine;

public class RenderOptimization : MonoBehaviour
{
    [SerializeField] float RenderRange = 10f;
    List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    private void Start()
    {
        // Collect all stones + breakables, but only their renderers
        GameObject[] stones = GameObject.FindGameObjectsWithTag("Stone");
        GameObject[] breakbles = GameObject.FindGameObjectsWithTag("Breakble");

        foreach (var obj in stones)
            renderers.Add(obj.GetComponentInChildren<SpriteRenderer>());

        foreach (var obj in breakbles)
            renderers.Add(obj.GetComponentInChildren<SpriteRenderer>());

        // Hide them at start
        foreach (var rend in renderers)
            if (rend != null) rend.enabled = false;
    }

    private void Update()
    {
        Render();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RenderRange);
    }

    void Render()
    {
        // Find all colliders inside range
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, RenderRange);

        // First hide everything
        foreach (var rend in renderers)
            if (rend != null) rend.enabled = false;

        // Then enable only those inside the circle
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Stone") || hit.CompareTag("Breakble"))
            {
                var rend = hit.GetComponentInChildren<SpriteRenderer>();
                if (rend != null) rend.enabled = true;
            }
        }
    }
}
