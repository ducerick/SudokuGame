using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameWon : MonoBehaviour
{
    public GameObject WinPopup;
    public Text ClockTime;

    private void Start()
    {
        WinPopup.SetActive(false);
    }

    private void OnBoardCompleted()
    {
        WinPopup.SetActive(true);
        ClockTime.text = Clock.Instance.GetCurrentTime().text;
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnBoardCompleted += OnBoardCompleted;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnBoardCompleted -= OnBoardCompleted;
    }

}
