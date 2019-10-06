using UnityEngine;
using UnityEngine.UI;

public class FlashColorOnPlayerDamage : MonoBehaviour
{
    [SerializeField] private GameEvent onDamaged;

    private Image image;
    private Color targetColor;
    private Color targetTransparent;

    bool wasFreshlyDamaged;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        targetColor = new Color(image.color.r, image.color.g, image.color.b, 0.6f);
        targetTransparent = new Color(image.color.r, image.color.g, image.color.b, 0f);
        onDamaged.Subscribe(() => wasFreshlyDamaged = true, this);
    }

    private void OnDisable()
    {
        onDamaged.Unsubscribe(this);
    }

    void Update()
    {
        if (wasFreshlyDamaged)
            image.color = Color.Lerp(image.color, targetColor, 20 * Time.deltaTime);
        else
            image.color = Color.Lerp(image.color, targetTransparent, 20 * Time.deltaTime);

        if (image.color.a >= 0.6)
            wasFreshlyDamaged = false;
    }
}
