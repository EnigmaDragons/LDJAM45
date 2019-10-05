using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownPresenter : MonoBehaviour
{
    [SerializeField] private Image DashOnCooldown;
    [SerializeField] private Text CooldownText;
    [SerializeField] private Dash Dash;

    private void Update()
    {
        DashOnCooldown.enabled = Dash.DashCooldownRemaining > 0;
        CooldownText.enabled = Dash.DashCooldownRemaining > 0;
        CooldownText.text = Dash.DashCooldownRemaining.ToString("0.0");
    }
}
