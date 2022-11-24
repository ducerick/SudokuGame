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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayText()
    {
        if(_number <= 0)
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
    }

    public void SetEnableChange(bool enable)
    {
        _enableChange = enable;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _selected = true;
        GameEvents.Instance.SquareSelectedMethod(_squareIndex);
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }

    private void OnEnable()
    {
        GameEvents.Instance.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.Instance.OnSquareSelected += OnSquareSelected;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.Instance.OnSquareSelected -= OnSquareSelected;
    }

    private void OnSquareSelected(int squareIndex)
    {
        if (_squareIndex != squareIndex) _selected = false;
    }

    private void OnSetNumber(int number)
    {
        if (_selected && _enableChange)
        {
            SetNumber(number);
            if(_correctNumber != number)
            {
                var color = this.colors;
                color.normalColor = Color.red;
                this.colors = color;
                GameEvents.Instance.WrongNumberMethod();
                HasWrongNumber = true;
            }
            else {
                var color = this.colors;
                color.normalColor = Color.white;
                this.colors = color;
                HasWrongNumber = false;
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

}
