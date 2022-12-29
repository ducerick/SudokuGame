using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameEvents Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public delegate void UpdateSquareNumber(int number);
    public event UpdateSquareNumber OnUpdateSquareNumber;

    public void UpdateSquareNumberMethod(int number)
    {
        if (OnUpdateSquareNumber != null)
            OnUpdateSquareNumber(number);
    }

    public delegate void SquareSelected(int squareIndex);
    public event SquareSelected OnSquareSelected;

    public void SquareSelectedMethod(int squareIndex)
    {
        if (OnSquareSelected != null)
            OnSquareSelected(squareIndex);
    }

    public delegate void WrongNumber();
    public event WrongNumber OnWrongNumber;

    public void WrongNumberMethod()
    {
        if(OnWrongNumber != null)
        {
            OnWrongNumber();
        }
    }

    public delegate void GameOver();
    public event GameOver OnGameOver;

    public void GameOverMethod()
    {
        if (OnGameOver != null)
        {
            OnGameOver();
        }
    }

    public delegate void GamePause();
    public event GamePause OnGamePause;

    public void GamePauseMethod()
    {
        if (OnGamePause != null)
            OnGamePause();
    }

    public delegate void NotesActive(bool active);
    public event NotesActive OnNotesActive;

    public void OnNotesActiveMethod(bool active)
    {
        if (OnNotesActive != null)
        {
            OnNotesActive(active);
        }
    }

    public delegate void ClearNumber();
    public event ClearNumber OnClearNumber;

    public void OnClearNumberMethod()
    {
        if (OnClearNumber != null)
        {
            OnClearNumber();
        }
    }

    public delegate void ShowMessage(int value);
    public event ShowMessage OnShowMessage;

    public void OnShowMessageMethod(int value)
    {
        if (OnShowMessage != null)
        {
            OnShowMessage(value);
        }
    }

    public delegate void QuestionActive(bool active);
    public event QuestionActive OnQuestionActive;

    public void OnQuestionActiveMethod(bool active)
    {
        if (OnQuestionActive != null)
        {
            OnQuestionActive(active);
        }
    }

}
