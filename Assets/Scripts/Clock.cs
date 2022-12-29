using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private int _hour;
    private int _minute;
    private int _seconds;
    private float _deltaTime;
    private bool _stopClock;

    private Text _textClock;

    [SerializeField] Image ClockImage;

    public static Clock Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);

        _textClock = GetComponent<Text>();
        _deltaTime = 0;

        if (GameSettings.Instance.GetContinuePreviousGame())
        {
            _deltaTime = Config.ReadGameTime();
        }
        else
        {
            _deltaTime = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _stopClock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSettings.Instance.GetContinuePreviousGame())
        {
            float delta_time = Config.ReadGameTime();
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);

            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);

            _textClock.text = hour + ":" + minute + ":" + seconds;
        }
        if (!_stopClock && Lives.Instance.GetError() < 3)
        {
            _deltaTime += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(_deltaTime);
            string hour = span.Hours.ToString().PadLeft(2, '0');
            string minute = span.Minutes.ToString().PadLeft(2, '0');
            string seconds = span.Seconds.ToString().PadLeft(2, '0');
            _textClock.text = hour + ":" + minute + ":" + seconds;

            ClockImage.transform.rotation = Quaternion.Lerp(ClockImage.transform.rotation, ClockImage.transform.rotation * Quaternion.Euler(0, 0, 90), Time.deltaTime);
        }
        
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnGameOver += OnGameOver;
        GameEvents.Instance.OnGamePause += OnGamePause;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnGameOver -= OnGameOver;
        GameEvents.Instance.OnGamePause -= OnGamePause;

    }

    private void OnGamePause()
    {
        _stopClock = true;
    }

    private void OnGameOver()
    {
        _stopClock = true;
    }
    
    public Text GetCurrentTime()
    {
        return _textClock;
    }

    public void ReturnGame()
    {
       _stopClock = false;
    }

    public static string GetCurrentTimeString()
    {
        return Instance._deltaTime.ToString();
    }

    private string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}
