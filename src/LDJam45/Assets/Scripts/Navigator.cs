using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Navigator : ScriptableObject
{
    public void NavigateToGameScene()
    {
        Cursor.visible = false;

        SceneManager.LoadScene("GameScene");
    }

    public void NavigateToMainMenu()
    {
        Cursor.visible = true;

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
        Cursor.visible = true;

        SceneManager.LoadScene("VictoryScene");
    }
}
