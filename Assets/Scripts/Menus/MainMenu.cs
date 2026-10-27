using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]private string levelToLoad;
    public GameObject optionsMenu;
    public GameObject levelSelect;
    public bool OptionsMenuActive = false;
    public void startGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void quitButton()
    {
        Application.Quit();
    }
    public void optionsAndCredits()
    {
        if(OptionsMenuActive == false)
        {
            optionsMenu.SetActive(true);
            OptionsMenuActive = true;
        }
        else
        {
            optionsMenu.SetActive(false);
            OptionsMenuActive = false;
        }
    }

    public void OpenLevelSelect()
    {
        levelSelect.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        levelSelect.SetActive(false);
    }
    public void LoadLevel(string levelName)
{
    Time.timeScale = 1f;
    SceneManager.LoadScene(levelName);
}
    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
