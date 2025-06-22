using System.Collections;
using UnityEngine;

public class DoorBetweenRooms : MonoBehaviour
{
    [Header("Serialize")]
    [SerializeField] ParticleSystem ParticleSystem;

    [Header("Settings")]
    [SerializeField] float DelayBeforeClose = 1f;
    [SerializeField] float DelayBeforeOpen = 1f;
    [SerializeField] Collider2D doorCollider;
    public bool IsOpen;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!IsOpen)
            {
                IsOpen = true;
                StartCoroutine(OpenDoor());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsOpen)
            {
                IsOpen = false;
                StartCoroutine(CloseDoor());
            }
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(DelayBeforeOpen);
        doorCollider.enabled = false;
        ParticleSystem.Stop();

    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(DelayBeforeClose);
        doorCollider.enabled = true;
        ParticleSystem.Play();
    }
}
