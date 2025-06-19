using Unity.VisualScripting;
using UnityEngine;

public class StartDoorEffect : MonoBehaviour
{
    [SerializeField] GameObject doorEffectPrefab;
    [SerializeField] GameObject Player;
    [SerializeField] Animator doorAnimator;
    public bool Collid = false;

    void Update()
    {
        doorEffectPrefab.transform.position = Player.transform.position;
        CollidWithPlayer();
    }

    void CollidWithPlayer()
    {
        if (Collid)
        {
            doorAnimator.SetTrigger("Close");
            Collid = false;
        }
    }
}
