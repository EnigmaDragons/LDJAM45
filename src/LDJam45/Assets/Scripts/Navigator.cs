using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Navigator : ScriptableObject
{
    public static void NavigateToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public static void NavigateToMainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
