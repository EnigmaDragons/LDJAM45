using UnityEngine;

public class SaveCatId : MonoBehaviour
{
    [SerializeField] private GameState state;

    private void Start()
    {
        state.CatId = GetComponent<CharacterID>().ID;
        Debug.Log($"CatId = {state.CatId}");
    }
}
