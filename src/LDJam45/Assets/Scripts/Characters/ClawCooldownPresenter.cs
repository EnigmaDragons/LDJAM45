﻿    using UnityEngine;
    using UnityEngine.UI;

public class ClawCooldownPresenter : MonoBehaviour
{
    [SerializeField] private Image DashOnCooldown;
    [SerializeField] private Text CooldownText;
    [SerializeField] private ClawSwipe ClawSwipe;

    private void Update()
    {
        DashOnCooldown.enabled = ClawSwipe.SwipeCooldownRemaining > 0;
        CooldownText.enabled = ClawSwipe.SwipeCooldownRemaining > 0;
        CooldownText.text = ClawSwipe.SwipeCooldownRemaining.ToString("0.0");
    }
}