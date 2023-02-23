using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiPlayerBoard : SudokuGrid
{
    private PhotonView photonView;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPhotonView(PhotonView pt)
    {
        photonView = pt;
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

    public override void SetGridNumber(string level, int _selectedData)
    {
        
        SudokuData.SudokuBoardData data = SudokuData.Instance.SudokuGame[level][_slectedData];
        SetGridSquareData(data);
    }

    public override void TryToStartThisGame(int _selectedData)
    {
        SetGridNumber(GameSettings.Instance.GetGameMode(), _selectedData);
    }
}
