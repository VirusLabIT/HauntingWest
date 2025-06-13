using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FastEnemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int Life = 3;
    public int Damage;

    [Header("Attack Stats")]
    [SerializeField] float IdleSpeed;
    [SerializeField] float AttackSpeed;
    [SerializeField] float MinTimeAttack;
    [SerializeField] float MaxTimeAttack;
    [SerializeField] GameObject AttackBox;
    [SerializeField] float AttackTime;
    [SerializeField] float AttackDelay;
    [SerializeField] float TimeToWaitBeforeCanceling;

    [Header("Serializetion")]
    [SerializeField] GameObject Player;
    [SerializeField] bool IsAttacking = true;
    [SerializeField] GameObject Coin;

    [Header("RandomStats")]
    [SerializeField] float PosRanRadios = 2.0f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int MinCoinsToSpawn = 0;
    [SerializeField] int MaxCoinsToSpawn = 3;

    bool isPlayerDirect;
    bool isSpawning = false;
    bool isGoingToRanPos = false;
    bool IsPlayerDetected = false;
    bool IsBulletDetected = false;
    NavMeshAgent agent;
    FastEnemyAttackDitection attackDitection;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        attackDitection = GetComponentInChildren<FastEnemyAttackDitection>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Player = GameObject.Find("Player");
    }


    private void Update()
    {
        IsPlayerDetected = GetComponentInChildren<EnemyDitection>().IsPlayerDetected;

        isPlayerDirect = IsAnyThingBetweenPlayer(Player.transform.position);

        if (IsPlayerDetected && isPlayerDirect || IsBulletDetected)
        {
            SetTarget(Player.transform.position);
            LookAt(Player.transform.position);
            if (IsAttacking) { Spawn(); }

            if (!IsPlayerDetected)
            {
                IsBulletDetected = false;
            }
        }
        else
        {
            if (!isGoingToRanPos)
            {
                StartCoroutine(GoToRanPos());
            }
        }
    }

    IEnumerator GoToRanPos()
    {
        isGoingToRanPos = true;
        print("No Player Detected Portocol");
        while (!IsPlayerDetected || !isPlayerDirect)
        {
            agent.speed = IdleSpeed;

            Vector3 pos = PickRanDestenation(transform);
            //Debug.Log("SettingTarget " + pos);
            SetTarget(pos);

            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance || agent.velocity.sqrMagnitude > 0f)
            {
                // Check again mid-movement if player becomes visible
                if (IsPlayerDetected && isPlayerDirect)
                {
                    Debug.Log("Player spotted while moving. Cancelling roaming.");
                    isGoingToRanPos = false;
                    yield break;
                }

                LookAt(pos);
                yield return null;
            }

            Debug.Log("Destination reached");
            yield return new WaitForSeconds(.1f);
        }

        isGoingToRanPos = false;
    }


    void SpawnCoins()
    {
        int coinstospawn = Random.Range(MinCoinsToSpawn, MaxCoinsToSpawn--);

        print("Coins Spawned: " + coinstospawn);

        for (int i = 0; i != coinstospawn; ++i)
        {

            Vector3 Ofsset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            Vector3 pos = gameObject.transform.position + Ofsset;

            Instantiate(Coin, pos, Quaternion.Euler(0, 0, 0));
        }
    }


    bool IsAnyThingBetweenPlayer(Vector3 target)
    {
        Vector3 dir = (target - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Vector3.Distance(transform.position, target), playerLayer);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawLine(transform.position, target, Color.green);
                return true;
            }
            else
            {
                Debug.DrawLine(transform.position, target, Color.yellow);
            }
        }

        return false;

    }

    Vector3 PickRanDestenation(Transform position)
    {

        for (int i = 0; i < 10; i++)
        {
            float randomModX = Random.Range(-PosRanRadios, PosRanRadios);
            float randomModY = Random.Range(-PosRanRadios, PosRanRadios);

            Vector3 offset = new Vector3(randomModX, randomModY, 0);
            Vector3 rawPos = position.position + offset;

            if (NavMesh.SamplePosition(rawPos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        print("CantFindNavMesh");
        return position.position;
    }





    void LookAt(Vector3 target)
    {
        Vector3 lool = transform.InverseTransformPoint(target);

        float angle = Mathf.Atan2(lool.y, lool.x) * Mathf.Rad2Deg - 90;

        transform.Rotate(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !collision.GetComponent<Bullet>().hitDetection && !collision.GetComponent<Bullet>().bulletDetection)
        {
            int damage = collision.GetComponent<Bullet>().Damage;

            Life -= damage;
            print(Life);

            if (Life <= 0)
            {
                SpawnCoins();
                Destroy(gameObject);
            }

            IsBulletDetected = false;
        }
        else if (collision.GetComponent<Bullet>() != null && collision.GetComponent<Bullet>().bulletDetection)
        {
            IsBulletDetected = true;
        }


    }


    void SetTarget(Vector3 target)
    {
        agent.SetDestination(target);
    }



    void Spawn()
    {

        if (!isSpawning)
        {
            StartCoroutine(ISpawn());
        }

    }

    IEnumerator ISpawn()
    {
        isSpawning = true;

        print("Shoot");

        agent.speed = AttackSpeed;

        float elapsedTime = 0;

        float timeBefore = Time.realtimeSinceStartup;

        while (!attackDitection.IsPlayerDetectedInAttackRadios)
        {
            elapsedTime = (Time.realtimeSinceStartup) - timeBefore;
            if (elapsedTime > TimeToWaitBeforeCanceling)
            {
                agent.speed = IdleSpeed;

                float intime = Random.Range(MinTimeAttack, MaxTimeAttack);

                yield return new WaitForSecondsRealtime(intime);

                isSpawning = false;
                yield break;
            }
            print(elapsedTime);
            yield return null;
        }

        print("Attacking");
        yield return new WaitForSecondsRealtime(AttackDelay);

        AttackBox.SetActive(true);

        yield return new WaitForSecondsRealtime(AttackTime);

        AttackBox.SetActive(false);

        agent.speed = IdleSpeed;

        float time = Random.Range(MinTimeAttack, MaxTimeAttack);

        yield return new WaitForSecondsRealtime(time);

        isSpawning = false;
    }
}
