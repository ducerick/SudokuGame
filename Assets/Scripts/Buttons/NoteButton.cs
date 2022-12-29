using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class NoteButton : Selectable, IPointerClickHandler
{
    [SerializeField] Sprite OnImage;
    [SerializeField] Sprite OffImage;

    private bool _active;
    
    // Start is called before the first frame update
    void Start()
    {
        _active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _active = !_active;
        if (_active)
        {
            GetComponent<Image>().sprite = OnImage;
        }
        else
        {
            GetComponent<Image>().sprite = OffImage;
        }

        GameEvents.Instance.OnNotesActiveMethod(_active);
    }
}
