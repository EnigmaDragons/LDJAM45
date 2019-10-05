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
        SceneManager.LoadScene("TitleScene");
    }

    public void NavigateToVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }
}
