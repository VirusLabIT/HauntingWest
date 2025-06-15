using UnityEngine;

public class GunArt : MonoBehaviour
{
    
    void Update()
    {
        ArtUpdate();
    }

    void ArtUpdate()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        Vector2 direction = mousePosition - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle > 90 || angle < -90)
        {
            transform.rotation = Quaternion.Euler(new Vector3(-180, 0, -angle));
        }else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
