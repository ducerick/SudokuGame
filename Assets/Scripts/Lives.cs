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

    // Start is called before the first frame update
    void Start()
    {
        _lives = ErrorImage.Count;
        _errorNumber = 0;
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
}
