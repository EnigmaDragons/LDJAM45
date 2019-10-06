using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Navigator : ScriptableObject
{
    public void NavigateToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void NavigateToMainMenu()
    {
        // Menu UI from TitleScene is DontDestroyOnLoad object but it needs to be destroyed on return to TitleScene in order to avoid duplicate GameObjects
        GameObject menuUI = GameObject.Find("Menu UI");
        if (menuUI != null)
        {
            SceneManager.MoveGameObjectToScene(menuUI, SceneManager.GetActiveScene());
        }

        Time.timeScale = 1.0f;

        SceneManager.LoadScene("TitleScene");
    }

    public void NavigateToVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }
}
