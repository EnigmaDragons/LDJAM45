using UnityEngine;

public class FlashWhileInvincible : MonoBehaviour
{
    [SerializeField] private GameState GameState;
    [SerializeField] private CharacterID ID;
    [SerializeField] private GameObject Flashing;

    private void Update()
    {
        if (GameState.IsInvincibleMap[ID.ID] && !Flashing.activeSelf)
            Flashing.SetActive(true);
        else if (!GameState.IsInvincibleMap[ID.ID] && Flashing.activeSelf)
            Flashing.SetActive(false);
    }
}
