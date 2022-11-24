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
        SetGridNumber(GameSettings.Instance.GetGameMode());
    }

    // Update is called once per frame
    void Update()
    {

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
                Debug.Log(squareSpanCount.y);
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(StartPosition.x + posXOffset, StartPosition.y - posYoffet);
            columnNumber++;
            Debug.Log(rowNumber);
        }
    }

    private void SetGridNumber(string level)
    {
        //foreach (GameObject square in GridSquares)
        //{
        //    square.GetComponent<GridSquare>().SetNumber(Random.Range(1, 10));
        //}
        _slectedData = Random.Range(0, SudokuData.Instance.SudokuGame[level].Count);
        SudokuData.SudokuBoardData data = SudokuData.Instance.SudokuGame[level][_slectedData];
        SetGridSquareData(data);
    }

    private void SetGridSquareData(SudokuData.SudokuBoardData data)
    {
        for(int index = 0; index < GridSquares.Count; index++)
        {
            if(data.UnsolvedData[index] > 0)
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
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnSquareSelected -= OnSquareSelected;
    }

    private void SetColorSquare(int [] data, Color col)
    {
        foreach(var dt in data)
        {
            var tmp = GridSquares[dt].GetComponent<GridSquare>();
            if (tmp.IsSelected() == false || data.Length == 81 )
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
}
