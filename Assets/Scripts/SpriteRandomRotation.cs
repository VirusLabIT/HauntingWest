using UnityEngine;

public class SpriteRandomRotation : MonoBehaviour
{
    [SerializeField] Vector2 MinMaxRotation = new Vector2(-45f, 45);

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, (int)(Random.Range(MinMaxRotation.x, MinMaxRotation.y)));
    }
}
