using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField] List<GameObject> ErrorImage;
    private int _errorNumber = 0;
    private int _lives = 0;
    [SerializeField] GameObject GameOverPopup;

    public static Lives Instance;

    public int GetError() => _errorNumber;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _lives = ErrorImage.Count;
        _errorNumber = 0;

        if (GameSettings.Instance.GetContinuePreviousGame())
        {
            _errorNumber = Config.ErrorNumber();
            _lives = ErrorImage.Count - _errorNumber;

            for (int error = 0; error < _errorNumber; error++)
            {
                ErrorImage[error].SetActive(true);
            }
        }
        if (GameSettings.Instance.GetLoadHistory1() || GameSettings.Instance.GetLoadHistory2() || GameSettings.Instance.GetLoadHistory3() || GameSettings.Instance.GetLoadHistory4() || GameSettings.Instance.GetLoadHistory5())
        {
            for (int error = 0; error < ErrorImage.Count; error++)
            {
                ErrorImage[error].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnWrongNumber += WrongNumber;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnWrongNumber -= WrongNumber;
    }

    private void WrongNumber()
    {
        if(_errorNumber < ErrorImage.Count)
        {
            ErrorImage[_errorNumber].SetActive(true);
            _errorNumber++;
            _lives--;
        }
        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        if (_lives <= 0)
        {
            GameEvents.Instance.GameOverMethod();
            GameOverPopup.SetActive(true);
        }
    }

    public int GetErrorNumbers() => _errorNumber;
}
