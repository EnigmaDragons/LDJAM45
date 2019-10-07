using UnityEngine;

public class GainLossHealthDebugger : MonoBehaviour
{
    [SerializeField] private GameState state;

    void Update()
    {
        if (!Application.isEditor)
            return;

        if (Input.GetKeyDown(KeyCode.U))
            state.Gain1PlayerHealth();
        if (Input.GetKeyDown(KeyCode.I))
            state.Lose1PlayerHealth();
    }
}
