using UnityEngine;

public class UpdatePlayerHealth : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] private GameEvent onHealthLost;
    [SerializeField] private GameEvent onHealthGained;
    [SerializeField] private GameEvent gameOver;

    private void OnEnable()
    {
        onHealthGained.Subscribe(() => ChangeHealthBy(1), this);
        onHealthLost.Subscribe(() => ChangeHealthBy(-1), this);
    }

    private void OnDisable()
    {
        onHealthGained.Unsubscribe(this);
        onHealthLost.Unsubscribe(this);
    }

    void ChangeHealthBy(int amount)
    {
        state.CurrentHp += amount;
        Debug.Log($"Player Health is now {state.CurrentHp}");
        if (state.CurrentHp <= 0)
            gameOver.Publish();
    }
}
