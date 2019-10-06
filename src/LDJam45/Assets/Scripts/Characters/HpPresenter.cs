using UnityEngine;

public class HpPresenter : MonoBehaviour
{
    [SerializeField] private GameEvent onHealthLost;
    [SerializeField] private GameEvent onHealthGained;
    [SerializeField] private GameState state;

    private GameObject[] hpIcons;

    private void OnEnable()
    {
        onHealthGained.Subscribe(UpdateHealth, this);
        onHealthLost.Subscribe(UpdateHealth, this);
        hpIcons = new GameObject[transform.childCount];
        for (int i = 0; i < hpIcons.Length; ++i)
            hpIcons[i] = transform.GetChild(i).gameObject;
    }

    private void Start()
    {
        UpdateHealth();
    }

    private void OnDisable()
    {
        onHealthLost.Unsubscribe(this);
        onHealthGained.Unsubscribe(this);
    }

    void UpdateHealth()
    {
        for (int i = 0; i < hpIcons.Length; ++i)
            hpIcons[i].SetActive(i < state.CurrentPlayerHp);
    }
}
