using UnityEngine;

public class HpPresenter : MonoBehaviour
{
    [SerializeField] private GameEvent onHealthLost;
    [SerializeField] private GameEvent onHealthGained;
    [SerializeField, ReadOnly] private int currentHealth;

    private GameObject[] hpIcons;

    private void OnEnable()
    {
        onHealthGained.Subscribe(IncrementHealth, this);
        onHealthLost.Subscribe(DecrementHealth, this);
        hpIcons = new GameObject[transform.childCount];
        currentHealth = hpIcons.Length;
        for (int i = 0; i < hpIcons.Length; ++i)
            hpIcons[i] = transform.GetChild(i).gameObject;
    }

    private void OnDisable()
    {
        onHealthLost.Unsubscribe(this);
        onHealthGained.Unsubscribe(this);
    }

    void DecrementHealth() => UpdateHealth(-1);
    void IncrementHealth() => UpdateHealth(1);

    void UpdateHealth(int amount)
    {
        currentHealth += amount;
        Debug.Log($"Health is now {currentHealth}");
        for (int i = 0; i < hpIcons.Length; ++i)
            hpIcons[i].SetActive(i < currentHealth);
    }
}
