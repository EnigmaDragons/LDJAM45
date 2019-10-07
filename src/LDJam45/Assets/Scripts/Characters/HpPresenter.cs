using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class HpPresenter : MonoBehaviour
{
    [SerializeField] private GameEvent onHealthLost;
    [SerializeField] private GameEvent onHealthGained;
    [SerializeField] private GameState state;

    [SerializeField] private float Delay;

    [SerializeField] private Sprite Health2;
    [SerializeField] private Sprite Health3;

    private GameObject[] hpIcons;
    private bool DelayTrigger = false;

    private void OnEnable()
    {
        onHealthGained.Subscribe(GainHealth, this);
        onHealthLost.Subscribe(LoseHealth, this);                
    }

    private void OnDisable() {
        onHealthLost.Unsubscribe(this);
        onHealthGained.Unsubscribe(this);
    }

    private void Start()
    {
        hpIcons = new GameObject[transform.childCount];
        for (int i = 0; i < hpIcons.Length; ++i) {
            hpIcons[i] = transform.GetChild(i).gameObject;
        }

        Debug.Log(hpIcons.Length);        
    }

    private void LateUpdate() {
        // HACK: Delay until late update, GameState is 0 at Start
        if (!DelayTrigger) {
            DrawHealth();
            DelayTrigger = true;
        }
    }

    void DrawHealth()
    {
        Debug.Log(state.CurrentPlayerHp);
        for (int i = 0; i < hpIcons.Length; ++i) {
            //if (i > state.CurrentPlayerHp - 1) {
            //    hpIcons[i].transform.localScale = Vector3.zero;
            //}
            hpIcons[i].SetActive(i < state.CurrentPlayerHp);
        }            
    }

    void GainHealth() {
        hpIcons[state.CurrentPlayerHp].transform.DOScale(1, 1.0f).OnComplete(() => DrawHealth());
    }

    void LoseHealth() {
        // hpIcons[state.CurrentPlayerHp].transform.DOScale(0, 1.0f).OnComplete(() => DrawHealth());
        StartCoroutine(AnimateHealthLoss());        
    }

    IEnumerator AnimateHealthLoss() {
        Image image = hpIcons[state.CurrentPlayerHp].GetComponent<Image>();        
        image.sprite = Health2;
        yield return new WaitForSeconds(Delay);

        image.sprite = Health3;
        yield return new WaitForSeconds(Delay);

        hpIcons[state.CurrentPlayerHp].SetActive(false);
    }
}
