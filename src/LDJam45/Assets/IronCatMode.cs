using UnityEngine;
using UnityEngine.UI;

public class IronCatMode : MonoBehaviour
{
    [SerializeField] private GameState GameState;
    [SerializeField] private Toggle Checkbox;

    void Start()
    {
        Checkbox.isOn = GameState.PlayIronmanMode;
    }

    public void Toggle()
    {
        GameState.PlayIronmanMode = Checkbox.isOn;
    }
}
