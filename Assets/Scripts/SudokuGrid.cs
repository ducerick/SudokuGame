using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : MonoBehaviour
{
    public int Columns = 0;
    public int Rows = 0;
    public float SquareOffset = 0.0f; // khoang cach giua cac object grid square
    public GameObject GridSquare;
    public Vector2 StartPosition = new Vector2(0.0f, 0.0f);
    public float SquareScale = 1.0f;
    private int _slectedData = -1;
    public Color HighlightColor = Color.red;

    [SerializeField] float SquareSpan;

    private readonly List<GameObject> GridSquares = new List<GameObject>();
    void Start()
    {
        if (GridSquare.GetComponent<GridSquare>() == null)
        {
            Debug.LogError("This Game Object need to have GridSquare script attached!");
        }
        CreateGrid();

        if (GameSettings.Instance.GetContinuePreviousGame())
        {
            SetGridFromFile();
        }
        else if (GameSettings.Instance.GetLoadHistory1()) {
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
        else
        {
            SetGridNumber(GameSettings.Instance.GetGameMode());
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
        foreach(var note in notes)
        {
            GridSquares[note.Key].GetComponent<GridSquare>().SetGridNotes(note.Value);

        }
    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SpawnGridSquares()
    {
        int squareIndex = 0;
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                GridSquares.Add(Instantiate(GridSquare) as GameObject);
                GridSquares[GridSquares.Count - 1].GetComponent<GridSquare>().SetNumberIndex(squareIndex);
                GridSquares[GridSquares.Count - 1].transform.parent = this.transform; // instantiate this game object as a child of the object holding this scipts
                GridSquares[GridSquares.Count - 1].transform.localScale = new Vector3(SquareScale, SquareScale, SquareScale);
                squareIndex++;
            }
        }
    }

    private void SetSquaresPosition()
    {
        var squareRect = GridSquares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        Vector2 squareSpanCount = new Vector2(0.0f, 0.0f);
        bool rowNext = false;

        offset.x = squareRect.rect.width * squareRect.transform.localScale.x + SquareOffset;
        offset.y = squareRect.rect.height * squareRect.transform.localScale.y + SquareOffset;

        int columnNumber = 0;
        int rowNumber = 0;
        foreach (GameObject square in GridSquares)
        {
            if (columnNumber + 1 > Columns)
            {
                rowNumber++;
                columnNumber = 0;
                squareSpanCount.x = 0;
                rowNext = true;
            }

            var posXOffset = offset.x * columnNumber + squareSpanCount.x * SquareSpan;
            var posYoffet = offset.y * rowNumber + squareSpanCount.y * SquareSpan;

            if (columnNumber > 0 && columnNumber % 3 == 0)
            {
                squareSpanCount.x++;
                posXOffset += SquareSpan;
            }

            if (rowNumber > 0 && rowNumber % 3 == 0 && rowNext == true)
            {
                rowNext = false;
                squareSpanCount.y++;
                posYoffet += SquareSpan;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(StartPosition.x + posXOffset, StartPosition.y - posYoffet);
            columnNumber++;
        }
    }

    private void SetGridNumber(string level)
    {
        _slectedData = Random.Range(0, SudokuData.Instance.SudokuGame[level].Count);
        SudokuData.SudokuBoardData data = SudokuData.Instance.SudokuGame[level][_slectedData];
        SetGridSquareData(data);
    }

    private void SetGridSquareData(SudokuData.SudokuBoardData data)
    {
        for (int index = 0; index < GridSquares.Count; index++)
        {
            if (data.UnsolvedData[index] > 0)
            {
                GridSquares[index].GetComponent<GridSquare>().SetEnableChange(false);
            }
            GridSquares[index].GetComponent<GridSquare>().SetNumber(data.UnsolvedData[index]);
            GridSquares[index].GetComponent<GridSquare>().SetCorrectNumber(data.SolvedData[index]);
        }
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

        if(GameSettings.Instance.GetExitAffterWon() == false) // donot save data when exit after completed board
        {
            Config.SaveBoardData(current_game_data, GameSettings.Instance.GetGameMode(), _slectedData, Lives.Instance.GetErrorNumbers(), grid_notes, GameScore.Instance.GetScore());
        }
        else
        {
            Config.DeleteDataFile();
        }

        if(Lives.Instance.GetError() == 3)
        {
            History.SetUpFile();
            Config.SetPath(History.hs1);
            Config.SaveBoardData(current_game_data, GameSettings.Instance.GetGameMode(), _slectedData, Lives.Instance.GetErrorNumbers(), grid_notes, GameScore.Instance.GetScore());
        }
    }

    private void SetColorSquare(int[] data, Color col)
    {
        foreach (var dt in data)
        {
            var tmp = GridSquares[dt].GetComponent<GridSquare>();
            if (tmp.IsSelected() == false || data.Length == 81)
            {
                if (tmp.HasWrongNumber == false)
                    tmp.SetColorSquare(col);
            }
        }
    }

    private void OnSquareSelected(int squareIndex)
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

    private void CheckBoardCompleted(int number)
    {
        foreach(var square in GridSquares)
        {
            var comp = square.GetComponent<GridSquare>();
            if (comp.IsCorrectNumberSet() == false)
            {
                return;
            }

        }

        GameEvents.Instance.OnBoardCompletedMethod();
    }

    public void SolveSudoku()
    {
        foreach(var square in GridSquares)
        {
            var comp = square.GetComponent<GridSquare>();
            comp.SetCorrectNumber();
        }

        CheckBoardCompleted(0);
    }
}
