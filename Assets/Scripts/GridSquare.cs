using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject NumberText;
    private int _number = 0;
    private int _squareIndex = -1;
    private bool _selected = false;
    private int _correctNumber = -1;
    private bool _enableChange = true;
    public bool HasWrongNumber { get; private set; } = false;
    public List<GameObject> NumberNotes;
    private bool _noteActive;
    private bool _questionActive;
    public bool IsCorrectNumberSet() => _number == _correctNumber;

    void Start()
    {
        _noteActive = false;
        _questionActive = false;

        if (GameSettings.Instance.GetContinuePreviousGame() == false)
        {
            DeActiveNumber();
        } else
        {
            OnClearNumber();
        }
    }

    public void DisplayText()
    {
        if (_number <= 0)
        {
            NumberText.GetComponent<Text>().text = " ";
        }
        else
        {
            NumberText.GetComponent<Text>().text = _number.ToString();
        }
    }

    public void SetNumber(int number)
    {
        _number = number;
        DisplayText();
    }

    public void SetNumberIndex(int squareIndex)
    {
        _squareIndex = squareIndex;
    }

    public void SetCorrectNumber(int number)
    {
        _correctNumber = number;

        if (_number != 0 && _number != _correctNumber)
        {
            HasWrongNumber = true;
            SetColorSquare(Color.red);
        }
    }

    public void SetCorrectNumber()
    {
        _number = _correctNumber;
        SetSingleNoteValue(0);
        DisplayText();
    }

    public void SetEnableChange(bool enable)
    {
        _enableChange = enable;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _selected = true;
        GameEvents.Instance.SquareSelectedMethod(_squareIndex);
        if (_questionActive)
        {
            GameEvents.Instance.OnShowMessageMethod(_correctNumber);
        }
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }

    private void OnEnable()
    {
        GameEvents.Instance.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.Instance.OnSquareSelected += OnSquareSelected;
        GameEvents.Instance.OnNotesActive += OnNotesActive;
        GameEvents.Instance.OnClearNumber += OnClearNumber;
        GameEvents.Instance.OnQuestionActive += OnQuestionActive;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.Instance.OnSquareSelected -= OnSquareSelected;
        GameEvents.Instance.OnNotesActive -= OnNotesActive;
        GameEvents.Instance.OnClearNumber -= OnClearNumber;
        GameEvents.Instance.OnQuestionActive -= OnQuestionActive;

    }

    private void OnSquareSelected(int squareIndex)
    {
        if (_squareIndex != squareIndex)
        {
            _selected = false;
            _questionActive = false;
            GameEvents.Instance.OnShowMessageMethod(-1);
        }
    }

    private void OnSetNumber(int number)
    {
        if (_selected && _enableChange && Lives.Instance.GetError() < 3)
        {
            if (_noteActive == true && HasWrongNumber == false)
            {
                SetSingleNoteValue(number);
            }
            else if (_noteActive == false)
            {
                DeActiveNumber();
                SetNumber(number);
                if (_correctNumber != number)
                {
                    var color = this.colors;
                    color.normalColor = Color.red;
                    this.colors = color;
                    GameEvents.Instance.WrongNumberMethod();
                    HasWrongNumber = true;
                }
                else
                {
                    SetColorSquare(Color.green);
                    HasWrongNumber = false;
                    _enableChange = false;
                    GameScore.Instance.SetSocre(1);
                }
            }
        }

    }

    public void SetColorSquare(Color col)
    {
        var color = this.colors;
        color.normalColor = col;
        this.colors = color;
    }

    public bool IsSelected() => _selected;

    public void SetSingleNoteValue(int value)
    {
        foreach (var number in NumberNotes)
        {
            if (number.GetComponent<Text>().text == value.ToString())
            {
                number.SetActive(true);
            }
        }
    }

    public void SetGridNotes(List<int> notes)
    {
        foreach (var number in NumberNotes)
        {
            if (notes.Contains(int.Parse(number.GetComponent<Text>().text)))
            {
                number.SetActive(true);
            }
            else
            {
                number.SetActive(false);
            }
        }
    }

    public List<string> GetSquareNotes()
    {
        List<string> list = new List<string>();
        foreach (var number in NumberNotes)
        {
            if (number.activeSelf)
            {
                list.Add(number.GetComponent<Text>().text);
            }
        }

        return list;
    }

    public int GetSquareNumber()
    {
        return _number;
    }


    private void OnNotesActive(bool active)
    {
        _noteActive = active;
    }

    private void DeActiveNumber()
    {
        foreach (var number in NumberNotes)
        {
            number.SetActive(false);
        }
    }


    private void OnClearNumber()
    {
        if (_selected && _enableChange)
        {
            _number = 0;
            HasWrongNumber = false;
            SetColorSquare(Color.white);
            DisplayText();
            DeActiveNumber();
        }
    }

    private void OnQuestionActive(bool active)
    {
        _questionActive = active;
    }


}
