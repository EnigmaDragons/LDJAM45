using UnityEngine;
using UnityEngine.UI;

public class IronCatMode : MonoBehaviour
{
    [SerializeField] private GameState GameState;
    [SerializeField] private Toggle Checkbox;
    
    private void OnEnable()
    {
        GameState.Reset();
        gameObject.SetActive(GameState.IronmanModeUnlocked);
    }

    void Start()
    {
        GameState.PlayIronmanMode = false;
        Checkbox.isOn = GameState.PlayIronmanMode;
    }

    public void Toggle()
    {
        GameState.PlayIronmanMode = Checkbox.isOn;
    }
}
