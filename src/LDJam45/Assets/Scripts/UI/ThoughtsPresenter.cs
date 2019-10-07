using TMPro;
using UnityEngine;

public class ThoughtsPresenter : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float showDuration = 2f;
    [SerializeField] private float transitionDuration = 1f;

    private float _hideInSeconds;
    private bool _finishedCurrent;

    private void FixedUpdate()
    {
        _hideInSeconds = Mathf.Max(0, _hideInSeconds - Time.deltaTime);
        if (!_finishedCurrent && _hideInSeconds < 0.01f)
        {
            _finishedCurrent = true;
            text.text = string.Empty;
        }

        if (state.ThoughtsMessageQueue.Count <= 0)
            return;

        _finishedCurrent = false;
        text.text = state.ThoughtsMessageQueue.Dequeue();
        _hideInSeconds = showDuration + transitionDuration;
    }
}