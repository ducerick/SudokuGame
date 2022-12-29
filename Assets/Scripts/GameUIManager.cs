using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private Text connectionStatusText;
    [SerializeField] private Button SingleButton;
    [SerializeField] private Button MultiplayerButton;
    public static bool onMultiplayerMode;

    private void Awake()
    {
        connectionStatusText.gameObject.SetActive(false);
        onMultiplayerMode = false;
    }
    public void SetConnectionStatusText(string text)
    {
        connectionStatusText.text = text;
    }

    public void OnSinglePlayerModeSelected()
    {
        DisableButtonAndShowText();
    }

    public void OnMultiPlayerModeSelected()
    {
        onMultiplayerMode = true;
        connectionStatusText.gameObject.SetActive(true);
        DisableButtonAndShowText();
    }

    public void DisableButtonAndShowText()
    {
        SingleButton.gameObject.SetActive(false);
        MultiplayerButton.gameObject.SetActive(false);
    }

    public void OnConnect()
    {
        networkManager.Connect();
    }

    public void OnEasyMode()
    {
        if (onMultiplayerMode) networkManager.SetPlayerLevel(GameSettings.EGameMode.EASY);
    }

    public void OnMediumMode()
    {
        if (onMultiplayerMode) networkManager.SetPlayerLevel(GameSettings.EGameMode.MEDIUM);
    }

    public void OnHardMode()
    {
        if (onMultiplayerMode) networkManager.SetPlayerLevel(GameSettings.EGameMode.HARD);
    }

    public void OnVeryHardMode()
    {
        if (onMultiplayerMode) networkManager.SetPlayerLevel(GameSettings.EGameMode.VERYHARD);
    }

}
