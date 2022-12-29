using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseMenu : MonoBehaviour
{
    [SerializeField] Text TimeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        GameEvents.Instance.GamePauseMethod();
        TimeText.text = Clock.Instance.GetCurrentTime().text;
    }
}
