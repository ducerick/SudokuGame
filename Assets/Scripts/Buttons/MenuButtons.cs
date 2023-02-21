using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        GameSettings.Instance.SetContinuePreviousGame(false);
    }

    public void LoadEasyGame(string name)
    {
        LoadScene(name);
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.EASY);
    }

    public void LoadMediumGame(string name)
    {
        LoadScene(name);
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.MEDIUM);
    }

    public void LoadHardGame(string name)
    {
        LoadScene(name);
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.HARD);
    }

    public void LoadVeryHardGame(string name)
    {
        LoadScene(name);
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.VERYHARD);
    }

    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void DeActivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void SetPause(GameObject obj)
    {

    }

    public void ContinuePreviousGame()
    {
        GameSettings.Instance.SetContinuePreviousGame(true);
    }
}
