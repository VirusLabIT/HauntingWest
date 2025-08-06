using UnityEngine;

public class ObjectSpirteRandomizer : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer spriteRenderer;
    private void Start()
    {
        int index = Random.Range(0, sprites.Length);

        spriteRenderer.sprite = sprites[index];
    }
}
