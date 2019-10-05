﻿using UnityEngine;

public class HpPresenter : MonoBehaviour
{
    [SerializeField] private GameEvent onHealthLost;
    [SerializeField] private GameEvent onHealthGained;
    [SerializeField] private Health PlayerHealth;

    private GameObject[] hpIcons;

    private void OnEnable()
    {
        onHealthGained.Subscribe(IncrementHealth, this);
        onHealthLost.Subscribe(DecrementHealth, this);
        hpIcons = new GameObject[transform.childCount];
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
        for (int i = 0; i < hpIcons.Length; ++i)
            hpIcons[i].SetActive(i < PlayerHealth.CurrentHealth);
    }
}