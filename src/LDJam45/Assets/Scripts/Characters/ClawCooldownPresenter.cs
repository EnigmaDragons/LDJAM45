    using UnityEngine;
    using UnityEngine.UI;

public class ClawCooldownPresenter : MonoBehaviour
{
    [SerializeField] private Image OnCooldownImage;
    [SerializeField] private Text CooldownText;

    private ClawSwipe ClawSwipe;

    private void Start()
    {
        ClawSwipe = FindObjectOfType<ClawSwipe>();
    }

    private void Update()
    {
        OnCooldownImage.enabled = ClawSwipe.SwipeCooldownRemaining > 0;
        CooldownText.enabled = ClawSwipe.SwipeCooldownRemaining > 0;
        CooldownText.text = ClawSwipe.SwipeCooldownRemaining.ToString("0.0");
    }
}
