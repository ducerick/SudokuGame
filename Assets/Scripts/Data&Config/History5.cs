using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class History5 : MonoBehaviour
{
    public Image ImageHS;
    public Text LevelHS;
    public Text ScoreHS;
    public Text TimeHS;
    //public Text ModeHS;

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
        Config.SetPath(History.hs5);
        Sprite image = Resources.Load<Sprite>("Sprites/" + Config.ReadBoradLevel() + "Button");
        ImageHS.sprite = image;
        LevelHS.text = "Level " + Config.ReadGameBoardLevel().ToString();
        ScoreHS.text = "Score " + Config.ReadGameScore().ToString();
        ConfigTime();
        //ModeHS.text = Config.ReadBoradLevel();
    }

    private void ConfigTime()
    {
        float delta_time = Config.ReadGameTime();
        delta_time += Time.deltaTime;
        TimeSpan span = TimeSpan.FromSeconds(delta_time);

        string hour = LeadingZero(span.Hours);
        string minute = LeadingZero(span.Minutes);
        string seconds = LeadingZero(span.Seconds);

        TimeHS.text = "Time " + hour + ":" + minute + ":" + seconds;
    }

    private string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}
