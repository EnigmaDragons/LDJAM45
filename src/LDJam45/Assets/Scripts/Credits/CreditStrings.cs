using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditStrings : MonoBehaviour
{
    [SerializeField] private List<string> Strings;
    [SerializeField] private Text Text;
    [SerializeField] private Image Panel;
    private int _index;

    private void Start()
    {
        _index = 0;
        Text.text = Strings[_index];
    }

    public void Next()
    {
        _index++;
        Text.text = _index >= Strings.Count ? "" : Strings[_index];
        if (_index >= Strings.Count)
            Panel.enabled = false;
    }
}
