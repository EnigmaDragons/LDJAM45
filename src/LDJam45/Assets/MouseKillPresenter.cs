using TMPro;
using UnityEngine;

public class MouseKillPresenter : MonoBehaviour
{
    [SerializeField] private GameEvent MouseDied;
    [SerializeField] private TextMeshProUGUI Text;

    private int _miceKilled = 0;

    private void OnEnable() => MouseDied.Subscribe(() => _miceKilled++, this);
    private void OnDisable() => MouseDied?.Unsubscribe(this);
    private void Update() => Text.text = _miceKilled == 0 ? "" : _miceKilled.ToString();
}
