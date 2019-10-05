using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownPresenter : MonoBehaviour
{
    [SerializeField] private Image DashOnCooldown;
    [SerializeField] private Text CooldownText;

    private Dash _dash;

    private void Start()
    {
        _dash = FindObjectOfType<Dash>();
    }

    private void Update()
    {
        DashOnCooldown.enabled = _dash.DashCooldownRemaining > 0;
        CooldownText.enabled = _dash.DashCooldownRemaining > 0;
        CooldownText.text = _dash.DashCooldownRemaining.ToString("0.0");
    }
}
