using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownPresenter : MonoBehaviour
{
    [SerializeField] private Image OnCooldownImage;
    [SerializeField] private Text CooldownText;

    private CatDash _dash;

    private void Start()
    {
        _dash = FindObjectOfType<CatDash>();
    }

    private void Update()
    {
        OnCooldownImage.enabled = _dash.DashCooldownRemaining > 0;
        CooldownText.enabled = _dash.DashCooldownRemaining > 0;
        CooldownText.text = _dash.DashCooldownRemaining.ToString("0.0");
    }
}
