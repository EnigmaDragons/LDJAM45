using UnityEngine;

public class InvincibleAfterDamage : MonoBehaviour
{
    [SerializeField] private Health Health;
    [SerializeField] private float TimeInvisible;
    [SerializeField] private float TimeVisible;
    [SerializeField] private Renderer Renderer;

    private float _secondsTilChange;

    private void Update()
    {
        if (Health.IsInvincible)
        {
            _secondsTilChange -= Time.deltaTime;
            if (_secondsTilChange <= 0)
            {
                _secondsTilChange = Renderer.enabled ? TimeInvisible : TimeVisible;
                Renderer.enabled = !Renderer.enabled;
            }
        }
        else
        {
            _secondsTilChange = 0;
            Renderer.enabled = true;
        }
    }
}
