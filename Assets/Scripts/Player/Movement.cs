using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Modifire")]
    public float PlayerSpeedMod = 1f;
    [SerializeField] float Speed = 3f;

    [Header("Dash")]
    [SerializeField] float DashSpeed = 5f;
    [SerializeField] float DashTime = 0.1f;

    Rigidbody2D rb2d;
    public bool isDashing = false;
    Stamina stamina;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        stamina = GetComponent<Stamina>();
    }

    private void Update()
    {
        MovementHandler();
        Dash();
    }

    void Dash()
    {
        if (!isDashing && Input.GetKeyDown(KeyCode.Space) && !stamina.isregen)
        {
            StartCoroutine(IDash());
        }
    }

    IEnumerator IDash()
    {
        isDashing = true;

        float speed = Speed;

        Speed = DashSpeed;

        yield return new WaitForSecondsRealtime(DashTime);

        stamina.currentmana = 0;
        stamina.UpdateSlider();

        Speed = speed;

        isDashing = false;
    }

    void MovementHandler()
    {
        if (rb2d == null)
        {
            print("No " + "Rigidbody2D" + " Component Found.");
        }
        else
        {
            Vector2 Dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Dir = Dir.normalized * Speed * PlayerSpeedMod;

            rb2d.linearVelocity = Dir;

        }
    }


}
