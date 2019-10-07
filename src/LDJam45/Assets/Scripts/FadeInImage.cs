using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour
{
    [SerializeField] private float duration = 0.75f;
    [SerializeField] private Image image;

    private readonly float _originalAmount = 1.0f;
    private Color _original = Color.white;

    private void Start()
    {
        _original = image.color;
    }

    private void OnEnable()
    {
        image.CrossFadeAlpha(0f, 0f, true);
        image.CrossFadeAlpha(255f, duration, true);
    }
}
