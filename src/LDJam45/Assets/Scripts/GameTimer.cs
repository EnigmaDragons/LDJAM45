using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private GameState GameState;

    private void Update()
    {
        if (!GameState.IsInCutscene)
            GameState.RunTime += Time.deltaTime;
    }
}
