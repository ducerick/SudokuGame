using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Suggestion : MonoBehaviour
{
    [SerializeField] Text ShowText;
    [SerializeField] Text NumberSuggest;
    private int _numberSuggest;

    public static Suggestion Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        _numberSuggest = 3;
        NumberSuggest.text = _numberSuggest.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (_numberSuggest >= 0)
        {
            NumberSuggest.text = _numberSuggest.ToString();
        } else
        {
            ShowText.text = "";
        }
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnShowMessage += OnShowMessage;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnShowMessage -= OnShowMessage;
    }

    public int GetSuggest() => _numberSuggest;

    public void SubSegguest()
    {
        _numberSuggest -= 1;
    }

    private void OnShowMessage(int value)
    {
        string s = Check(value);
        ShowText.text = s;
    }

    private string Check(int value)
    {
        if (value == -1) return "";
        string s;
        if (value % 2 == 0)
        {
            s = "This's an even number!";
        } else if (value % 3 == 0) {
            s = "This's a number that is divisible by 3!";
        } else
        {
            s = "Here are some primes!";
        }
        return s;
    }
}
