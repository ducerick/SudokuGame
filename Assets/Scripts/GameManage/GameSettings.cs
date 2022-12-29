using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public enum EGameMode
    {
        NOT_SET,
        EASY,
        MEDIUM,
        HARD,
        VERYHARD
    }

    public static GameSettings Instance;
    private EGameMode _gameMode;
    private bool _continuePreviousGame = false;
    private bool _exitAfterWon = false;
    private bool _Paused = false;
    private bool _loadHis1 = false;
    private bool _loadHis2 = false;
    private bool _loadHis3 = false;
    private bool _loadHis4 = false;
    private bool _loadHis5 = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _continuePreviousGame = false;
    }

    public void SetGameMode(EGameMode mode)
    {
        _gameMode = mode;
    }

    public void SetGameMode(string mode)
    {
        if (mode == "Easy") SetGameMode(EGameMode.EASY);
        else if (mode == "Medium") SetGameMode(EGameMode.MEDIUM);
        else if (mode == "Hard") SetGameMode(EGameMode.HARD);
        else if (mode == "VeryHard") SetGameMode(EGameMode.VERYHARD);
        else SetGameMode(EGameMode.NOT_SET);
    }

    public string GetGameMode()
    {
        switch (_gameMode)
        {
            case EGameMode.EASY: return "Easy";
            case EGameMode.MEDIUM: return "Medium";
            case EGameMode.HARD: return "Hard";
            case EGameMode.VERYHARD: return "VeryHard";
        }
        Debug.LogError("ERROR: Game Level is not set!");
        return "";
    }

    public void SetExitAffterWon(bool set)
    {
        _exitAfterWon = set;
        _continuePreviousGame = false;
    }

    public bool GetExitAffterWon()
    {
        return _exitAfterWon;
    }

    public void SetContinuePreviousGame(bool continue_game)
    {
        _continuePreviousGame = continue_game;
    }

    public bool GetContinuePreviousGame()
    {
        return _continuePreviousGame;
    }

    public void SetLoadHistory1(bool history)
    {
        _loadHis1 = history;
    }

    public bool GetLoadHistory1() => _loadHis1;

    public void SetLoadHistory2(bool history)
    {
        _loadHis2 = history;
    }

    public bool GetLoadHistory2() => _loadHis2;
    public void SetLoadHistory3(bool history)
    {
        _loadHis3 = history;
    }

    public bool GetLoadHistory3() => _loadHis3;
    public void SetLoadHistory4(bool history)
    {
        _loadHis4 = history;
    }

    public bool GetLoadHistory4() => _loadHis4;
    public void SetLoadHistory5(bool history)
    {
        _loadHis5 = history;
    }

    public bool GetLoadHistory5() => _loadHis5;

}
