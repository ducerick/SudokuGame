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


    private int _selectedData;

    public void SetSelectedData(int value)
    {
        if (networkManager.photonView.IsMine)
        {
            _selectedData = value;
        }
    }

    private void Start()
    {
        
    }


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

    public PhotonView CreateMultiplayerBoard()
    {
        GameObject mulPrefab = PhotonNetwork.Instantiate(multiPlayerBoardPrefab.name, boardAnchor.position, boardAnchor.rotation);
        mulPrefab.transform.SetParent(boardAnchor);
        mulPrefab.transform.localScale = Vector3.one;
        PhotonView photonView = mulPrefab.GetComponent<PhotonView>();
        if (photonView == null)
        {
            photonView = mulPrefab.AddComponent<PhotonView>();
        }

        photonView.TransferOwnership(PhotonNetwork.MasterClient);
        return photonView;
    }

    public void CreateSingleplayerBoard()
    {
        Instantiate(singlePlayerBoardPrefab, boardAnchor);
    }

    public void InitializeMultiplayerController()
    {
        MultiPlayerBoard board = FindObjectOfType<MultiPlayerBoard>();
        board.TryToStartThisGame(_selectedData);
    }

    public void InitializeSingleplayerController()
    {
        SinglePlayerBoard board = FindObjectOfType<SinglePlayerBoard>();
        board.TryToStartThisGame(0);
    }

}
