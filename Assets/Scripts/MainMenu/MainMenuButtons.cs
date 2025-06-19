using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [Header("Panles")]
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject AboutPanle;






    public void OpenAboutPanel()
    {
        MainMenuPanel.SetActive(false);
        AboutPanle.SetActive(true);
    }

    public void CloseAboutPanel()
    {
        MainMenuPanel.SetActive(true);
        AboutPanle.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
        print("Quit Game");
    }
}
