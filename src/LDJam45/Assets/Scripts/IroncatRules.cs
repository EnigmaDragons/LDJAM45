using UnityEngine;

public class IroncatRules : MonoBehaviour
{
    [SerializeField] private GameState GameState;
    [SerializeField] private GameEvent UnlockDash;
    [SerializeField] private GameEvent UnlockSlash;

    private bool _init;

    private void Update()
    {
        if (_init)
            return;
        _init = true;
        if (GameState.PlayIronmanMode)
        {
            UnlockDash.Publish();
            UnlockSlash.Publish();
        }
    }
}
