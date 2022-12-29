using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ContinueButton : MonoBehaviour
{
    public Text TimeText;
    public Text LevelText;

    // Start is called before the first frame update
    void Start()
    {
        Config.SetDefaultPath();
        if(Config.GameDataFileExist() == false)
        {
            gameObject.GetComponent<Button>().interactable = false;
            TimeText.text = " ";
            LevelText.text = " ";
        } else
        {
            float delta_time = Config.ReadGameTime();
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);

            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);

            TimeText.text = hour + ":" + minute + ":" + seconds;

            LevelText.text = Config.ReadBoradLevel();
        }
    }

    private string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

 
    public void SetGameData()
    {
        GameSettings.Instance.SetGameMode(Config.ReadBoradLevel());
    }
}
