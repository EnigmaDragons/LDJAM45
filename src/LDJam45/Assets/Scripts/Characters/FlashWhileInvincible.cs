using UnityEngine;

public class FlashWhileInvincible : MonoBehaviour
{
    [SerializeField] private Health Health;
    [SerializeField] private GameObject Flashing;

    private float _secondsTilChange;

    private void Update()
    {
        if (Health.JustGotHit && !Flashing.activeSelf)
        {
            Flashing.SetActive(true);
        }
        else if (!Health.JustGotHit && Flashing.activeSelf)
        {
            Flashing.SetActive(false);
        }
    }
}
