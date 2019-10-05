using UnityEngine;

public class OnEventEnable : MonoBehaviour
{
    [SerializeField] private GameEvent trigger;
    [SerializeField] private GameObject target;

    private void OnEnable()
    {
        trigger.Subscribe(() => target.SetActive(true), this);
    }

    private void OnDisable()
    {
        trigger.Unsubscribe(this);
    }
}
