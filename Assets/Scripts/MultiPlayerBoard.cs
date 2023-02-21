using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiPlayerBoard : SudokuGrid
{
    private PhotonView photonView;
    [SerializeField] private NetworkManager networkManager;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        photonView = GetComponent<PhotonView>();
        networkManager.SetPlayerLevel(GameSettings.Instance.GameMode());
        networkManager.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CheckBoardCompleted(int number)
    {

    }

    public override void OnSquareSelected(int squareIndex)
    {
        photonView.RPC(nameof(RPC_OnSquareSelected), RpcTarget.AllBuffered, new object[] { squareIndex });
    }

    private void RPC_OnSquareSelected(int squareIndex)
    {
        var tmp = GridSquares[squareIndex].GetComponent<GridSquare>();
        tmp.SetColorSquare(Color.yellow);
    }

    public override void SetGridNumber(string level)
    {
        _slectedData = Random.Range(0, SudokuData.Instance.SudokuGame[level].Count);
        SudokuData.SudokuBoardData data = SudokuData.Instance.SudokuGame[level][_slectedData];
        SetGridSquareData(data);
    }

    public override void TryToStartThisGame()
    {
        if(networkManager.IsRoomFull())
            SetGridNumber(GameSettings.Instance.GetGameMode());
    }
}
