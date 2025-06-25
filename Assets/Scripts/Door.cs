using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] int LevelToLoad;
    [SerializeField] string LevelNameToLoad;
    [SerializeField] float DelayBeforeLoad = 0.5f;
    [SerializeField] bool LoadWithIndex = true;
    [SerializeField] StartDoorEffect doorEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Player entered the door trigger.");
            if (doorEffect != null)
            {
                doorEffect.Collid = true;
            }
            StartCoroutine(LoadLevel());
        }
    }


    IEnumerator LoadLevel()
    {
        yield return new WaitForSecondsRealtime(DelayBeforeLoad);
        if (LevelToLoad > 0 && LoadWithIndex)
        {
            SceneManager.LoadScene(LevelToLoad);
        }
        else if (!string.IsNullOrEmpty(LevelNameToLoad))
        {
            SceneManager.LoadScene(LevelNameToLoad);
        }
        else
        {
            Debug.LogError("No level specified to load.");
        }
    }

}
