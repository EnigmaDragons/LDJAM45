using UnityEngine;
using UnityEngine.UI;

public class LaserEyesCooldownPresenter : MonoBehaviour
{
    [SerializeField] private Image OnCooldownImage;
    [SerializeField] private Text CooldownText;

    private LaserEyes LaserEyes;

    private void Start()
    {
        LaserEyes = FindObjectOfType<LaserEyes>();
    }

    private void Update()
    {
        OnCooldownImage.enabled = LaserEyes.LaserEyesCooldownRemaining > 0;
        CooldownText.enabled = LaserEyes.LaserEyesCooldownRemaining > 0;
        CooldownText.text = LaserEyes.LaserEyesCooldownRemaining.ToString("0.0");
    }
}