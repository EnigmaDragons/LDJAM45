using UnityEngine;

public class CooldownPresenter : MonoBehaviour
{
    [SerializeField] private GameObject dashHud;
    [SerializeField] private GameObject laserHud;
    [SerializeField] private GameState state;
    
    // TODO: Maybe make this reactive to save CPU
    private void Update()
    {
        dashHud.SetActive(state.DashUnlocked);
        laserHud.SetActive(state.LaserEyesUnlocked);
    }
}
