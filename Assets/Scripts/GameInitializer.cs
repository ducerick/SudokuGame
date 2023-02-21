using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private SinglePlayerBoard singlePlayerBoardPrefab;
    [SerializeField] private MultiPlayerBoard multiPlayerBoardPrefab;

    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private Transform boardAnchor;


    private void Awake()
    {
        if (GameMode.Instance.GetGameState() == State.SINGLE)
        {
            CreateSingleplayerBoard();
            InitializeSingleplayerController();
        }
        else
        {
            networkManager.Connect();
        }
    }

    public void CreateMultiplayerBoard()
    {
        if (!networkManager.IsRoomFull())
        {
            PhotonNetwork.Instantiate(multiPlayerBoardPrefab.name, boardAnchor.localPosition, boardAnchor.localRotation);
        }
    }

    public void CreateSingleplayerBoard()
    {
        Instantiate(singlePlayerBoardPrefab, boardAnchor);
    }

    public void InitializeMultiplayerController()
    {
        MultiPlayerBoard board = FindObjectOfType<MultiPlayerBoard>();
        board.TryToStartThisGame();
    }

    public void InitializeSingleplayerController()
    {
        SinglePlayerBoard board = FindObjectOfType<SinglePlayerBoard>();
        board.TryToStartThisGame();
    }

}
