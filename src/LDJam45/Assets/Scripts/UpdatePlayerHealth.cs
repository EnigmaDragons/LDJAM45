using UnityEngine;

public class UpdatePlayerHealth : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] private CharacterID PlayerID;
    [SerializeField] private GameEvent onPlayerHealthLost;
    [SerializeField] private GameEvent onPlayerHealthGained;
    [SerializeField] private GameEvent gameOver;

    private void Update()
    {
        var health = state.HealthMap[PlayerID.ID];
        if (health != state.CurrentPlayerHp)
        {
            if (health < state.CurrentPlayerHp)
            {
                state.CurrentPlayerHp = health;
                onPlayerHealthLost.Publish();
                Debug.Log($"Player Health is now {state.CurrentPlayerHp}");
                if (state.CurrentPlayerHp <= 0)
                {
                    Cursor.visible = true;
                    gameOver.Publish();
                }                  
            }
            else
            {
                state.CurrentPlayerHp = health;
                onPlayerHealthGained.Publish();
            }
        }
    }
}
