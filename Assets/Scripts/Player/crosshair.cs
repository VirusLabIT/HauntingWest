using UnityEngine;

public class crosshair : MonoBehaviour
{
    public bool isReloading = false;
    bool oldReload = false;
    void Update()
    {
        UpdatecrosshairPosition();
        Reload();
    }

    void UpdatecrosshairPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set the distance from the camera
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);

        Cursor.visible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }else
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }

    
    
    void Reload()
    {
        if (isReloading)
        {
            oldReload = true;
            GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        }
        else if (!isReloading && oldReload)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
            oldReload = false;
        }
    }
}
