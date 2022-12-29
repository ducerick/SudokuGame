using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    private int _score = 0;

    public static GameScore Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public int GetScore() => _score;

    public void SetSocre(int score) => _score += score;
}
