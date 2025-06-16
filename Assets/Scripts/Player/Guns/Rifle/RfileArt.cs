using UnityEngine;

public class RifleArt : MonoBehaviour
{
    [Header("Types")]
    public RifleArtState rifleArtState;

    [Header("Lists")]
    [SerializeField] GameObject[] Types;
    [SerializeField] GameObject[] BulletShootingPoint;
    [SerializeField] int[] DamageForEachType;
    [SerializeField] float[] ReloadTimeForEachType;

    [Header("Var")]
    public GameObject ShootingPoint;
    public Animator animator;
    public int Damage;
    public float ReloadTime;

    public enum RifleArtState
    {
        Standard = 0,
        Rusty = 1,
        Guilded = 2
    }

    void Awake()
    {
        UpdateGunState();
    }

    public void UpdateGunState()
    {
        foreach (GameObject type in Types)
        {
            if (type.name.ToLower() == rifleArtState.ToString().ToLower())
            {
                type.SetActive(true);

                animator = type.GetComponentInChildren<Animator>();

                ShootingPoint = BulletShootingPoint[((int)rifleArtState)];

                Damage = DamageForEachType[((int)rifleArtState)];

                ReloadTime = ReloadTimeForEachType[((int)rifleArtState)];

                type.GetComponentInParent<Rifle>().SetUpGun();
            }
            else
            {
                type.SetActive(false);
                print("RifleArt Type:" + type.name);
            }
        }
    }
}
