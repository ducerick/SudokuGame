using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseMenu : MonoBehaviour
{
    [SerializeField] Text TimeText;

    private void OnEnable()
    {
        GameEvents.Instance.GamePauseMethod();
        TimeText.text = Clock.Instance.GetCurrentTime().text;
    }
}
