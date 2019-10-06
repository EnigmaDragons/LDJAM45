
using UnityEngine;

public class UnlockAbilityEvents : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] private GameEvent unlockDash;
    [SerializeField] private GameEvent unlockSlash;
    [SerializeField] private GameEvent unlockRend;
    [SerializeField] private GameEvent unlockLaser;

    private void OnEnable()
    {
        unlockDash.Subscribe(() => state.DashUnlocked = true, this);
        unlockSlash.Subscribe(() => state.SlashUnlocked = true, this);
        unlockRend.Subscribe(() => state.RendUnlocked = true, this);
        unlockLaser.Subscribe(() => state.LaserEyesUnlocked = true, this);
    }

    private void OnDisable()
    {
        unlockDash.Unsubscribe(this);
        unlockSlash.Unsubscribe(this);
        unlockRend.Unsubscribe(this);
        unlockLaser.Unsubscribe(this);
    }
}
