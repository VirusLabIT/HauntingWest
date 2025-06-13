using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Breakble : MonoBehaviour
{
    public int lives;

    [SerializeField]GameObject[] ThingsToSpawn;
    [SerializeField] float offset;
    bool trigger = false;

    public void LiveUpdate()
    {
        lives--;

        if (lives <= 0)
        {
            SpawnThings();
        }
    }

    void SpawnThings()
    {
        if (!trigger)
        {
            StartCoroutine(ISpawnThings());
            trigger = true;
        }
        

    }
    
    IEnumerator ISpawnThings()
    {

        print("Ienum start");
        foreach (GameObject thing in ThingsToSpawn)
        {
            print("NewThing");

            int spawn = Random.Range(1, 3);

            if (spawn == 1)
            {
                print("spawn");
                Vector3 spawnpos = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 0);


                Instantiate(thing, transform.position + spawnpos, Quaternion.identity);
            }

            yield return new WaitForSecondsRealtime(.1f);

        }

        Destroy(gameObject, .5f);
    }
}
