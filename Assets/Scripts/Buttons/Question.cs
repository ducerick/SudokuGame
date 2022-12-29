using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;


public class Question : Selectable, IPointerClickHandler
{
    private bool _active;
    public void OnPointerClick(PointerEventData eventData)
    {
        Suggestion.Instance.SubSegguest();
        if (Suggestion.Instance.GetSuggest() >= 0)
        {
            _active = true;
            GameEvents.Instance.OnQuestionActiveMethod(_active);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
