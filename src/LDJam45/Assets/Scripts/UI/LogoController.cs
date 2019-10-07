
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float showDuration = 2f;
    [SerializeField] private float transitionDuration = 0.75f;

    private Color targetColor;
    private Color targetTransparent;

    private float _fadingInFinishedInSeconds;
    private float _startFadingOutInSeconds;
    private float _finishInSeconds;
    private bool _finishedCurrent;

    private void Awake()
    {
        targetColor = image.color;
        targetTransparent = new Color(targetColor.r, targetColor.g, targetColor.b, 0f);
        BeginAnim();
    }

    private void FixedUpdate()
    {
        UpdateCounters();
        UpdatePresentation();
        if (_finishedCurrent)
            NavigateToMainMenu();
    }


    private void BeginAnim()
    {
        _finishedCurrent = false;
        _finishInSeconds = showDuration + transitionDuration * 2;
        _fadingInFinishedInSeconds = transitionDuration;
        _startFadingOutInSeconds = transitionDuration + showDuration;
    }

    private void UpdatePresentation()
    {
        if (_finishedCurrent)
            return;

        if (_fadingInFinishedInSeconds > 0.01f)
            image.color = Color.Lerp(targetTransparent, targetColor, (transitionDuration - _fadingInFinishedInSeconds) / transitionDuration);
        if (_startFadingOutInSeconds < 0.1f)
            image.color = Color.Lerp(targetColor, targetTransparent, (transitionDuration - _finishInSeconds) / transitionDuration);
        if (_finishInSeconds < 0.01f)
            _finishedCurrent = true;
    }

    private void UpdateCounters()
    {
        _fadingInFinishedInSeconds = Mathf.Max(0, _fadingInFinishedInSeconds - Time.deltaTime);
        _startFadingOutInSeconds = Mathf.Max(0, _startFadingOutInSeconds - Time.deltaTime);
        _finishInSeconds = Mathf.Max(0, _finishInSeconds - Time.deltaTime);
    }

    private void NavigateToMainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
