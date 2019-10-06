using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateOnUnlock : MonoBehaviour
{
    [SerializeField] private GameEvent UnlockEvent;
    [SerializeField] private GameObject Icon;

    private void OnEnable() {
        UnlockEvent.Subscribe(() => {            
            Vector3 EndPosition = Icon.transform.position;
            Icon.transform.position = Icon.transform.position + new Vector3(250, 250, 0);
            Icon.transform.localScale = new Vector3(2.0f, 2.0f);
            Icon.transform.DOMove(EndPosition, 1.0f);
            Icon.transform.DOScale(1, 1.0f);
            UnlockEvent.Unsubscribe(this);
        }, this);
    }

    private void OnDisable() {
        UnlockEvent.Unsubscribe(this);
    }
}
