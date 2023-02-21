using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    SINGLE,
    MULTI
}

public class GameMode : MonoBehaviour
{
    public static GameMode Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }


    private State _gameState;

    public void SetGameState(State state)
    {
        _gameState = state;
    }

    public void SetGameState(string state)
    {
        if (state == "SINGLE") SetGameState(State.SINGLE);
        else SetGameState(State.MULTI);
    }

    public State GetGameState()
    {
        return _gameState;
    }


}
