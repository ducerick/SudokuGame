using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerBoard : SudokuGrid
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        if (GameSettings.Instance.GetContinuePreviousGame())
        {
            SetGridFromFile();
        }
        else if (GameSettings.Instance.GetLoadHistory1())
        {
            Config.SetPath(History.hs1);
            SetGridFromFile();
        }
        else if (GameSettings.Instance.GetLoadHistory2())
        {
            Config.SetPath(History.hs2);
            SetGridFromFile();
        }
        else if (GameSettings.Instance.GetLoadHistory3())
        {
            Config.SetPath(History.hs3);
            SetGridFromFile();
        }
        else if (GameSettings.Instance.GetLoadHistory4())
        {
            Config.SetPath(History.hs4);
            SetGridFromFile();
        }
        else if (GameSettings.Instance.GetLoadHistory5())
        {
            Config.SetPath(History.hs5);
            SetGridFromFile();
        }
    }

    private void SetGridFromFile()
    {
        string level = GameSettings.Instance.GetGameMode();
        _slectedData = Config.ReadGameBoardLevel();
        var data = Config.ReadGridData();

        SetGridSquareData(data);
        SetGridNotes(Config.GetGridNotes());
    }

    private void SetGridNotes(Dictionary<int, List<int>> notes)
    {
        foreach (var note in notes)
        {
            GridSquares[note.Key].GetComponent<GridSquare>().SetGridNotes(note.Value);
        }
    }

    public override void SetGridNumber(string level)
    {
        _slectedData = Random.Range(0, SudokuData.Instance.SudokuGame[level].Count);
        SudokuData.SudokuBoardData data = SudokuData.Instance.SudokuGame[level][_slectedData];
        SetGridSquareData(data);
    }

    public override void OnSquareSelected(int squareIndex)
    {
        var horizontalLine = LineIndicator.Instance.GetHorizontalLine(squareIndex);
        var verticalLine = LineIndicator.Instance.GetVerticalLine(squareIndex);
        var squaresLine = LineIndicator.Instance.GetSquaresLine(squareIndex);
        var allLine = LineIndicator.Instance.GetAllline();

        SetColorSquare(allLine, Color.white);
        SetColorSquare(horizontalLine, HighlightColor);
        SetColorSquare(verticalLine, HighlightColor);
        SetColorSquare(squaresLine, HighlightColor);
    }

    public override void CheckBoardCompleted(int number)
    {
        foreach (var square in GridSquares)
        {
            var comp = square.GetComponent<GridSquare>();
            if (comp.IsCorrectNumberSet() == false)
            {
                return;
            }

        }

        GameEvents.Instance.OnBoardCompletedMethod();
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnSquareSelected += OnSquareSelected;
        GameEvents.Instance.OnUpdateSquareNumber += CheckBoardCompleted;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnSquareSelected -= OnSquareSelected;
        GameEvents.Instance.OnUpdateSquareNumber -= CheckBoardCompleted;

        var solved_data = SudokuData.Instance.SudokuGame[GameSettings.Instance.GetGameMode()][_slectedData].SolvedData;
        int[] unsolved_data = new int[81];
        Dictionary<string, List<string>> grid_notes = new Dictionary<string, List<string>>();

        for (int i = 0; i < GridSquares.Count; i++)
        {
            var comp = GridSquares[i].GetComponent<GridSquare>();
            unsolved_data[i] = comp.GetSquareNumber();
            string key = "square_note:" + i.ToString();
            grid_notes.Add(key, comp.GetSquareNotes());
        }

        SudokuData.SudokuBoardData current_game_data = new SudokuData.SudokuBoardData(unsolved_data, solved_data);

        if (GameSettings.Instance.GetExitAffterWon() == false) // donot save data when exit after completed board
        {
            Config.SaveBoardData(current_game_data, GameSettings.Instance.GetGameMode(), _slectedData, Lives.Instance.GetErrorNumbers(), grid_notes, GameScore.Instance.GetScore());
        }
        else
        {
            Config.DeleteDataFile();
        }

        if (Lives.Instance.GetError() == 3)
        {
            History.SetUpFile();
            Config.SetPath(History.hs1);
            Config.SaveBoardData(current_game_data, GameSettings.Instance.GetGameMode(), _slectedData, Lives.Instance.GetErrorNumbers(), grid_notes, GameScore.Instance.GetScore());
        }
    }

    public override void TryToStartThisGame()
    {
       if (!GameSettings.Instance.GetContinuePreviousGame())
            SetGridNumber(GameSettings.Instance.GetGameMode());
    }
}
